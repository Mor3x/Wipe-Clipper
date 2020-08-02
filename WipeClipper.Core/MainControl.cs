using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Advanced_Combat_Tracker;
using DiscordAndTwitch;

namespace WipeClipperPlugin {
    public partial class MainControl : UserControl, IActPluginV1 {
        public static event EventHandler OnPostSummary;
        public static event EventHandler OnChannelsChanged;
        public static event EventHandler OnDiscordChannelsChanged;

        readonly string settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\WipeClipper.config.xml");
        SettingsSerializer xmlSettings;
        private bool _isStarted;

        public MainControl() {
            InitializeComponent();
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText) {
            pluginScreenSpace.Controls.Add(this);
            pluginStatusText.Text = "Ready.";
            pluginScreenSpace.Text = "Wipe Clipper";
            Dock = DockStyle.Fill;

            xmlSettings = new SettingsSerializer(this); // Create a new settings serializer and pass it this instance
            LoadSettings();

            Logger.Log += Log;
            MainLogic.OnStatusLabelChanged += HandleStatusChanged;
            Logger.Debug("Loaded.");

            if (AutoStartCheckBox.Checked) {
                Logger.Debug("Starting on boot.");
                MainLogic.Setup().ConfigureAwait(false);
            }
        }

        public void DeInitPlugin() {
            SaveSettings();
            MainLogic.Deinit();
            Settings.UserIDs.Clear();
            Settings.Channels.Clear();
        }

