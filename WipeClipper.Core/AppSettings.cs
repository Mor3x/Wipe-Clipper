using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WipeClipper.Core {
    class AppSettings {
        [JsonProperty("AutoStart")]
        public static bool AutoStart { get; set; }
        [JsonProperty("IncludeTimePlot")]
        public static bool IncludeTimePlot { get; set; }
    }
}
