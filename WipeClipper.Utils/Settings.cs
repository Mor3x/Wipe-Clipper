﻿using System.Collections.Generic;

namespace WipeClipperUtils {
    public class Settings {
        public string ClientId;
        public string AccessToken;
        public string DiscordToken;
        public ulong ClipsChannel = 0;
        public ulong SummariesChannel = 0;
        public int GreenThreshold;
        public bool AddTeaMarkers;
        public string Zone = "";
        public string ClipKeyword = "!clip";

        public List<string> Channels = new List<string>();
        public static Dictionary<string, string> UserIDs = new Dictionary<string, string>();
    }
}