        #region Settings
        void LoadSettings() {
            xmlSettings.AddControlSetting(AccessTokenTextBox.Name, AccessTokenTextBox);
            xmlSettings.AddControlSetting(DiscordTokenTextBox.Name, DiscordTokenTextBox);
            xmlSettings.AddControlSetting(ClientIdTextBox.Name, ClientIdTextBox);
            xmlSettings.AddControlSetting(ClipsChannelTextBox.Name, ClipsChannelTextBox);
            xmlSettings.AddControlSetting(SummariesChannelTextBox.Name, SummariesChannelTextBox);
            xmlSettings.AddControlSetting(ChannelsListBox.Name, ChannelsListBox);
            xmlSettings.AddControlSetting(GreenThresholdTextBox.Name, GreenThresholdTextBox);
            xmlSettings.AddControlSetting(AutoStartCheckBox.Name, AutoStartCheckBox);
            xmlSettings.AddControlSetting(ZoneTextBox.Name, ZoneTextBox);
            xmlSettings.AddControlSetting(ManualClipKeywordTextBox.Name, ManualClipKeywordTextBox);

            if (File.Exists(settingsFile)) {
                using (var fs = new FileStream(settingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var xReader = new XmlTextReader(fs)) {
                    try {
                        while (xReader.Read()) {
                            if (xReader.NodeType == XmlNodeType.Element) {
                                if (xReader.LocalName == "SettingsSerializer") {
                                    xmlSettings.ImportFromXml(xReader);
                                }
                            }
                        }
                    } catch (Exception e) {
                        Logger.Error("Error loading settings.", e);
                    }
                }
            }

            if (ChannelsListBox.Items.Count != 0) {
                foreach (string channelName in ChannelsListBox.Items) {
                    Settings.Channels.Add(channelName);
                }
            }
        }

        void SaveSettings() {
            using (var fs = new FileStream(settingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (var xWriter = new XmlTextWriter(fs, Encoding.UTF8) { Formatting = Formatting.Indented, Indentation = 1, IndentChar = '\t' }) {
                xWriter.WriteStartDocument(true);
                xWriter.WriteStartElement("Config"); // <Config>
                xWriter.WriteStartElement("SettingsSerializer"); // <Config><SettingsSerializer>
                xmlSettings.ExportToXml(xWriter); // Fill the SettingsSerializer XML
                xWriter.WriteEndElement(); // </SettingsSerializer>
                xWriter.WriteEndElement(); // </Config>
                xWriter.WriteEndDocument(); // Tie up loose ends (shouldn't be any)
            }
        }
        #endregion

        public void Log(string text) {
            var row = new string[2];
            row[0] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            row[1] = text;
            LogList.Items.Add(new ListViewItem(row)).EnsureVisible();
        }

        private void HandleStatusChanged(object sender, StatusChangedEventArgs e) {
            _isStarted = e.Status;
            if (_isStarted) {
                StartStopButton.Text = "Stop";
                StatusLabel.ForeColor = Color.Green;
                StatusLabel.Text = "Started";
            } else {
                StartStopButton.Text = "Start";
                StatusLabel.ForeColor = Color.Red;
                StatusLabel.Text = "Stopped";
            }
        }

        private void ClientIdTextBox_TextChanged(object sender, EventArgs e) {
            Settings.ClientId = ClientIdTextBox.Text;
        }

        private void AccessTokenTextBox_TextChanged(object sender, EventArgs e) {
            Settings.AccessToken = AccessTokenTextBox.Text;
        }

        private void DiscordTokenTextBox_TextChanged(object sender, EventArgs e) {
            Settings.DiscordToken = DiscordTokenTextBox.Text;
        }

        private void ClipsChannelTextBox_TextChanged(object sender, EventArgs e) {
            if (ulong.TryParse(ClipsChannelTextBox.Text.Trim(), out var result)) {
                Settings.ClipsChannel = result;
                EventHandler handler = OnDiscordChannelsChanged;
                handler?.Invoke(this, new EventArgs());
            } else if (string.IsNullOrWhiteSpace(ClipsChannelTextBox.Text)) {
                Settings.ClipsChannel = 0;
                EventHandler handler = OnDiscordChannelsChanged;
                handler?.Invoke(this, new EventArgs());
            }
        }

        private void SummariesChannelTextBox_TextChanged(object sender, EventArgs e) {
            if (ulong.TryParse(SummariesChannelTextBox.Text.Trim(), out var result)) {
                Settings.SummariesChannel = result;
                EventHandler handler = OnDiscordChannelsChanged;
                handler?.Invoke(this, new EventArgs());
            } else if (string.IsNullOrWhiteSpace(SummariesChannelTextBox.Text)) {
                Settings.SummariesChannel = 0;
                EventHandler handler = OnDiscordChannelsChanged;
                handler?.Invoke(this, new EventArgs());
            }
        }

        private void StartStopButton_Click(object sender, EventArgs e) {
            if (!_isStarted) {
                Logger.Debug("Started");
                MainLogic.Setup().ConfigureAwait(false);
            } else {
                MainLogic.Stop();
            }
        }

        private void PostSummaryButton_Click(object sender, EventArgs e) {
            EventHandler handler = OnPostSummary;
            handler?.Invoke(this, new EventArgs());
        }

        private void AddBreakButton_Click(object sender, EventArgs e) {
            MainLogic.AddBreak();
        }

        private void GreenThresholdTextBox_TextChanged(object sender, EventArgs e) {
            if (int.TryParse(GreenThresholdTextBox.Text, out var result)) {
                Settings.GreenThreshold = result;
                Logger.Debug($"Updating green color threshold to {result} seconds.");
            }
        }

        private void AddChannelButton_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(ChannelTextBox.Text)) {
                var channelName = ChannelTextBox.Text.Trim();
                Logger.Debug($"Adding channel {channelName} to channels list.");
                ChannelsListBox.Items.Add(channelName);
                ChannelTextBox.Text = "";
                Settings.Channels.Add(channelName);
                EventHandler handler = OnChannelsChanged;
                handler?.Invoke(this, new EventArgs());
            }
        }

        private void RemoveChannelButton_Click(object sender, EventArgs e) {
            if (ChannelsListBox.SelectedItem != null) {
                if (!(ChannelsListBox.SelectedItem is string selectedChannel)) return;

                ChannelsListBox.Items.Remove(selectedChannel);
                if (Settings.Channels.Contains(selectedChannel)) {
                    Settings.Channels.Remove(selectedChannel);
                }
                EventHandler handler = OnChannelsChanged;
                handler?.Invoke(this, new EventArgs());
            }
        }

        private void ChannelTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                AddChannelButton_Click(sender, new EventArgs());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void GetCurrentZoneButton_Click(object sender, EventArgs e) {
            ZoneTextBox.Text = System.Text.RegularExpressions.Regex.Escape(ActGlobals.oFormActMain.CurrentZone);
        }

        private void ZoneTextBox_TextChanged(object sender, EventArgs e) {
            Regex.ChangeZone(ZoneTextBox.Text);
            var zoneName = string.IsNullOrWhiteSpace(ZoneTextBox.Text) ? "any" : ZoneTextBox.Text;
            Logger.Debug($"Updated zone to {zoneName}.");
        }

        private void ManualClipKeywordTextBox_TextChanged(object sender, EventArgs e) {
            Regex.ChangeManualClipKeyword(ManualClipKeywordTextBox.Text);
            var clipKeyword = string.IsNullOrWhiteSpace(ManualClipKeywordTextBox.Text) ? "any" : ManualClipKeywordTextBox.Text;
            Logger.Debug($"Updated manual keyword to {clipKeyword}.");
        }

        private void ResetPullsButton_Click(object sender, EventArgs e) {
            MainLogic.ResetPulls();
        }
    }
}
