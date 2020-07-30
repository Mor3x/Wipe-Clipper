using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WipeClipperPlugin {
    class Settings {
        public static string Secret;
        public static string ClientId;
        public static string AccessToken;
        public static string DiscordToken;
        public static uint ClipsChannel;
        public static uint SummariesChannel;
        public static string ChartTitle = "TEAm";

        public static Dictionary<string, string> userIDs = new Dictionary<string, string>();
    }
}
