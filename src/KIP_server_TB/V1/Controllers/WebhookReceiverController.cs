// <copyright file="WebhookReceiverController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.IO;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using KIP_server_TB.Attributes;
using KIP_server_TB.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace KIP_server_TB.V1.Controllers
{
    /// <summary>
    /// Current Rank controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class WebhookReceiverController : Controller
    {
        private readonly ILogger<WebhookReceiverController> logger;
        private readonly TelegramDbContext context;

        private readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookReceiverController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">context.</param>
        public WebhookReceiverController(
            ILogger<WebhookReceiverController> logger,
            TelegramDbContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Current rank of student's group.
        /// </summary>
        /// <returns>Start message.</returns>
        [HttpPost]
        [Route("receive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public OkResult Receive()
        {
            WebhookRequest request;
            using (var reader = new StreamReader(this.Request.Body))
            {
                request = this.jsonParser.Parse<WebhookRequest>(reader);
            }

            Console.WriteLine(request.QueryResult.QueryText + " " +
                request.OriginalDetectIntentRequest.Payload.Fields["data"].StructValue.Fields["callback_query"].StructValue.Fields["from"].StructValue.Fields["username"].StringValue);

            /*
            var response = new WebhookResponse();

            response.FulfillmentText = request.QueryResult.QueryText + " " +
                request.OriginalDetectIntentRequest.Payload.Fields["data"].StructValue.Fields["callback_query"].StructValue.Fields["from"].StructValue.Fields["username"].StringValue;

            response.FulfillmentMessages[0].Text = request.QueryResult.QueryText + " " +
                request.OriginalDetectIntentRequest.Payload.Fields["data"].StructValue.Fields["callback_query"].StructValue.Fields["from"].StructValue.Fields["username"].StringValue;

            return this.Json(response);*/
            return this.Ok();
        }
    }
}
