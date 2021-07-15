// <copyright file="WebhookController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using KIP_Backend.Attributes;
using KIP_Backend.Extensions;
using KIP_POST_APP.DB;
using KIP_server_TB.DB;
using KIP_server_TB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

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
        private readonly TelegramDbContext _telegramDbContext;
        private readonly PostDbContext _postDbContext;

        private readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="telegramDbContext">The telegram db context.</param>
        /// <param name="postDbContext">The POST db context.</param>
        public WebhookController(ILogger<WebhookController> logger, TelegramDbContext telegramDbContext, PostDbContext postDbContext)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._telegramDbContext = telegramDbContext ?? throw new ArgumentNullException(nameof(telegramDbContext));
            this._postDbContext = postDbContext ?? throw new ArgumentNullException(nameof(postDbContext));
        }

        /// <summary>
        /// Current rank of student's group.
        /// </summary>
        /// <returns>Start message.</returns>
        [HttpPost]
        [Route("receive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:Do not place regions within elements", Justification = "Webhook receive")]
        public async Task<IActionResult> ReceiveAsync()
        {
            WebhookRequest request;

            try
            {
                #region Identification

                using (var reader = new StreamReader(this.Request.Body))
                {
                    request = this.jsonParser.Parse<WebhookRequest>(reader);
                }

                var message = request.QueryResult.QueryText;

                if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                {
                    // log
                    return this.Ok();
                }

                double userId = 0;

                try
                {
                    userId = GetUserIdFromButton(request);
                }
                catch
                {
                    // log
                    userId = GetUserIdFromMessage(request);
                }

                if (userId == 0)
                {
                    // log
                    return this.Ok();
                }

                #endregion

                #region Faculty

                if (message.Contains("Faculty"))
                {
                    var messageContent = message.Split(":");
                    var facultyValue = messageContent[1];

                    if (string.IsNullOrEmpty(facultyValue))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    if (user == null)
                    {
                        string userName = null;
                        try
                        {
                            userName = GetUserNameFromButton(request);
                        }
                        catch (Exception ex)
                        {
                            // log
                            Console.WriteLine(ex.Message);
                        }

                        var newUser = new TelegramUser()
                        {
                            UserId = (int)userId,
                            UserName = userName,
                            Faculty = facultyValue,
                        };

                        await this._telegramDbContext.Users.AddAsync(newUser);
                        await this._telegramDbContext.SaveChangesAsync();

                        // log
                        return this.Ok();
                    }

                    user.Faculty = facultyValue;
                    await this._telegramDbContext.SaveChangesAsync();
                }

                #endregion

                #region Course

                if (message.Contains("Course"))
                {
                    var messageContent = message.Split(":");
                    var courseValue = messageContent[1];

                    if (string.IsNullOrEmpty(courseValue))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    if (user == null)
                    {
                        // log
                        return this.Ok();
                    }

                    user.Course = ConvertExtensions.StringToInt(courseValue);
                    await this._telegramDbContext.SaveChangesAsync();

                    var userFaculty = this._postDbContext.Faculty.FirstOrDefault(f => f.FacultyShortName == user.Faculty);
                    var groups = this._postDbContext.Group.Where(g => g.Course == user.Course && g.Faculty == userFaculty);

                    var outputSb = new StringBuilder();
                    foreach (var g in groups)
                    {
                        outputSb.AppendLine(g.GroupName);
                    }

                    // log
                    var response = new WebhookResponse();
                    response.FulfillmentText = outputSb.ToString();
                    return this.Json(response);
                }

                #endregion

                #region Group

                var existingUser = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                if (existingUser != null && existingUser.Course != null && message.Contains(existingUser.Faculty))
                {
                    var userFaculty = this._postDbContext.Faculty.FirstOrDefault(f => f.FacultyShortName == existingUser.Faculty);
                    var groups = this._postDbContext.Group.Where(g => g.Course == existingUser.Course && g.Faculty == userFaculty);

                    var outputSb = new StringBuilder();
                    foreach (var g in groups)
                    {
                        if (message.Contains(g.GroupName))
                        {
                            existingUser.Group = message;
                            await this._telegramDbContext.SaveChangesAsync();

                            outputSb.AppendLine("Ваш профіль");
                            outputSb.AppendLine($"Факультет: {existingUser.Faculty}");
                            outputSb.AppendLine($"Курс: {existingUser.Course}");
                            outputSb.AppendLine($"Група: {existingUser.Group}");

                            // log
                            var response = new WebhookResponse();
                            response.FulfillmentText = outputSb.ToString();
                            return this.Json(response);
                        }
                    }

                    // log
                    return this.Ok();
                }

                #endregion

                // log
                return this.Ok();
            }
            catch (Exception ex)
            {
                // log
                Console.WriteLine(ex.Message);
                return this.Ok();
            }
        }

        private static double GetUserIdFromButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["from"].StructValue
                .Fields["id"].NumberValue;
        }

        private static double GetUserIdFromMessage(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["from"].StructValue
                .Fields["id"].NumberValue;
        }

        private static string GetUserNameFromButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["from"].StructValue
                .Fields["username"].StringValue;
        }
    }
}
