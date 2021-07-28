﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using KIP_Backend.Attributes;
using KIP_Backend.DB;
using KIP_Backend.Extensions;
using KIP_Backend.Models.KIP.NoAuth.Helpers;
using KIP_Backend.Models.NoAuth.UI;
using KIP_server_TB.Constants;
using KIP_server_TB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace KIP_server_TB.V1.Controllers
{
    /// <summary>
    /// Webhook controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class WebhookController : Controller
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly KIPDbContext _dbContext;
        private readonly ITelegramBotClient _telegramBotClient;

        private readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="postDbContext">The POST db context.</param>
        /// <param name="telegramBotClient">The telegramBotClient.</param>
        public WebhookController(
            ILogger<WebhookController> logger,
            KIPDbContext postDbContext,
            ITelegramBotClient telegramBotClient)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._dbContext = postDbContext ?? throw new ArgumentNullException(nameof(postDbContext));
            this._telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
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
            WebhookRequest request;

            try
            {
                #region User registration

                using (var reader = new StreamReader(this.Request.Body))
                {
                    request = this.jsonParser.Parse<WebhookRequest>(reader);
                }

                var message = request.QueryResult.QueryText;
                if (string.IsNullOrEmpty(message))
                {
                    // log
                    return this.Ok();
                }

                var intent = request.QueryResult.Intent.DisplayName;
                if (string.IsNullOrEmpty(intent))
                {
                    // log
                    return this.Ok();
                }

                var userId = TelegramRequestProcessing.GetUserId(request);
                if (userId == null)
                {
                    // log
                    return this.Ok();
                }

                var chatId = TelegramRequestProcessing.GetChatId(request);
                if (chatId == null)
                {
                    // log
                    return this.Ok();
                }

                #endregion

                // No auth mode
                #region Faculty output

                if (intent == DialogflowConstants.NoAuthModeIntent)
                {
                    var faculties = this._dbContext.Faculty.OrderBy(f => f.FacultyShortName).AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();
                    foreach (var f in faculties)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(f.FacultyShortName, f.FacultyShortName),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть свій факультет", replyMarkup: inlineKeyboard);
                    return this.Ok();
                }

                #endregion

                #region Course output

                if (intent == DialogflowConstants.FacultyIntent)
                {
                    #region FacultyRegistration

                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    if (user == null)
                    {
                        user = new TelegramUser
                        {
                            UserId = (int)userId,
                            UserName = TelegramRequestProcessing.GetUserName(request),
                            Faculty = message,
                        };

                        await this._dbContext.Users.AddAsync(user);
                        await this._dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        user.Faculty = message;
                        await this._dbContext.SaveChangesAsync();
                    }

                    #endregion

                    var inlineButtons = new List<List<InlineKeyboardButton>>();
                    for (var i = 1; i <= KIPTelegramConstants.MaxCourse; i++)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(i.ToString(), i.ToString()),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть свій курс", replyMarkup: inlineKeyboard);
                    return this.Ok();
                }

                #endregion

                #region Group output

                if (intent == DialogflowConstants.CourseIntent)
                {
                    #region CourseRegistration

                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    if (user == null)
                    {
                        // log
                        return this.Ok();
                    }

                    user.Course = ConvertExtensions.StringToInt(message);
                    await this._dbContext.SaveChangesAsync();

                    #endregion

                    var userFaculty = this._dbContext.Faculty.FirstOrDefault(f => f.FacultyShortName == user.Faculty);
                    var groups = this._dbContext.Group.Where(g => g.Course == user.Course && g.FacultyId == userFaculty.FacultyId).OrderBy(g => g.GroupName);

                    var inlineButtons = new List<List<InlineKeyboardButton>>();
                    foreach (var g in groups)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(g.GroupName, g.GroupName),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть свою групу", replyMarkup: inlineKeyboard);
                    return this.Ok();
                }

                #endregion

                #region Group registration

                if (intent == DialogflowConstants.CourseIntentFallback)
                {
                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    user.Group = message;
                    await this._dbContext.SaveChangesAsync();

                    var outputSb = new StringBuilder();

                    outputSb.AppendLine($"Ваш профіль {user.UserName ?? string.Empty}");
                    outputSb.AppendLine($"Факультет: {user.Faculty}");
                    outputSb.AppendLine($"Курс: {user.Course}");
                    outputSb.AppendLine($"Група: {user.Group}");

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

                #endregion

                // Group schedule
                #region Group schedule

                if (intent == DialogflowConstants.GroupScheduleIntentFallback)
                {
                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    var group = this._dbContext.Group.FirstOrDefault(i => i.GroupName == user.Group);

                    var day = Enum.Parse<Day>(message);
                    var schedule = this._dbContext.StudentSchedule.Where(i => i.GroupId == group.GroupId && i.Day == day).AsNoTracking().ToList();

                    if (schedule.Count == 0)
                    {
                        await this._telegramBotClient.SendTextMessageAsync(
                            chatId,
                            $"Вибачте, але в базі даних немає розкладу для вашої групи {group.GroupName} на {KIPTelegramConstants.DayUkrConstants.GetValueOrDefault(day)}");

                        return this.Ok();
                    }

                    var week0List = schedule.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                    var week1List = schedule.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                    var outputSb = new StringBuilder();

                    outputSb.AppendLine($"{group.GroupName}\n");
                    outputSb.AppendLine("Непарный тиждень:\n");
                    foreach (var l in week0List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Викладач: {l.ProfName}");
                        outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                    }

                    outputSb.AppendLine("\nПарный тиждень:\n");
                    foreach (var l in week1List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Викладач: {l.ProfName}");
                        outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                    return this.Ok();
                }

                #endregion

                // Prof schedule
                #region Cathedras output

                if (intent == DialogflowConstants.ProfScheduleIntent)
                {
                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    var faculty = this._dbContext.Faculty.FirstOrDefault(f => f.FacultyShortName == user.Faculty);
                    var cathedras = this._dbContext.Cathedra.Where(c => c.FacultyId == faculty.FacultyId).OrderBy(c => c.CathedraName).AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();
                    foreach (var c in cathedras)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(c.CathedraName, c.CathedraId.ToString()),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть кафедру", replyMarkup: inlineKeyboard);
                    return this.Ok();
                }

                #endregion

                #region ProfsOutput

                if (intent == DialogflowConstants.ProfScheduleIntentCathedra)
                {
                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    var cathedra = this._dbContext.Cathedra.FirstOrDefault(c => c.CathedraId == ConvertExtensions.StringToInt(message));
                    var profs = this._dbContext.Prof.Where(p => p.CathedraId == cathedra.CathedraId).OrderBy(p => p.ProfSurname).AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();
                    foreach (var p in profs)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData($"{p.ProfSurname} {p.ProfName} {p.ProfPatronymic}", p.ProfSurname),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть викладача", replyMarkup: inlineKeyboard);
                    return this.Ok();
                }

                #endregion

                #region Day output

                if (intent == DialogflowConstants.ProfScheduleIntentProf)
                {
                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    var prof = this._dbContext.Prof.FirstOrDefault(p => p.ProfSurname == message);

                    user.TempProfValue = prof.ProfId;
                    await this._dbContext.SaveChangesAsync();

                    await TelegramRequestProcessing.OutputDaysButtons(this._telegramBotClient, chatId);
                    return this.Ok();
                }

                #endregion

                #region Prof schedule

                if (intent == DialogflowConstants.ProfScheduleIntentDay)
                {
                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    var prof = this._dbContext.Prof.FirstOrDefault(i => i.ProfId == user.TempProfValue);

                    var day = Enum.Parse<Day>(message);
                    var list = this._dbContext.ProfSchedule.Where(i => i.ProfId == user.TempProfValue && i.Day == day).AsNoTracking().ToList();

                    if (list.Count == 0)
                    {
                        await this._telegramBotClient.SendTextMessageAsync(
                            chatId,
                            $"Вибачте, але в базі даних немає розкладу для {prof.ProfSurname} на {KIPTelegramConstants.DayUkrConstants.GetValueOrDefault(day)}");

                        return this.Ok();
                    }

                    var outputSb = new StringBuilder();

                    var week0List = list.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                    var week1List = list.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                    outputSb.AppendLine($"{prof.ProfSurname} {prof.ProfName} {prof.ProfPatronymic}\n");
                    outputSb.AppendLine("Непарный тиждень:\n");
                    foreach (var l in week0List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                        outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                    }

                    outputSb.AppendLine("\nПарный тиждень:\n");
                    foreach (var l in week1List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                        outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                    return this.Ok();
                }

                #endregion

                // Audience schedule
                #region Buildings output

                if (intent == DialogflowConstants.AudienceScheduleIntent)
                {
                    var buildings = this._dbContext.Building.OrderBy(b => b.BuildingShortName).AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();
                    foreach (var b in buildings)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData($"{b.BuildingName} ({b.BuildingShortName})", b.BuildingId.ToString()),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть навчальний корпус", replyMarkup: inlineKeyboard);
                    return this.Ok();
                }

                #endregion

                #region Audiences output

                if (intent == DialogflowConstants.AudienceScheduleIntentBuilding)
                {
                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    user.TempBuildingValue = ConvertExtensions.StringToInt(message);
                    await this._dbContext.SaveChangesAsync();

                    var audiences = this._dbContext.Audience.Where(a => a.BuildingId == user.TempBuildingValue).OrderBy(a => a.AudienceName)
                        .AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();
                    foreach (var a in audiences)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData($"{a.AudienceName}", $"Audience:{a.AudienceName}"),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть навчальну аудиторію", replyMarkup: inlineKeyboard);
                    return this.Ok();
                }

                #endregion

                #region Day output

                if (intent == DialogflowConstants.AudienceScheduleIntentAudience)
                {
                    message = message.Split(':')[1];

                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    var audience = this._dbContext.Audience.FirstOrDefault(a => a.AudienceName.Contains(message));

                    user.TempAudienceValue = audience.AudienceId;
                    await this._dbContext.SaveChangesAsync();

                    await TelegramRequestProcessing.OutputDaysButtons(this._telegramBotClient, chatId);
                    return this.Ok();
                }

                #endregion

                #region Audience schedule

                if (intent == DialogflowConstants.AudienceScheduleIntentDay)
                {
                    var user = this._dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    var audience = this._dbContext.Audience.FirstOrDefault(a => a.AudienceId == user.TempAudienceValue);

                    var day = Enum.Parse<Day>(message);
                    var schedule = this._dbContext.AudienceSchedule.Where(i => i.AudienceId == user.TempAudienceValue && i.Day == day).AsNoTracking().ToList();

                    if (schedule.Count == 0)
                    {
                        await this._telegramBotClient.SendTextMessageAsync(
                            chatId,
                            $"Вибачте, але в базі даних немає розкладу для {audience.AudienceName} на {KIPTelegramConstants.DayUkrConstants.GetValueOrDefault(day)}");

                        return this.Ok();
                    }

                    var outputSb = new StringBuilder();

                    var week0List = schedule.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                    var week1List = schedule.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                    outputSb.AppendLine($"{audience.AudienceName}\n");
                    outputSb.AppendLine("Непарный тиждень:\n");
                    foreach (var l in week0List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                        outputSb.AppendLine($"  Викладач: {l.ProfName}\n");
                    }

                    outputSb.AppendLine("\nПарный тиждень:\n");
                    foreach (var l in week1List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                        outputSb.AppendLine($"  Викладач: {l.ProfName}\n");
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());
                    return this.Ok();
                }

                #endregion
            }
            catch (Exception ex)
            {
                // log
                Console.WriteLine(ex.Message);
                return this.Ok();
            }

            // log
            return this.Ok();
        }
    }
}
