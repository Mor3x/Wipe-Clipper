using System.Collections.Generic;
using System.ComponentModel;

namespace WipeClipperUtils {
    public class Settings {
        public string ClientId;
        public string AccessToken;
        public string DiscordToken;
        public ulong ClipsChannel = 0;
        public ulong SummariesChannel = 0;
        public int GreenThreshold;
        public string Zone = "";
        public string ClipKeyword = "!clip";

        public BindingList<PlotLine> PlotLines = new BindingList<PlotLine>();
        public List<string> Channels = new List<string>();
        public static Dictionary<string, string> UserIDs = new Dictionary<string, string>();
    }

    public struct PlotLine {
        public string name;
        public int time;

        public override string ToString() => $"{name}: {time}";
    }
}