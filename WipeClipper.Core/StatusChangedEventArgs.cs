using System;

namespace WipeClipperPlugin {
    public class StatusChangedEventArgs : EventArgs {
        public StatusChangedEventArgs(bool status) {
            Status = status;
        }

        public bool Status { get; }
    }
}
