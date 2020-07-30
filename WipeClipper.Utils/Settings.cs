using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAndTwitch {
    public class Settings {
        //public static string Secret;
        public static string ClientId;
        public static string AccessToken;
        public static string DiscordToken;
        public static ulong ClipsChannel = 0;
        public static ulong SummariesChannel = 0;
        public static int GreenThreshold;
        public static string ChartTitle = "TEAm";

        public static List<string> Channels = new List<string>();
        public static Dictionary<string, string> UserIDs = new Dictionary<string, string>();
    }
}
