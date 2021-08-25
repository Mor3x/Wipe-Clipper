using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advanced_Combat_Tracker;
using DiscordAndTwitch;

namespace WipeClipperPlugin {
    class MainLogic {
        private static bool _isStarted;
        private static int _wipes = 0;
        private static TimeSpan _pullTime;
        private static bool _isPulled;
        private static readonly List<int> _pullTimes = new List<int>();
        private static readonly List<double> _breaks = new List<double>();

        public delegate void LabelChangedEventHandler(object sender, StatusChangedEventArgs args);
        public static event LabelChangedEventHandler OnStatusLabelChanged;

        public static async Task Setup() {
            while (string.IsNullOrWhiteSpace(Settings.AccessToken) || string.IsNullOrWhiteSpace(Settings.ClientId)
                || string.IsNullOrWhiteSpace(Settings.DiscordToken)) {
                Logger.Error("Please provide the Client ID, Access Token and the Discord Token and start again.");
                return;
            }

            await Task.Factory.StartNew(TwitchApiHandler.Setup);

            foreach (var streamName in Settings.Channels) {
                var id = await TwitchApiHandler.GetUserIdByName(streamName);
                if (!Settings.UserIDs.ContainsKey(streamName)) {
                    Settings.UserIDs.Add(streamName, id);
                }
            }

            await Task.Factory.StartNew(() => Discord.SetupBot(Settings.ClipsChannel, Settings.SummariesChannel));

            ActGlobals.oFormActMain.OnLogLineRead += OnLogLineRead;
            MainControl.OnPostSummary += HandlePostSummary;
            MainControl.OnChannelsChanged += HandleChannelsChanged;
            MainControl.OnDiscordChannelsChanged += HandleDiscordChannelsChanged;

            _isStarted = true;
            ChangeStatusLabel(true);
        }

        private static void OnLogLineRead(bool isImport, LogLineEventArgs logInfo) {
            if (Regex.IsCorrectZone(ActGlobals.oFormActMain.CurrentZone)) {
                if (Regex.IsPull(logInfo.logLine)) {
                    HandlePulled();
                }

                if (Regex.IsWipe(logInfo.logLine)) {
                    HandleWiped();
                }

                if (Regex.IsManualClip(logInfo.logLine)) {
                    HandleManualClip();
                }
            }
        }

        public static void HandlePulled() {
            _pullTime = DateTime.Now.TimeOfDay;
            _isPulled = true;
        }

        public static async void HandleWiped() {
            if (_isPulled) {
                Logger.Debug("Clipping and sending message.");
                _isPulled = false;
                var clips = await TwitchApiHandler.MakeClip(Settings.UserIDs);
                string message = "";

                foreach (var userIdPair in Settings.UserIDs) {
                    try {
                        Logger.Debug($"Adding clip from {userIdPair.Key} to message.");
                        message += $"{userIdPair.Key}'s POV: {clips[userIdPair.Value]}\n";
                    } catch (Exception ex) {
                        Logger.Error($"Unable to add clip from #{userIdPair.Key}.", ex);
                    }
                }

                var totalPullTime = DateTime.Now.TimeOfDay - _pullTime;

                var isGreen = totalPullTime.TotalSeconds > Settings.GreenThreshold;
                _pullTimes.Add((int)totalPullTime.TotalSeconds);

                await Discord.SendMessage(message + "\n", $"Wipe #{++_wipes} - {totalPullTime:mm\\:ss}min", isGreen);
            }
        }

        public static async void HandleManualClip() {
            Logger.Debug("Creating clip manually.");

            var clips = await TwitchApiHandler.MakeClip(Settings.UserIDs);
            string message = "";

            foreach (var userIdPair in Settings.UserIDs) {
                try {
                    Logger.Debug($"Adding clip from {userIdPair.Key} to message.");
                    message += $"{userIdPair.Key}'s POV: {clips[userIdPair.Value]}\n";
                } catch (Exception ex) {
                    Logger.Error($"Unable to add clip from #{userIdPair.Key}.", ex);
                }
            }

            await Discord.SendMessage(message + "\n", "Manual clip", null);
        }

        public static async void HandlePostSummary(object o, EventArgs e) {
            if (_pullTimes.Count == 0) {
                Logger.Debug("Summary cannot be posted when there's 0 pulls.");
                return;
            }

            Logger.Debug("Posting summary.");
            Stats.SavePlot(_pullTimes, _breaks);
            var result = Stats.GetStats(_pullTimes);
            await Discord.SendSummary(result);
        }

        public static async void HandleChannelsChanged(object o, EventArgs e) {
            foreach (var streamName in Settings.Channels.Where(streamName => !Settings.UserIDs.ContainsKey(streamName))) {
                var id = await TwitchApiHandler.GetUserIdByName(streamName);
                if (id is null) {
                    Logger.Error($"Channel {streamName} either does not exist or there has been an error with the API call. Please check the spelling and try again.");
                    return;
                }
                Settings.UserIDs.Add(streamName, id);
                Logger.Debug($"Adding {streamName}:{id} to channels list.");
            }

            var removedStreamNames = Settings.UserIDs.Keys.Where(streamName => !Settings.Channels.Contains(streamName)).ToList();
            foreach (var streamName in removedStreamNames) {
                Settings.UserIDs.Remove(streamName);
                Logger.Debug($"Removing {streamName} from channels list.");
            }
        }
        
        public static async void HandleDiscordChannelsChanged(object o, EventArgs e) {
            await Discord.UpdateChannels();
        }

        public static void AddBreak() {
            _breaks.Add(_wipes + 0.5);
            Logger.Debug($"Adding break at {_wipes + 0.5}.");
        }

        public static void Stop() {
            if (!_isStarted) return;

            Logger.Debug("Stopping.");
            Discord.Disconnect();
            Settings.UserIDs.Clear();
            MainControl.OnPostSummary -= HandlePostSummary;
            MainControl.OnChannelsChanged -= HandleChannelsChanged;
            MainControl.OnDiscordChannelsChanged -= HandleDiscordChannelsChanged;
            ActGlobals.oFormActMain.OnLogLineRead -= OnLogLineRead;
            _isStarted = false;
            ChangeStatusLabel(false);
        }

        public static void ChangeStatusLabel(bool working) {
            if (working) {
                LabelChangedEventHandler handler = OnStatusLabelChanged;
                handler?.Invoke(null, new StatusChangedEventArgs(true));
            } else {
                LabelChangedEventHandler handler = OnStatusLabelChanged;
                handler?.Invoke(null, new StatusChangedEventArgs(false));
            }
        }

        public static void Deinit() {
            Stop();
            ActGlobals.oFormActMain.OnLogLineRead -= OnLogLineRead;
        }

        public static void ResetPulls() {
            Logger.Debug("Resetting pulls and breaks.");
            _pullTimes.Clear();
            _breaks.Clear();
            _wipes = 0;
        }
    }
}
