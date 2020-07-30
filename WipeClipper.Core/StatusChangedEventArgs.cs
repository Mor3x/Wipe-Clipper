using System;

namespace WipeClipperPlugin {
    public class StatusChangedEventArgs : EventArgs {
        public StatusChangedEventArgs(string text, bool status) {
            Text = text;
            Status = status;
        }

        public string Text { get; }
        public bool Status { get; }
    }
}
