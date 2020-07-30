using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
