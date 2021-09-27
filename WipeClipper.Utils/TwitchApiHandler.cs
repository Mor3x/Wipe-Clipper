using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;

namespace DiscordAndTwitch {
    public class TwitchApiHandler {
        private static TwitchAPI _api;

        public static void Setup(Preset preset) {
            Logger.Debug("Setting up Twitch API.");
            var dummy = typeof(TwitchLib.Api.Core.ApiBase); // otherwise there's an exception that core doesn't load lmao
            Console.WriteLine(dummy.FullName);

            _api = new TwitchAPI();
            _api.Settings.ClientId = preset.settings.ClientId;
            _api.Settings.AccessToken = preset.settings.AccessToken;
        }

        public static async Task<Dictionary<string, string>> MakeClip(Dictionary<string, string> channelIDs) {
            try {
                var clips = new Dictionary<string, string>();
                foreach (var channelPair in channelIDs) {
                    try {
                        Logger.Debug($"Creating a clip for #{channelPair.Key}.");
                        var clip = await _api.Helix.Clips.CreateClipAsync(channelPair.Value);
                        clips.Add(channelPair.Value, clip.CreatedClips[0].EditUrl.Replace("/edit", ""));
                    } catch (Exception e) {
                        Logger.Error($"Unable to make clip for #{channelPair.Key}.", e);
                    }
                }
                Logger.Debug("Done creating clip(s).");
                return clips;
            } catch (Exception e) {
                Logger.Error("Error while creating clips.", e);
                return new Dictionary<string, string>();
            }
        }

        public static async Task<string> GetUserIdByName(string username) {
            try {
                Logger.Debug($"Fetching userID for {username}.");
                var users = await _api.V5.Users.GetUserByNameAsync(username);
                return users.Matches[0].Id; // there can only be one that matches
            } catch (Exception e) {
                Logger.Error("Error while fetching userID.", e);
                return null;
            }
        }
    }
}
