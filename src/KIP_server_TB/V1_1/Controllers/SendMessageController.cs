using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KIP_Backend.Attributes;
using KIP_server_TB.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace KIP_server_TB.V1_1.Controllers
{
    /// <summary>
    /// Webhook controller.
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
        public SendMessageController(
            ILogger<SendMessageController> logger, TelegramApiDbContext dbContext, ITelegramBotClient telegramBotClient)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
        }

        /// <summary>
        /// Current rank of student's group.
        /// </summary>
        /// <returns>Start message.</returns>
        [HttpPost]
        [Route("send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OkResult> SendMessageToAllUsersAsync()
        {
            try
            {
                using var sr = new StreamReader(this.Request.Body);
                var message = await sr.ReadToEndAsync();
                if (string.IsNullOrEmpty(message))
                {
                    // TODO log
                    return this.Ok();
                }

                // var users = this._dbContext.Users.Where(x => x.UserId == 414372921).AsNoTracking().ToList();
                var users = this._dbContext.Users.AsNoTracking().ToList();
                foreach (var user in users)
                {
                    try
                    {
                        await this._telegramBotClient.SendTextMessageAsync(user.UserId, message);
                    }
                    catch (Exception ex)
                    {
                        // TODO properly work
                        if (ex.Message.Contains("bot was blocked by the user"))
                        {
                            // this._dbContext.Users.Remove(user);
                            // await this._dbContext.SaveChangesAsync();
                        }

                        // TODO log
                        Console.WriteLine($"{ex.Message} -> userId {user.UserId} userName {user.UserName}");
                    }
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                // TODO log
                Console.WriteLine(ex.Message);
                return this.Ok();
            }
        }
    }
}
