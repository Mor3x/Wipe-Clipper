using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WipeClipperUtils {
    public class Preset {
        public string Name { get; set; }
        public Settings settings { get; set; }

        public Preset(string name) {
            Name = name;
            settings = new Settings();
        }

        public void LoadPreset(Preset otherPreset) {
            Name = otherPreset.Name;
            settings.AccessToken = otherPreset.settings.AccessToken;
            settings.AddTeaMarkers = otherPreset.settings.AddTeaMarkers;
            settings.ClientId = otherPreset.settings.ClientId;
            settings.ClipsChannel = otherPreset.settings.ClipsChannel;
            settings.DiscordToken = otherPreset.settings.DiscordToken;
            settings.SummariesChannel = otherPreset.settings.SummariesChannel;
            settings.GreenThreshold = otherPreset.settings.GreenThreshold;
            settings.Zone = otherPreset.settings.Zone;
            settings.ClipKeyword = otherPreset.settings.ClipKeyword;


            var channelsCopy = new List<string>();
            otherPreset.settings.Channels.ForEach(channel => channelsCopy.Add(channel));
            settings.Channels = channelsCopy;

            var plotLinesCopy = new BindingList<PlotLine>();
            otherPreset.settings.PlotLines.ToList().ForEach(line => plotLinesCopy.Add(line));
            settings.PlotLines = plotLinesCopy;

        }

        public override string ToString() {
            return Name;
        }
    }
}