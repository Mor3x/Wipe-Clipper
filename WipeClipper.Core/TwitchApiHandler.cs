using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;

namespace WipeClipperPlugin {
    internal class TwitchApiHandler {
        //private readonly TwitchAPI _api;

        public TwitchApiHandler() {
            //Log.Debug("Setting up Twitch API.");
            //TwitchAPI _api = new TwitchAPI();
            //_api.Settings.ClientId = Settings.ClientId;
            //_api.Settings.AccessToken = Settings.AccessToken;
            //_api.Settings.Secret = Settings.Secret;
        }

        public async Task<Dictionary<string, string>> MakeClip(Dictionary<string, string> channelIDs, TwitchAPI _api) {
            try {
                var clips = new Dictionary<string, string>();
                Log.Debug("MakeClip() - calling twitch api.");
                foreach (var channel in channelIDs.Keys) {
                    try {
                        Log.Debug($"Creating a clip for #{channelIDs[channel]}");
                        var clip = await _api.Helix.Clips.CreateClipAsync(channel);
                        clips.Add(channel, clip.CreatedClips[0].EditUrl.Replace("/edit", ""));
                    } catch (Exception e) {
                        Log.Error(e, $"Unable to make clip for #{channelIDs[channel]},");
                    }
                }
                Log.Debug("Done creating clip(s).");
                return clips;
            } catch (Exception e) {
                Log.Error(e, "MakeClip() - error while calling twitch api.");
                return new Dictionary<string, string>();
            }
        }

        public async Task<string> GetUserIdByName(string username, TwitchAPI _api) {
            try {
                Log.Debug("GetUserIDByName() - calling twitch api.");
                var users = await _api.V5.Users.GetUserByNameAsync(username);
                return users.Matches[0].Id; // there can only be one that matches
            } catch (Exception e) {
                Log.Error(e, "GetUserIDByName() - error while calling twitch api.");
                return null;
            }
        }
    }
}
