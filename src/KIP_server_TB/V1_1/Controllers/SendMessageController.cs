using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KIP_Backend.Attributes;
using KIP_server_TB.Constants;
using KIP_server_TB.DB;
using KIP_server_TB.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace KIP_server_TB.V1_1.Controllers
{
    /// <summary>
    /// Send direct messages controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1_1]
    [ApiRoute]
    [ApiController]
    public class SendMessageController : Controller
    {
        private readonly ILogger<SendMessageController> _logger;
        private readonly TelegramApiDbContext _dbContext;
        private readonly ITelegramBotClient _telegramBotClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The db context.</param>
        /// <param name="telegramBotClient">The telegramBotClient.</param>
        public SendMessageController(ILogger<SendMessageController> logger, TelegramApiDbContext dbContext, ITelegramBotClient telegramBotClient)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
        }

        /// <summary>
        /// Current rank of student's group.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>OkResult.</returns>
        /// <remarks>
        /// Controller returns OkResult result always. It is needed to perform operations and log error by logger.
        /// Don't break Dialogflow.
        /// </remarks>
        [HttpPost]
        [Route("send/{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OkResult> SendMessageAsync(int userId)
        {
            using var sr = new StreamReader(this.Request.Body);
            var message = await sr.ReadToEndAsync();
            if (string.IsNullOrEmpty(message))
            {
                return this.Ok();
            }

            try
            {
                var user = await this._dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                if (user == null)
                {
                    this._logger.LogUserNotFound(ActionNames.SendMessage, message, userId);
                    return this.Ok();
                }

                try
                {
                    await this._telegramBotClient.SendTextMessageAsync(user.UserId, message);
                }
                catch
                {
                    // TODO delete user if bot was blocked
                    // ex.Message.Contains("bot was blocked by the user")
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogSendMessageUnexpectedError(ActionNames.SendMessage, message, ex);
                return this.Ok();
            }
        }

        /// <summary>
        /// Current rank of student's group.
        /// </summary>
        /// <returns>OkResult.</returns>
        /// <remarks>
        /// Controller returns OkResult result always. It is needed to perform operations and log error by logger.
        /// Don't break Dialogflow.
        /// </remarks>
        [HttpPost]
        [Route("send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OkResult> SendMessageToAllUsersAsync()
        {
            using var sr = new StreamReader(this.Request.Body);
            var message = await sr.ReadToEndAsync();
            if (string.IsNullOrEmpty(message))
            {
                return this.Ok();
            }

            try
            {
                var users = this._dbContext.Users.AsNoTracking().ToList();
                foreach (var user in users)
                {
                    try
                    {
                        await this._telegramBotClient.SendTextMessageAsync(user.UserId, message);
                    }
                    catch
                    {
                        // TODO delete user if bot was blocked
                        // ex.Message.Contains("bot was blocked by the user")
                    }
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogSendMessageUnexpectedError(ActionNames.SendMessage, message, ex);
                return this.Ok();
            }
        }
    }
}
