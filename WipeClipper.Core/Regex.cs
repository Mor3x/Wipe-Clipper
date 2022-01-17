using System.Text.RegularExpressions;

namespace WipeClipperPlugin {
    class Regex {
        private static readonly System.Text.RegularExpressions.Regex _wipe = new System.Text.RegularExpressions.Regex(@"(21:([0-9,a-f,A-F]{8}):40000010)", RegexOptions.Compiled);
        private static readonly System.Text.RegularExpressions.Regex _pull = new System.Text.RegularExpressions.Regex(@"(0039::Engage)|(0039::Start)|(0039::À l'attaque)|(0039::戦闘開始)", RegexOptions.Compiled);
        private static System.Text.RegularExpressions.Regex _zone = new System.Text.RegularExpressions.Regex(@"");
        private static System.Text.RegularExpressions.Regex _manualClip = new System.Text.RegularExpressions.Regex(@"!clip", RegexOptions.Compiled);

        public static bool IsWipe(string text) => _wipe.IsMatch(text);
        public static bool IsPull(string text) => _pull.IsMatch(text);
        public static bool IsCorrectZone(string text) => _zone.IsMatch(text);
        public static bool IsManualClip(string text) => _manualClip.IsMatch(text);

        public static void ChangeZone(string text) {
            _zone = new System.Text.RegularExpressions.Regex(text);
        }
        
        public static void ChangeManualClipKeyword(string text) {
            if (string.IsNullOrWhiteSpace(text)) { // in case someone wants to remove the manual clip functionality, make it a never-matching regex
                _manualClip = new System.Text.RegularExpressions.Regex("(?!x)x");
            } else {
                _manualClip = new System.Text.RegularExpressions.Regex(text);
            }
        }
    }
}
