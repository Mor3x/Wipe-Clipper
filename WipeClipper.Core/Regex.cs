using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WipeClipperPlugin {
    class Regex {
        private static readonly System.Text.RegularExpressions.Regex _wipe = new System.Text.RegularExpressions.Regex(@"(21:([0-9,a-f,A-F]{8}):40000010)", RegexOptions.Compiled);
        private static readonly System.Text.RegularExpressions.Regex _pull = new System.Text.RegularExpressions.Regex(@"0039:Engage", RegexOptions.Compiled);
        private static System.Text.RegularExpressions.Regex _zone = new System.Text.RegularExpressions.Regex(@"", RegexOptions.Compiled);
        private static System.Text.RegularExpressions.Regex _manualClip = new System.Text.RegularExpressions.Regex(@"!clip", RegexOptions.Compiled);

        public static bool IsWipe(string text) => _wipe.IsMatch(text);
        public static bool IsPull(string text) => _pull.IsMatch(text);
        public static bool IsCorrectZone(string text) => _zone.IsMatch(text);
        public static bool IsManualClip(string text) => _manualClip.IsMatch(text);

        public static void ChangeZone(string text) {
            _zone = new System.Text.RegularExpressions.Regex(text);
        }
        
        public static void ChangeManualClipKeyword(string text) {
            _manualClip = new System.Text.RegularExpressions.Regex(text);
        }
    }
}
