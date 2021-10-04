using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using Newtonsoft.Json;
using WipeClipper.Core;
using WipeClipperUtils;

namespace WipeClipperPlugin {
    public partial class MainControl : UserControl, IActPluginV1 {
        private static BindingList<Preset> _presets = new BindingList<Preset>();
        private static Preset CurrentPreset = new Preset("");

        private readonly string settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\WipeClipper.config.json");
        private bool _isStarted;

        public MainControl() {
            InitializeComponent();
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText) {
            pluginScreenSpace.Controls.Add(this);
            pluginStatusText.Text = "Ready.";
            pluginScreenSpace.Text = "Wipe Clipper";
            Dock = DockStyle.Fill;
            Logger.Log += Log;

            presetsComboBox.DataSource = _presets;
            plotLinesListBox.DataSource = CurrentPreset.settings.PlotLines;
            LoadSettings();

            MainLogic.OnStatusLabelChanged += HandleStatusChanged;
            Logger.Debug("Loaded.");

            if (AutoStartCheckBox.Checked) {
                Logger.Debug("Starting on boot.");
                MainLogic.Setup(CurrentPreset).ConfigureAwait(false);
            }
        }

        public void DeInitPlugin() {
            SaveSettings();
            MainLogic.Deinit();
            Settings.UserIDs.Clear();
            CurrentPreset.settings.Channels.Clear();
        }

        public static event EventHandler OnPostSummary;
        public static event EventHandler OnChannelsChanged;
        public static event EventHandler OnDiscordChannelsChanged;

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
            CurrentPreset.settings.ClientId = ClientIdTextBox.Text;
        }

        private void AccessTokenTextBox_TextChanged(object sender, EventArgs e) {
            CurrentPreset.settings.AccessToken = AccessTokenTextBox.Text;
        }

        private void DiscordTokenTextBox_TextChanged(object sender, EventArgs e) {
            CurrentPreset.settings.DiscordToken = DiscordTokenTextBox.Text;
        }

