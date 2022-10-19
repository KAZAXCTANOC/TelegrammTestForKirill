using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegrammTestForKirill
{
    class Program
    {
        static TelegramBotClient botClient = new TelegramBotClient("5692954830:AAHQ8yAf7ccKhOntIDuLd1YWAdCoIy6QusQ");
        static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: null,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Старт бота @{me.Username}");
            Console.ReadLine();


            cts.Cancel();
        }

        #region error
        private static Task HandlePollingErrorAsync(ITelegramBotClient arg1, Exception update, CancellationToken arg3)
        {
            Console.WriteLine(update.Message);

            return null;
        }
        #endregion

        private static async Task HandleUpdateAsync(ITelegramBotClient client, Update message, CancellationToken token)
        {
            switch (message.Type)
            {
                case UpdateType.Unknown:
                    Console.WriteLine("Unknown");
                    break;
                case UpdateType.Message:
                    //var stiker = message.Message.Sticker;
                    //Console.WriteLine(stiker.FileUniqueId);
                    //Console.WriteLine("Message");
                    await botClient.SendStickerAsync(message.CallbackQuery.From.Id, new InputOnlineFile("AgADqwADK-ceKQ"));
                    break;
                case UpdateType.InlineQuery:
                    Console.WriteLine("InlineQuery");
                    break;
                case UpdateType.ChosenInlineResult:
                    Console.WriteLine("ChosenInlineResult");
                    break;
                case UpdateType.CallbackQuery:
                    Console.WriteLine("CallbackQuery");
                    break;
                case UpdateType.EditedMessage:
                    Console.WriteLine("EditedMessage");
                    break;
                case UpdateType.ChannelPost:
                    Console.WriteLine("ChannelPost");
                    break;
                case UpdateType.EditedChannelPost:
                    Console.WriteLine("EditedChannelPost");
                    break;
                case UpdateType.ShippingQuery:
                    Console.WriteLine("ShippingQuery");
                    break;
                case UpdateType.PreCheckoutQuery:
                    Console.WriteLine("PreCheckoutQuery");
                    break;
                case UpdateType.Poll:
                    Console.WriteLine("Poll");
                    break;
                case UpdateType.PollAnswer:
                    Console.WriteLine("PollAnswer");
                    break;
                case UpdateType.MyChatMember:
                    Console.WriteLine("MyChatMember");
                    break;
                case UpdateType.ChatMember:
                    Console.WriteLine("ChatMember");
                    break;
                case UpdateType.ChatJoinRequest:
                    Console.WriteLine("ChatJoinRequest");
                    break;
                default:
                    Console.WriteLine("null");
                    break;
            }
            //var messageText = message.Message.Text;
            //var chatId = message.Message.From.Id;

            //Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            //await client.SendTextMessageAsync(chatId: chatId, messageText);
        }
    }
}
