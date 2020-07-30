//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DSharpPlus;
//using DSharpPlus.Entities;

//namespace WipeClipperPlugin {
//    class Discord {
//        private static DiscordClient Bot;
//        private static DiscordChannel ClipChannel;
//        private static DiscordChannel SummaryChannel;

//        public static async Task SetupBot(ulong clipChannel, ulong summaryChannel) {
//            try {
//                SetupBotCredentials();
//                await Connect();
//                Log.Debug("Discord connected.");
//                ClipChannel = await Bot.GetChannelAsync(clipChannel);
//                SummaryChannel = await Bot.GetChannelAsync(summaryChannel);

//                await Task.Delay(-1);
//            } catch (Exception e) {
//                Log.Error(e, "Error on discord initialization -> SetupBot()");
//            }
//        }

//        private static void SetupBotCredentials() {
//            Bot = new DiscordClient(new DiscordConfiguration {
//                AutoReconnect = true,
//                Token = Settings.DiscordToken,
//                TokenType = TokenType.Bot,
//                UseInternalLogHandler = false,
//            });
//        }

//        private static async Task Connect() {
//            await Bot.ConnectAsync();
//            await Task.Delay(5000);
//        }

//        public static async Task SendMessage(string message, string title, DiscordColor color) {
//            Log.Debug($"Sending message - {message}");


//            var time = DateTime.Now.TimeOfDay; //.Subtract(new TimeSpan(0, 2, 0, 0));

//            var embed = new DiscordEmbedBuilder();
//            embed.WithAuthor(title);
//            embed.WithDescription(message);
//            embed.WithColor(color);
//            embed.WithTimestamp(DateTime.Today + time);

//            embed.Build();

//            await Bot.SendMessageAsync(ClipChannel, null, false, embed);
//        }

//        public static async Task SendSummary((int count, int median, int mean, int longest) stats) {
//            Log.Debug($"Sending stats.");

//            var time = DateTime.Now.TimeOfDay;

//            var embed = new DiscordEmbedBuilder();
//            embed.WithAuthor($"Summary for {DateTime.Now.Date.ToShortDateString()}");
//            embed.AddField($"Number of pulls", $"{stats.count}");
//            embed.AddField($"Median pull", $"{stats.median}s");
//            //embed.AddField($"Mean pull", $"{stats.mean}s");
//            embed.AddField($"Longest pull", $"{stats.longest}s");
//            embed.WithColor(DiscordColor.White);
//            embed.WithTimestamp(DateTime.Today + time);

//            embed.Build();

//            await Bot.SendMessageAsync(SummaryChannel, null, false, embed);
//            await SummaryChannel.SendFileAsync("plot.png");
//            Console.WriteLine("Stats sent.");
//        }
//    }
//}