        private void ClipsChannelTextBox_TextChanged(object sender, EventArgs e) {
            if (ulong.TryParse(ClipsChannelTextBox.Text.Trim(), out var result)) {
                CurrentPreset.settings.ClipsChannel = result;
                var handler = OnDiscordChannelsChanged;
                handler?.Invoke(this, EventArgs.Empty);
            } else if (string.IsNullOrWhiteSpace(ClipsChannelTextBox.Text)) {
                CurrentPreset.settings.ClipsChannel = 0;
                var handler = OnDiscordChannelsChanged;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        private void SummariesChannelTextBox_TextChanged(object sender, EventArgs e) {
            if (ulong.TryParse(SummariesChannelTextBox.Text.Trim(), out var result)) {
                CurrentPreset.settings.SummariesChannel = result;
                var handler = OnDiscordChannelsChanged;
                handler?.Invoke(this, EventArgs.Empty);
            } else if (string.IsNullOrWhiteSpace(SummariesChannelTextBox.Text)) {
                CurrentPreset.settings.SummariesChannel = 0;
                var handler = OnDiscordChannelsChanged;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        private void StartStopButton_Click(object sender, EventArgs e) {
            if (!_isStarted) {
                Logger.Debug("Started");
                MainLogic.Setup(CurrentPreset).ConfigureAwait(false);
            } else {
                MainLogic.Stop();
            }
        }

        private void PostSummaryButton_Click(object sender, EventArgs e) {
            var handler = OnPostSummary;
            handler?.Invoke(this, EventArgs.Empty);
        }

        private void AddBreakButton_Click(object sender, EventArgs e) {
            MainLogic.AddBreak();
        }

        private void GreenThresholdTextBox_TextChanged(object sender, EventArgs e) {
            if (int.TryParse(GreenThresholdTextBox.Text, out var result)) {
                CurrentPreset.settings.GreenThreshold = result;
                Logger.Debug($"Updating green color threshold to {result} seconds.");
            }
        }

        private void AddChannelButton_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(ChannelTextBox.Text)) {
                var channelName = ChannelTextBox.Text.Trim();
                Logger.Debug($"Adding channel {channelName} to channels list.");
                ChannelsListBox.Items.Add(channelName);
                ChannelTextBox.Text = "";
                CurrentPreset.settings.Channels.Add(channelName);
                var handler = OnChannelsChanged;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        private void RemoveChannelButton_Click(object sender, EventArgs e) {
            if (ChannelsListBox.SelectedItem != null) {
                if (!(ChannelsListBox.SelectedItem is string selectedChannel)) {
                    return;
                }

                ChannelsListBox.Items.Remove(selectedChannel);
                if (CurrentPreset.settings.Channels.Contains(selectedChannel)) {
                    CurrentPreset.settings.Channels.Remove(selectedChannel);
                }

                var handler = OnChannelsChanged;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        private void ChannelTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                AddChannelButton_Click(sender, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void GetCurrentZoneButton_Click(object sender, EventArgs e) {
            ZoneTextBox.Text = System.Text.RegularExpressions.Regex.Escape(ActGlobals.oFormActMain.CurrentZone);
        }

        private void ZoneTextBox_TextChanged(object sender, EventArgs e) {
            CurrentPreset.settings.Zone = ZoneTextBox.Text;
            Regex.ChangeZone(ZoneTextBox.Text);
            var zoneName = string.IsNullOrWhiteSpace(ZoneTextBox.Text) ? "any" : ZoneTextBox.Text;
            Logger.Debug($"Updated zone to {zoneName}.");
        }

        private void ManualClipKeywordTextBox_TextChanged(object sender, EventArgs e) {
            CurrentPreset.settings.ClipKeyword = ManualClipKeywordTextBox.Text;
            Regex.ChangeManualClipKeyword(ManualClipKeywordTextBox.Text);
            var clipKeyword = string.IsNullOrWhiteSpace(ManualClipKeywordTextBox.Text) ? "nothing" : ManualClipKeywordTextBox.Text;
            Logger.Debug($"Updated manual keyword to {clipKeyword}.");
        }

        private void ResetPullsButton_Click(object sender, EventArgs e) {
            MainLogic.ResetPulls();
        }

        private void loadPresetButton_Click(object sender, EventArgs e) {
            Logger.Debug($"Loading preset {((Preset)presetsComboBox.SelectedItem).Name}.");
            CurrentPreset.LoadPreset((Preset)presetsComboBox.SelectedItem);

            LoadFromCurrentPreset();

            Logger.Debug("Preset loaded!");
            if (_isStarted) {
                Logger.Debug("Restarting the bot...");
                MainLogic.Stop();
                MainLogic.Setup(CurrentPreset).ConfigureAwait(false);
            }
        }

        private void deletePresetButton_Click(object sender, EventArgs e) {
            if (((Preset)presetsComboBox.SelectedItem).Name == CurrentPreset.Name) {
                CurrentPreset.Name = "";
            }

            var preset = _presets.ToList().Find(item => item.Name.ToLower() == ((Preset)presetsComboBox.SelectedItem).Name.ToLower());
            _presets.Remove(preset);
            Logger.Debug("Removed preset!");
        }

        private void savePresetButton_Click(object sender, EventArgs e) {
            CurrentPreset.Name = newPresetName.Text;
            if (_presets.Count(x => x.Name == CurrentPreset.Name) > 0) {
                var preset = _presets.ToList().Find(item => item.Name.ToLower() == newPresetName.Text.ToLower());
                preset.LoadPreset(CurrentPreset);
                presetsComboBox.SelectedItem = preset;
            } else {
                var newPreset = new Preset("");
                newPreset.LoadPreset(CurrentPreset);
                _presets.Add(newPreset);
                presetsComboBox.SelectedItem = newPreset;
            }

            Logger.Debug($"Saved preset {newPresetName.Text}!");
        }

        private void includeTimePlotCheckBox_CheckedChanged(object sender, EventArgs e) {
            AppSettings.IncludeTimePlot = includeTimePlotCheckBox.Checked;
        }

        private void AutoStartCheckBox_CheckedChanged(object sender, EventArgs e) {
            AppSettings.AutoStart = AutoStartCheckBox.Checked;
        }

        private void addPlotLineButton_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(plotLineNameTextBox.Text) || string.IsNullOrEmpty(plotLineTimeTextBox.Text)) {
                return;
            }


            if (int.TryParse(plotLineTimeTextBox.Text, out var result)) {
                if (CurrentPreset.settings.PlotLines.Count(x => x.name == plotLineNameTextBox.Text || x.time == result) > 0) {
                    return;
                }

                CurrentPreset.settings.PlotLines.Add(new PlotLine { name = plotLineNameTextBox.Text, time = result });
                CurrentPreset.settings.PlotLines = new BindingList<PlotLine>(CurrentPreset.settings.PlotLines.OrderBy(x => x.time).ToList());
                plotLinesListBox.DataSource = CurrentPreset.settings.PlotLines;

                plotLineNameTextBox.Text = "";
                plotLineTimeTextBox.Text = "";
                Logger.Debug("Added new plot line.");
            }
        }

        private void removePlotLineButton_Click(object sender, EventArgs e) {
            if (plotLinesListBox.SelectedItem != null) {
                CurrentPreset.settings.PlotLines.Remove((PlotLine)plotLinesListBox.SelectedItem);
                Logger.Debug("Removed plot line.");
            }
        }

        #region Settings

        private void LoadSettings() {
            if (File.Exists(settingsFile)) {
                using (var fs = File.OpenText(settingsFile)) {
                    try {
                        var fileContent = fs.ReadToEnd();
                        var definition = new {
                            Presets = _presets,
                            Current = CurrentPreset,
                            AppSettings = new AppSettings()
                        };
                        var deserialized = JsonConvert.DeserializeAnonymousType(fileContent, definition);

                        _presets = deserialized.Presets;
                        presetsComboBox.DataSource = _presets;
                        plotLinesListBox.DataSource = CurrentPreset.settings.PlotLines;
                        CurrentPreset = deserialized.Current;

                        AutoStartCheckBox.Checked = AppSettings.AutoStart;
                        includeTimePlotCheckBox.Checked = AppSettings.IncludeTimePlot;

                        presetsComboBox.SelectedItem = _presets.ToList().Find(item => item.Name.ToLower() == CurrentPreset.Name.ToLower());
                        newPresetName.Text = CurrentPreset.Name;

                        LoadFromCurrentPreset();
                    } catch (Exception e) {
                        Logger.Error("Error loading settings.", e);
                    }
                }
            }
        }

        private void SaveSettings() {
            using (var fs = File.CreateText(settingsFile)) {
                var serializer = new JsonSerializer();
                var settings = new {
                    Presets = _presets,
                    Current = CurrentPreset,
                    AppSettings = new AppSettings()
                };
                serializer.Serialize(fs, settings);
            }
        }

        private void LoadFromCurrentPreset() {
            ClientIdTextBox.Text = CurrentPreset.settings.ClientId;
            AccessTokenTextBox.Text = CurrentPreset.settings.AccessToken;
            DiscordTokenTextBox.Text = CurrentPreset.settings.DiscordToken;
            ClipsChannelTextBox.Text = CurrentPreset.settings.ClipsChannel.ToString();
            SummariesChannelTextBox.Text = CurrentPreset.settings.SummariesChannel.ToString();
            GreenThresholdTextBox.Text = CurrentPreset.settings.GreenThreshold.ToString();
            ChannelsListBox.Items.Clear();
            CurrentPreset.settings.Channels.ForEach(x => ChannelsListBox.Items.Add(x));
            ZoneTextBox.Text = CurrentPreset.settings.Zone;
            ManualClipKeywordTextBox.Text = CurrentPreset.settings.ClipKeyword;
            newPresetName.Text = CurrentPreset.Name;
            plotLinesListBox.DataSource = CurrentPreset.settings.PlotLines;
        }

        #endregion
    }
}