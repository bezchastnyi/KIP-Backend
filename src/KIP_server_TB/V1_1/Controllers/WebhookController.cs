﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using KIP_Backend.Attributes;
using KIP_Backend.Constants;
using KIP_Backend.Extensions;
using KIP_Backend.Models.Auth;
using KIP_Backend.Models.Helpers;
using KIP_Backend.Models.NoAuth;
using KIP_server_TB.Constants;
using KIP_server_TB.DB;
using KIP_server_TB.Models;
using KIP_server_TB.Services;
using KIP_server_TB.V1_1.NoAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace KIP_server_TB.V1_1.Controllers
{
    /// <summary>
    /// Webhook controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1_1]
    [ApiRoute]
    [ApiController]
    public class WebhookController : Controller
    {
        private readonly string _noAuthServerUrl;
        private readonly string _authServerUrl;

        private readonly ILogger<WebhookController> _logger;
        private readonly TelegramApiDbContext _dbContext;
        private readonly ITelegramBotClient _telegramBotClient;

        private readonly JsonParser _jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The db context.</param>
        /// <param name="telegramBotClient">The telegramBotClient.</param>
        /// <param name="configuration">The configuration.</param>
        public WebhookController(ILogger<WebhookController> logger, TelegramApiDbContext dbContext, ITelegramBotClient telegramBotClient, IConfiguration configuration)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));

            this._noAuthServerUrl = configuration?.GetConnectionString("KIP-Backend-NoAuth");
            this._authServerUrl = configuration?.GetConnectionString("KIP-Backend-Auth");
        }

        /// <summary>
        /// Current rank of student's group.
        /// </summary>
        /// <returns>Start message.</returns>
        [HttpPost]
        [Route("receive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:Do not place regions within elements", Justification = "Webhook receive")]
        public async Task<OkResult> ReceiveAsync()
        {
            try
            {
                #region User registration

                using var reader = new StreamReader(this.Request.Body);
                var request = this._jsonParser.Parse<WebhookRequest>(await reader.ReadToEndAsync());

                var message = request.QueryResult.QueryText;
                if (string.IsNullOrEmpty(message))
                {
                    // TODO log
                    return this.Ok();
                }

                var intent = request.QueryResult.Intent.DisplayName;
                if (string.IsNullOrEmpty(intent))
                {
                    // TODO log
                    return this.Ok();
                }

                var userId = TelegramRequestProcessing.GetUserId(request);
                if (userId == null)
                {
                    // TODO log
                    return this.Ok();
                }

                var chatId = TelegramRequestProcessing.GetChatId(request);
                if (chatId == null)
                {
                    // TODO log
                    return this.Ok();
                }

                #endregion

                switch (intent)
                {
                    #region No auth mode

                    // Faculty output
                    case DialogflowConstants.NoAuthModeIntent:
                    {
                        var faculties = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Faculty>(
                            $"{this._noAuthServerUrl}/{RoutConstants.AllFaculties}", this._logger);

                        if (faculties == null || !faculties.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        faculties = faculties.OrderBy(f => f.FacultyShortName);
                        var inlineButtons = faculties.Select(f => new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData($"{f.FacultyShortName}", $"FacultyId:{f.FacultyId}"),
                        }).ToList();

                        var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                        await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть свій факультет", replyMarkup: inlineKeyboard);
                        return this.Ok();
                    }

                    // Course output
                    case DialogflowConstants.FacultyIntent:
                    {
                        var facultyId = ConvertExtensions.StringToInt(message.Split(':')[1]);
                        var faculties = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Faculty>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.FacultyById, facultyId)}",
                            this._logger);

                        if (faculties == null || !faculties.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var faculty = faculties.FirstOrDefault();
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            user = new TelegramUser
                            {
                                UserId = (int)userId,
                                UserName = TelegramRequestProcessing.GetUserName(request),
                                FacultyId = faculty?.FacultyId,
                                FacultyName = faculty?.FacultyName,
                                FacultyShortName = faculty?.FacultyShortName,
                            };

                            await this._dbContext.Users.AddAsync(user);
                            await this._dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            user.FacultyId = faculty?.FacultyId;
                            user.FacultyName = faculty?.FacultyName;
                            user.FacultyShortName = faculty?.FacultyShortName;

                            await this._dbContext.SaveChangesAsync();
                        }

                        var inlineButtons = new List<List<InlineKeyboardButton>>();
                        for (var i = 1; i <= KIPTelegramConstants.MaxCourse; i++)
                        {
                            inlineButtons.Add(new List<InlineKeyboardButton>
                            {
                                InlineKeyboardButton.WithCallbackData($"{i.ToString()} курс", $"Course:{i.ToString()}"),
                            });
                        }

                        var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                        await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть свій курс", replyMarkup: inlineKeyboard);
                        return this.Ok();
                    }

                    // Group output
                    case DialogflowConstants.CourseIntent:
                    {
                        var course = ConvertExtensions.StringToInt(message.Split(':')[1]);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        user.Course = course;
                        await this._dbContext.SaveChangesAsync();

                        var groups = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Group>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.GroupsByFacultyId, user.FacultyId)}",
                            this._logger);

                        if (groups == null || !groups.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        groups = groups.Where(g => g.Course == user.Course).OrderBy(g => g.GroupName);
                        var inlineButtons = groups.Select(g => new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(g.GroupName, $"Group:{g.GroupId}:{g.GroupName}"),
                        }).ToList();

                        var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                        await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть свою групу", replyMarkup: inlineKeyboard);
                        return this.Ok();
                    }

                    // Group registration
                    case DialogflowConstants.CourseIntentFallback:
                    {
                        var data = message.Split(':');

                        var groupId = ConvertExtensions.StringToInt(data[1]);
                        var groupName = data[2];

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        user.GroupId = groupId;
                        user.GroupName = groupName;
                        await this._dbContext.SaveChangesAsync();

                        var outputSb = new StringBuilder();

                        outputSb.AppendLine($"Ваш профіль {user.UserName ?? string.Empty}");
                        outputSb.AppendLine($"Факультет: {user.FacultyShortName} ({user.FacultyName})");
                        outputSb.AppendLine($"Курс: {user.Course}");
                        outputSb.AppendLine($"Група: {user.GroupName}");

                        var inlineButtons = new List<List<InlineKeyboardButton>>
                        {
                            new List<InlineKeyboardButton>
                            {
                                InlineKeyboardButton.WithCallbackData("Вірно ✅", DialogflowConstants.ScheduleIntent),
                                InlineKeyboardButton.WithCallbackData("Невірно ❌", "Гостьовий режим"),
                            },
                        };

                        var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                        await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString(), replyMarkup: inlineKeyboard);
                        return this.Ok();
                    }

                    // Group schedule
                    // Day output
                    case DialogflowConstants.GroupScheduleIntent:
                    {
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вашого профілю немає в базі даних\nОновити профіль: /start");
                            return this.Ok();
                        }

                        var schedules = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<StudentSchedule>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.StudentScheduleByGroupId, user.GroupId)}",
                            this._logger);

                        if (schedules == null)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var schedule = schedules?.ToList();
                        if (schedule?.Count == 0)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(
                                chatId,
                                $"Вибачте, але в базі даних немає розкладу для вашої групи {user.GroupName}");

                            return this.Ok();
                        }

                        await TelegramRequestProcessing.OutputDaysButtons(this._telegramBotClient, chatId, "GroupSchedule");
                        return this.Ok();
                    }

                    // Group schedule
                    case DialogflowConstants.GroupScheduleIntentDay:
                    {
                        message = message.Split(':')[1];
                        var day = Enum.Parse<Day>(message);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        var schedules = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<StudentSchedule>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.StudentScheduleByGroupIdAndDay, user.GroupId, (int)day)}",
                            this._logger);

                        if (schedules == null)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var schedule = schedules?.ToList();
                        if (schedule?.Count == 0)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(
                                chatId,
                                $"Немає розкладу для вашої групи {user.GroupName} на {KIPTelegramConstants.DayUkrConstants.GetValueOrDefault(day)}");

                            return this.Ok();
                        }

                        var week0List = schedule?.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                        var week1List = schedule?.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                        var outputSb = new StringBuilder();

                        outputSb.AppendLine($"{user.GroupName}\n");
                        outputSb.AppendLine("Непарный тиждень:\n");
                        foreach (var l in week0List)
                        {
                            outputSb.AppendLine(
                                $"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                            outputSb.AppendLine($"  Викладач: {l.ProfName}");
                            outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                        }

                        outputSb.AppendLine("\nПарный тиждень:\n");
                        foreach (var l in week1List)
                        {
                            outputSb.AppendLine(
                                $"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                            outputSb.AppendLine($"  Викладач: {l.ProfName}");
                            outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                        }

                        await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                        return this.Ok();
                    }

                    // Prof schedule
                    // Cathedras output
                    case DialogflowConstants.ProfScheduleIntent:
                    {
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вашого профілю немає в базі даних\nОновити профіль: /start");
                            return this.Ok();
                        }

                        var cathedras = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Cathedra>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.CathedrasByFacultyId, user.FacultyId)}",
                            this._logger);

                        if (cathedras == null || !cathedras.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        cathedras = cathedras.OrderBy(c => c.CathedraName);
                        var inlineButtons = cathedras.Select(c => new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(c.CathedraName, $"CathedraId:{c.CathedraId}"),
                        }).ToList();

                        var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                        await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть кафедру", replyMarkup: inlineKeyboard);
                        return this.Ok();
                    }

                    // Profs output
                    case DialogflowConstants.ProfScheduleIntentCathedra:
                    {
                        var cathedraId = ConvertExtensions.StringToInt(message.Split(':')[1]);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        var profs = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Prof>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.ProfsByCathedraId, cathedraId)}",
                            this._logger);

                        if (profs == null || !profs.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        profs = profs.OrderBy(p => p.ProfSurname);
                        var inlineButtons = profs.Select(p => new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(
                                $"{p.ProfSurname} {p.ProfName} {p.ProfPatronymic}", $"ProfId:{p.ProfId}"),
                        }).ToList();

                        var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                        await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть викладача", replyMarkup: inlineKeyboard);
                        return this.Ok();
                    }

                    // Day output
                    case DialogflowConstants.ProfScheduleIntentProf:
                    {
                        var profId = ConvertExtensions.StringToInt(message.Split(':')[1]);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        user.TempProfValue = profId;
                        await this._dbContext.SaveChangesAsync();

                        var profs = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Prof>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.ProfById, user.TempProfValue)}",
                            this._logger);

                        if (profs == null || !profs.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var prof = profs?.First();

                        var schedules = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<ProfSchedule>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.ProfScheduleByProfId, user.TempProfValue)}",
                            this._logger);

                        if (schedules == null)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var schedule = schedules?.ToList();
                        if (schedule?.Count == 0)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(
                                chatId,
                                $"Вибачте, але в базі даних немає розкладу для {prof?.ProfSurname}");

                            return this.Ok();
                        }

                        await TelegramRequestProcessing.OutputDaysButtons(this._telegramBotClient, chatId, "ProfSchedule");
                        return this.Ok();
                    }

                    // Prof schedule
                    case DialogflowConstants.ProfScheduleIntentDay:
                    {
                        message = message.Split(':')[1];
                        var day = Enum.Parse<Day>(message);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        var profs = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Prof>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.ProfById, user.TempProfValue)}",
                            this._logger);

                        if (profs == null || !profs.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var prof = profs?.First();

                        var schedules = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<ProfSchedule>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.ProfScheduleByProfIdAndDay, user.TempProfValue, (int)day)}",
                            this._logger);

                        if (schedules == null)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var schedule = schedules?.ToList();
                        if (schedule?.Count == 0)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(
                                chatId,
                                $"Немає розкладу для {prof?.ProfSurname} на {KIPTelegramConstants.DayUkrConstants.GetValueOrDefault(day)}");

                            return this.Ok();
                        }

                        var outputSb = new StringBuilder();

                        var week0List = schedule?.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                        var week1List = schedule?.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                        outputSb.AppendLine($"{prof?.ProfSurname} {prof?.ProfName} {prof?.ProfPatronymic}\n");
                        outputSb.AppendLine("Непарный тиждень:\n");
                        foreach (var l in week0List)
                        {
                            outputSb.AppendLine(
                                $"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                            outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                            outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                        }

                        outputSb.AppendLine("\nПарный тиждень:\n");
                        foreach (var l in week1List)
                        {
                            outputSb.AppendLine(
                                $"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                            outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                            outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                        }

                        await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                        return this.Ok();
                    }

                    // Audience schedule
                    // Buildings output
                    case DialogflowConstants.AudienceScheduleIntent:
                    {
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вашого профілю немає в базі даних\nОновити профіль: /start");
                            return this.Ok();
                        }

                        var buildings = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Building>(
                            $"{this._noAuthServerUrl}/{RoutConstants.AllBuildings}", this._logger);

                        if (buildings == null || !buildings.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        buildings = buildings.OrderBy(b => b.BuildingShortName);
                        var inlineButtons = buildings.Select(b => new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData($"{b.BuildingName} ({b.BuildingShortName})", $"BuildingId:{b.BuildingId}"),
                        }).ToList();

                        var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                        await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть навчальний корпус", replyMarkup: inlineKeyboard);
                        return this.Ok();
                    }

                    // Audiences output
                    case DialogflowConstants.AudienceScheduleIntentBuilding:
                    {
                        var buildingId = ConvertExtensions.StringToInt(message.Split(':')[1]);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        user.TempBuildingValue = buildingId;
                        await this._dbContext.SaveChangesAsync();

                        var audiences = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Audience>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.AudiencesByBuildingId, buildingId)}",
                            this._logger);

                        if (audiences == null || !audiences.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        audiences = audiences.OrderBy(a => a.AudienceName);
                        var inlineButtons = audiences.Select(a => new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData($"{a.AudienceName}", $"AudienceId:{a.AudienceId}"),
                        }).ToList();

                        var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                        await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть навчальну аудиторію", replyMarkup: inlineKeyboard);
                        return this.Ok();
                    }

                    // Day output
                    case DialogflowConstants.AudienceScheduleIntentAudience:
                    {
                        var audienceId = ConvertExtensions.StringToInt(message.Split(':')[1]);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        user.TempAudienceValue = audienceId;
                        await this._dbContext.SaveChangesAsync();

                        var audiences = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Audience>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.AudienceById, user.TempAudienceValue)}",
                            this._logger);

                        if (audiences == null || !audiences.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var audience = audiences?.First();

                        var schedules = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<AudienceSchedule>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.AudienceScheduleByAudienceId, user.TempAudienceValue)}",
                            this._logger);

                        if (schedules == null)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var schedule = schedules?.ToList();
                        if (schedule?.Count == 0)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(
                                chatId,
                                $"Вибачте, але в базі даних немає розкладу для {audience?.AudienceName}");

                            return this.Ok();
                        }

                        await TelegramRequestProcessing.OutputDaysButtons(this._telegramBotClient, chatId, "AudienceSchedule");
                        return this.Ok();
                    }

                    // Audience schedule
                    case DialogflowConstants.AudienceScheduleIntentDay:
                    {
                        message = message.Split(':')[1];
                        var day = Enum.Parse<Day>(message);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        var audiences = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<Audience>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.AudienceById, user.TempAudienceValue)}",
                            this._logger);

                        if (audiences == null || !audiences.Any())
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var audience = audiences?.First();

                        var schedules = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<AudienceSchedule>(
                            $"{this._noAuthServerUrl}/{string.Format(RoutConstants.AudienceScheduleByAudienceIdAndDay, user.TempAudienceValue, (int)day)}",
                            this._logger);

                        if (schedules == null)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Вибачте за незручності 🥺 але сервер тимчасово не працює\nkip.ntu.khpi@gmail.com - пошта для зв'язку");

                            // TODO log
                            return this.Ok();
                        }

                        var schedule = schedules?.ToList();
                        if (schedule?.Count == 0)
                        {
                            await this._telegramBotClient.SendTextMessageAsync(
                                chatId,
                                $"Немає розкладу для {audience?.AudienceName} на {KIPTelegramConstants.DayUkrConstants.GetValueOrDefault(day)}");

                            return this.Ok();
                        }

                        var outputSb = new StringBuilder();

                        var week0List = schedule?.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                        var week1List = schedule?.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                        outputSb.AppendLine($"{audience?.AudienceName}\n");
                        outputSb.AppendLine("Непарный тиждень:\n");
                        foreach (var l in week0List)
                        {
                            outputSb.AppendLine(
                                $"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                            outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                            outputSb.AppendLine($"  Викладач: {l.ProfName}\n");
                        }

                        outputSb.AppendLine("\nПарный тиждень:\n");
                        foreach (var l in week1List)
                        {
                            outputSb.AppendLine(
                                $"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                            outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                            outputSb.AppendLine($"  Викладач: {l.ProfName}\n");
                        }

                        await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                        return this.Ok();
                    }

                    #endregion

                    #region Auth mode

                    case DialogflowConstants.EmailIntent:
                    {
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            user = new TelegramUser
                            {
                                UserId = (int)userId,
                                UserName = TelegramRequestProcessing.GetUserName(request),
                                UserEmail = message,
                            };

                            await this._dbContext.Users.AddAsync(user);
                        }
                        else
                        {
                            user.UserEmail = message;
                        }

                        await this._dbContext.SaveChangesAsync();
                        return this.Ok();
                    }

                    case DialogflowConstants.PasswordIntent:
                    {
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        user.UserPassword = message;
                        await this._dbContext.SaveChangesAsync();

                        var personalInformation = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<PersonalInformation>(
                            $"{this._authServerUrl}/{string.Format(Auth.RoutConstants.PersonalInformation, user.UserEmail, user.UserPassword)}",
                            this._logger);

                        if (personalInformation == null || !personalInformation.Any())
                        {
                            // TODO log
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Логін або пароль невірні\n" +
                                "Якщо все ж таки дані вірні, а вхід не відбувається - пошта для зв'язку з розробниками KIP kip.ntu.khpi@gmail.com");
                            return this.Ok();
                        }

                        var studentInfo = personalInformation.ToList()[0];

                        user.FacultyId = studentInfo.FacultyId;
                        user.FacultyName = studentInfo.Faculty;

                        var facultyShortName = FacultiesShortNames.ShortNames.FirstOrDefault(f => studentInfo.Faculty.Contains(f.Key)).Value;
                        user.FacultyShortName = facultyShortName;

                        user.GroupId = studentInfo.GroupId;
                        user.GroupName = studentInfo.Group;

                        user.Course = studentInfo.Course;
                        await this._dbContext.SaveChangesAsync();

                        var keyboard = new ReplyKeyboardMarkup
                        {
                            Keyboard = new[]
                            {
                                new[]
                                {
                                    new KeyboardButton("Мій профіль"),
                                    new KeyboardButton("Мій розклад"),
                                },

                                new[]
                                {
                                    new KeyboardButton("Залікова книжка"),
                                    new KeyboardButton("Навчальний план"),
                                },

                                new[]
                                {
                                    new KeyboardButton("Рейтинг"),
                                    new KeyboardButton("Борги"),
                                },

                                new[]
                                {
                                    new KeyboardButton("Розклад викладачів"),
                                    new KeyboardButton("Розклад аудиторій"),
                                },
                            },
                        };

                        await this._telegramBotClient.SendTextMessageAsync(chatId, GetStudentPersonalInformation(user, studentInfo), replyMarkup: keyboard);
                        return this.Ok();
                    }

                    case DialogflowConstants.StudentInformationIntent:
                    {
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        var personalInformation = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<PersonalInformation>(
                            $"{this._authServerUrl}/{string.Format(Auth.RoutConstants.PersonalInformation, user.UserEmail, user.UserPassword)}",
                            this._logger);

                        if (personalInformation == null || !personalInformation.Any())
                        {
                            // TODO log
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Не вдалося отримати особисту інформацію\n\n" +
                                "Якщо проблема не вирішиться - пошта для зв'язку з розробниками KIP kip.ntu.khpi@gmail.com");
                            return this.Ok();
                        }

                        var studentInfo = personalInformation.ToList()[0];

                        await this._telegramBotClient.SendTextMessageAsync(chatId, GetStudentPersonalInformation(user, studentInfo));
                        return this.Ok();
                    }

                    case DialogflowConstants.StudentDebtsIntent:
                    {
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        var debts = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<DebtList>(
                            $"{this._authServerUrl}/{string.Format(Auth.RoutConstants.DebtList, user.UserEmail, user.UserPassword)}",
                            this._logger);

                        if (debts == null || !debts.Any())
                        {
                            // TODO log
                            await this._telegramBotClient.SendTextMessageAsync(chatId, "Не вдалося отримати інформацію про борги\n\n" +
                                "Якщо проблема не вирішиться - пошта для зв'язку з розробниками KIP kip.ntu.khpi@gmail.com");
                            return this.Ok();
                        }

                        var debtList = debts.ToList();
                        var outputSb = new StringBuilder();
                        outputSb.AppendLine("Ваші Борги\n");

                        for (var i = 0; i < debtList.Count; i++)
                        {
                            outputSb.AppendLine($"{i + 1}. Дисципліна: {debtList[i].Subject}");
                            outputSb.AppendLine($"     Викладач: {debtList[i].Prof} ({debtList[i].ShortCathedra})");
                            outputSb.AppendLine($"     Курс: {debtList[i].Course}");
                            outputSb.AppendLine($"     Семестр: {debtList[i].Semester}");
                            outputSb.AppendLine($"     Дата: {debtList[i].Date}");
                            outputSb.AppendLine($"     Е\\З: {debtList[i].Control}");
                            outputSb.AppendLine($"     Кредити: {debtList[i].Credits}\n");
                        }

                        await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                        return this.Ok();
                    }

                    case DialogflowConstants.CurrentRankIntent: // TODO check how it works when it appears
                    {
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        var rank = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<CurrentRank>(
                            $"{this._authServerUrl}/{string.Format(Auth.RoutConstants.CurrentRank, user.UserEmail, user.UserPassword)}",
                            this._logger);

                        if (rank == null || !rank.Any())
                        {
                            // TODO log
                            await this._telegramBotClient.SendTextMessageAsync(chatId, $"Не вдалося отримати інформацію про поточний рейтинг вашої групи {user.GroupName} (можливо її ще немає)\n\n" +
                                "Якщо проблема не вирішиться - пошта для зв'язку з розробниками KIP kip.ntu.khpi@gmail.com");
                            return this.Ok();
                        }

                        var currentRank = rank.ToList();
                        var outputSb = new StringBuilder();
                        outputSb.AppendLine($"Поточний рейтинг вашої групи {currentRank[0].Group}\n");

                        foreach (var student in currentRank)
                        {
                            outputSb.AppendLine($"{student.Rank}. {student.FIO} {student.FullRankMark} ({student.ShortRankMark}) {student.RankFormula}\n");
                        }

                        await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                        return this.Ok();
                    }

                    case DialogflowConstants.SemesterMarksIntent:
                    {
                        message = message.Split(':')[1];
                        var semester = ConvertExtensions.StringToInt(message);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        var marks = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<SemesterMarksList>(
                            $"{this._authServerUrl}/{string.Format(Auth.RoutConstants.SemesterMarksList, user.UserEmail, user.UserPassword, semester)}",
                            this._logger);

                        if (marks == null || !marks.Any())
                        {
                            // TODO log
                            await this._telegramBotClient.SendTextMessageAsync(chatId, $"Не вдалося отримати інформацію про ваші оцінки на {semester} семестр\n\n" +
                                "Якщо проблема не вирішиться - пошта для зв'язку з розробниками KIP kip.ntu.khpi@gmail.com");
                            return this.Ok();
                        }

                        var marksList = marks.ToList();
                        var outputSb = new StringBuilder();
                        outputSb.AppendLine($"Ваша залікова книжка на {semester} семестр\n");

                        for (var i = 0; i < marksList.Count; i++)
                        {
                            // TODO marksList[i].IsDebt
                            if (string.IsNullOrEmpty(marksList[i].ECTSMark))
                            {
                                outputSb.AppendLine($"{i + 1}. БОРГ ❗️❗️❗️");
                                outputSb.AppendLine($"     Дисципліна: {marksList[i].Subject}");
                                outputSb.AppendLine($"     Викладач: {marksList[i].Prof} ({marksList[i].ShortCathedra})");
                                outputSb.AppendLine($"     Е\\З: {marksList[i].Control}\n");

                                continue;
                            }

                            outputSb.AppendLine($"{i + 1}. Дисципліна: {marksList[i].Subject}");
                            outputSb.AppendLine($"     Викладач: {marksList[i].Prof} ({marksList[i].ShortCathedra})");
                            outputSb.AppendLine($"     Е\\З: {marksList[i].Control}");
                            outputSb.AppendLine($"     Національний бал: {marksList[i].NationalMark}");
                            outputSb.AppendLine($"     Загальний бал: {marksList[i].ShortMark}");
                            outputSb.AppendLine($"     ECTS: {marksList[i].ECTSMark}\n");
                        }

                        await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                        return this.Ok();
                    }

                    case DialogflowConstants.StudyingPlanIntent:
                    {
                        message = message.Split(':')[1];
                        var semester = ConvertExtensions.StringToInt(message);

                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        var studyingPlan = await ConvertExtensions.ConvertJsonDataToListOfModelsAsync<SemesterStudyingPlan>(
                            $"{this._authServerUrl}/{string.Format(Auth.RoutConstants.SemesterStudyingPlan, user.UserEmail, user.UserPassword, semester)}",
                            this._logger);

                        if (studyingPlan == null || !studyingPlan.Any())
                        {
                            // TODO log
                            await this._telegramBotClient.SendTextMessageAsync(chatId, $"Не вдалося отримати інформацію про навчальний план для вашої групи {user.GroupName} на {semester} семестр\n\n" +
                                "Якщо проблема не вирішиться - пошта для зв'язку з розробниками KIP kip.ntu.khpi@gmail.com");
                            return this.Ok();
                        }

                        var studyingPlanList = studyingPlan.ToList();
                        var outputSb = new StringBuilder();
                        outputSb.AppendLine($"Навчальний план для вашої групи {user.GroupName} на {semester} семестр\n");

                        for (var i = 0; i < studyingPlanList.Count; i++)
                        {
                            outputSb.AppendLine($"{i + 1}. Дисципліна: {studyingPlanList[i].Subject}");
                            outputSb.AppendLine($"     Кафедра: {studyingPlanList[i].ShortCathedra}");
                            outputSb.AppendLine($"     Е\\З: {studyingPlanList[i].Control}");
                            outputSb.AppendLine($"     Години: {studyingPlanList[i].Audits}");
                            outputSb.AppendLine($"     Кредити: {studyingPlanList[i].Credits}\n");
                        }

                        await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                        return this.Ok();
                    }

                    #endregion

                    // Exit command
                    case DialogflowConstants.ExitIntent:
                    {
                        var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == (int)userId);
                        if (user == null)
                        {
                            // TODO log
                            return this.Ok();
                        }

                        this._dbContext.Users.Remove(user);
                        await this._dbContext.SaveChangesAsync();
                        return this.Ok();
                    }

                    default:
                    {
                        // TODO log
                        return this.Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO log
                Console.WriteLine(ex.Message);
                return this.Ok();
            }
        }

        private static string GetStudentPersonalInformation(TelegramUser user, PersonalInformation studentInfo)
        {
            var facultyShortName = FacultiesShortNames.ShortNames.FirstOrDefault(f => studentInfo.Faculty.Contains(f.Key)).Value;
            var outputSb = new StringBuilder();

            outputSb.AppendLine("Особистий кабінет студента\n");

            outputSb.AppendLine($"Прізвище: {studentInfo.LastName}");
            outputSb.AppendLine($"Ім'я: {studentInfo.FirstName}");
            outputSb.AppendLine($"По-батькові: {studentInfo.Patronymic}\n");

            outputSb.AppendLine($"Курс: {studentInfo.Course}");
            outputSb.AppendLine($"Група: {studentInfo.Group}");
            outputSb.AppendLine($"Факультет/інститут: {studentInfo.Faculty} ({facultyShortName ?? string.Empty})");
            outputSb.AppendLine($"Кафедра: {studentInfo.Cathedra}\n");

            outputSb.AppendLine($"Спеціалізація/пропозиція/блок: {studentInfo.Specialization}");
            outputSb.AppendLine($"Спеціальність: {studentInfo.Specialty}");
            outputSb.AppendLine($"Освітня програма: {studentInfo.StudyingProgram}");
            outputSb.AppendLine($"Рівень навчання: {studentInfo.StudyingLevel}");
            outputSb.AppendLine($"Форма навчання: {studentInfo.StudyingForm}");
            outputSb.AppendLine($"Оплата: {studentInfo.BudgetForm}");

            return outputSb.ToString();
        }
    }
}
