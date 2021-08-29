using System;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace DiscordAndTwitch {
    public class Discord {
        private static DiscordClient Bot;
        private static DiscordChannel ClipChannel;
        private static DiscordChannel SummaryChannel;

        public static async Task SetupBot(ulong clipChannel, ulong summaryChannel) {
            try {
                SetupBotCredentials();
                await Connect();
                Logger.Debug("Discord connected.");

                if (clipChannel == 0) {
                    Logger.Error("Please enter a clip channel ID.");
                    return;
                }
                ClipChannel = await Bot.GetChannelAsync(clipChannel);

                if (summaryChannel == 0) {
                    return;
                }
                SummaryChannel = await Bot.GetChannelAsync(summaryChannel);
            } catch (Exception e) {
                Logger.Error("Error on Discord initialization.", e);
            }
        }

        private static void SetupBotCredentials() {
            Bot = new DiscordClient(new DiscordConfiguration {
                AutoReconnect = true,
                Token = Settings.DiscordToken,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = false,
            });
        }

        private static async Task Connect() {
            await Bot.ConnectAsync();
        }

        public static async Task SendMessage(string message, string title, bool? isGreen) {
            try {
                Logger.Debug($"Sending message - {message}");
                var time = DateTime.Now.TimeOfDay;

                var embed = new DiscordEmbedBuilder();
                embed.WithAuthor(title);
                embed.WithDescription(message.Replace("_", "\\_"));
                embed.WithTimestamp(DateTime.Today + time);

                if (!isGreen.HasValue) {
                    embed.WithColor(DiscordColor.White);
                } else if (isGreen.Value) {
                    embed.WithColor(DiscordColor.Green);
                } else {
                    embed.WithColor(DiscordColor.Red);
                }

                embed.Build();
                await Bot.SendMessageAsync(ClipChannel, null, false, embed);
            } catch (Exception e) {
                Logger.Error("Error while posting a clip to Discord. Please make sure the clips channel ID is correct.", e);
            }
        }

        public static async Task SendSummary(Stats.Statistics stats) {
            try {
                Logger.Debug("Sending stats.");

                var time = DateTime.Now.TimeOfDay;

                var embed = new DiscordEmbedBuilder();
                embed.WithAuthor($"Summary for {DateTime.Now.Date.ToShortDateString()}");
                embed.AddField("Number of pulls", $"{stats.PullCount}");
                embed.AddField("Median pull", $"{stats.MedianPull}s");
                embed.AddField("Longest pull", $"{stats.LongestPull}s");
                embed.AddField("Time % spent on pulls", $"{Math.Round(stats.PercentageSpentOnPulls, 2)}%");
                embed.AddField("Time spent on pulls", $"{new TimeSpan(0, 0,stats.TimeSpentPulling):h\\:mm\\:ss}");
                embed.AddField($"Time spent on pulls past {Settings.GreenThreshold}s", $"{new TimeSpan(0, 0,stats.TimeSpentPullingPastThreshold):h\\:mm\\:ss}");
                embed.WithColor(DiscordColor.White);
                embed.WithTimestamp(DateTime.Today + time);

                embed.Build();

                await Bot.SendMessageAsync(SummaryChannel, null, false, embed);
                await SummaryChannel.SendFileAsync("simplePlot.png");
                await SummaryChannel.SendFileAsync("timePlot.png");
                File.Delete("simplePlot.png");
                File.Delete("timePlot.png");
                Logger.Debug("Stats sent.");
            } catch (Exception e) {
                Logger.Error("Error while sending summary to Discord. Please make sure the summary channel ID is correct.", e);
            }
        }

        public static async Task UpdateChannels() {
            Logger.Debug("Updating discord channels.");
            try {
                if (Settings.ClipsChannel != 0) {
                    ClipChannel = await Bot.GetChannelAsync(Settings.ClipsChannel);
                }

                if (Settings.SummariesChannel != 0) {
                    SummaryChannel = await Bot.GetChannelAsync(Settings.SummariesChannel);
                }
            } catch (Exception e) {
                Logger.Error("Error while updating Discord channels. Please make sure the ID's are correct.", e);
            }
        }

        public static void Disconnect() {
            Logger.Debug("Disconnecting from Discord.");
            Bot?.DisconnectAsync().ConfigureAwait(false);
        }
    }
}
