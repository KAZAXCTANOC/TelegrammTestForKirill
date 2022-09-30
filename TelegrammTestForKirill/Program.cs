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
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegrammTestForKirill
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("5692954830:AAHQ8yAf7ccKhOntIDuLd1YWAdCoIy6QusQ");

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
        //"Новости отдела", "Личная новость", "Новости отрасли", "Новость внешнего мира", "Новости компании", "Другое"
        public static async Task ChooseCategory(ITelegramBotClient client, Update message)
        {
            var chatId = message.Message.From.Id;
            Message sentMessage = await client.SendTextMessageAsync(
                       chatId: chatId,
                       "Кнопки",
                       replyMarkup: new ReplyKeyboardMarkup(new[] {
                        new KeyboardButton("Новости отдела"),
                        new KeyboardButton("Личная новость"),
                        new KeyboardButton("Новости отрасли"),
                        new KeyboardButton("Новость внешнего мира"),
                        new KeyboardButton("Новости компании"),
                        new KeyboardButton("Другое"),
                        new KeyboardButton("Отмена")
                       }));
        }
        public static async Task CreateMenuForSentenceAsync(ITelegramBotClient client, Update message)
        {
            var chatId = message.Message.From.Id;
            Message sentMessage = await client.SendTextMessageAsync(
                       chatId: chatId,
                       "Кнопки",
                       replyMarkup: new ReplyKeyboardMarkup(new[] {
                        new KeyboardButton("Отправить новость"),
                        new KeyboardButton("Отмена"),
                       }));
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient client, Update message, CancellationToken token)
        {
            var messageText = message.Message.Text;
            var chatId = message.Message.From.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            switch (messageText)
            {
                case "Start":
                    {
                        await CreateMenuForSentenceAsync(client, message);
                        break;
                    }
                
                case "Предложить новость":
                    {
                        await CreateMenuForSentenceAsync(client, message);

                        break;
                    }

                case "Отправить новость":
                    {
                        await ChooseCategory(client, message);
                        break;
                    }

                case "Новости отдела":
                    {
                        break;
                    }
                case "2":
                    {
                        break;
                    }
                case "3":
                    {
                        break;
                    }
                case "4":
                    {
                        break;
                    }
            }

            if (messageText == "Start")
            {
                Message sentMessage = await client.SendTextMessageAsync(
                       chatId: chatId,
                       "Кнопки",
                       replyMarkup: new ReplyKeyboardMarkup(new[] {
                        new KeyboardButton("Отправить новость"),
                        new KeyboardButton("Отмена"),
                       }));
            }
        }
    }
}
