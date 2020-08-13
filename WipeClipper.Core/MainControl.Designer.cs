using System.Windows.Forms;

namespace WipeClipperPlugin {
    partial class MainControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ManualClipKeywordTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.GetCurrentZoneButton = new System.Windows.Forms.Button();
            this.GreenThresholdTextBox = new System.Windows.Forms.TextBox();
            this.ZoneTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SummariesChannelTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ClipsChannelTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AccessTokenTextBox = new System.Windows.Forms.TextBox();
            this.DiscordTokenTextBox = new System.Windows.Forms.TextBox();
            this.ClientIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.LogList = new System.Windows.Forms.ListView();
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PostSummaryButton = new System.Windows.Forms.Button();
            this.AddBreakButton = new System.Windows.Forms.Button();
            this.AutoStartCheckBox = new System.Windows.Forms.CheckBox();
            this.ChannelsListBox = new System.Windows.Forms.ListBox();
            this.ChannelTextBox = new System.Windows.Forms.TextBox();
            this.AddChannelButton = new System.Windows.Forms.Button();
            this.RemoveChannelButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ResetPullsButton = new System.Windows.Forms.Button();
            this.TeaMechanicsCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TeaMechanicsCheckBox);
            this.groupBox1.Controls.Add(this.ManualClipKeywordTextBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.GetCurrentZoneButton);
            this.groupBox1.Controls.Add(this.GreenThresholdTextBox);
            this.groupBox1.Controls.Add(this.ZoneTextBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.SummariesChannelTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ClipsChannelTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.AccessTokenTextBox);
            this.groupBox1.Controls.Add(this.DiscordTokenTextBox);
            this.groupBox1.Controls.Add(this.ClientIdTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 252);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // ManualClipKeywordTextBox
            // 
            this.ManualClipKeywordTextBox.Location = new System.Drawing.Point(151, 148);
            this.ManualClipKeywordTextBox.Name = "ManualClipKeywordTextBox";
            this.ManualClipKeywordTextBox.Size = new System.Drawing.Size(164, 20);
            this.ManualClipKeywordTextBox.TabIndex = 6;
            this.ManualClipKeywordTextBox.Text = "!clip";
            this.ManualClipKeywordTextBox.TextChanged += new System.EventHandler(this.ManualClipKeywordTextBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Clip keyword (regex)";
            // 
            // GetCurrentZoneButton
            // 
            this.GetCurrentZoneButton.Location = new System.Drawing.Point(213, 224);
            this.GetCurrentZoneButton.Name = "GetCurrentZoneButton";
            this.GetCurrentZoneButton.Size = new System.Drawing.Size(102, 22);
            this.GetCurrentZoneButton.TabIndex = 9;
            this.GetCurrentZoneButton.Text = "Get Current Zone";
            this.GetCurrentZoneButton.UseVisualStyleBackColor = true;
            this.GetCurrentZoneButton.Click += new System.EventHandler(this.GetCurrentZoneButton_Click);
            // 
            // GreenThresholdTextBox
            // 
            this.GreenThresholdTextBox.Location = new System.Drawing.Point(212, 174);
            this.GreenThresholdTextBox.Name = "GreenThresholdTextBox";
            this.GreenThresholdTextBox.Size = new System.Drawing.Size(103, 20);
            this.GreenThresholdTextBox.TabIndex = 7;
            this.GreenThresholdTextBox.TextChanged += new System.EventHandler(this.GreenThresholdTextBox_TextChanged);
            // 
            // ZoneTextBox
            // 
            this.ZoneTextBox.Location = new System.Drawing.Point(151, 200);
            this.ZoneTextBox.Name = "ZoneTextBox";
            this.ZoneTextBox.Size = new System.Drawing.Size(164, 20);
            this.ZoneTextBox.TabIndex = 8;
            this.ZoneTextBox.TextChanged += new System.EventHandler(this.ZoneTextBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 203);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Allowed zone (regex)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Summaries Channel";
            // 
            // SummariesChannelTextBox
            // 
            this.SummariesChannelTextBox.Location = new System.Drawing.Point(151, 119);
            this.SummariesChannelTextBox.Name = "SummariesChannelTextBox";
            this.SummariesChannelTextBox.Size = new System.Drawing.Size(164, 20);
            this.SummariesChannelTextBox.TabIndex = 5;
            this.SummariesChannelTextBox.TextChanged += new System.EventHandler(this.SummariesChannelTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Green message threshold (in seconds)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Clips Channel";
            // 
            // ClipsChannelTextBox
            // 
            this.ClipsChannelTextBox.Location = new System.Drawing.Point(151, 92);
            this.ClipsChannelTextBox.Name = "ClipsChannelTextBox";
            this.ClipsChannelTextBox.Size = new System.Drawing.Size(164, 20);
            this.ClipsChannelTextBox.TabIndex = 4;
            this.ClipsChannelTextBox.TextChanged += new System.EventHandler(this.ClipsChannelTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Twitch Access Token";
            // 
            // AccessTokenTextBox
            // 
            this.AccessTokenTextBox.Location = new System.Drawing.Point(151, 39);
            this.AccessTokenTextBox.Name = "AccessTokenTextBox";
            this.AccessTokenTextBox.Size = new System.Drawing.Size(164, 20);
            this.AccessTokenTextBox.TabIndex = 2;
            this.AccessTokenTextBox.UseSystemPasswordChar = true;
            this.AccessTokenTextBox.TextChanged += new System.EventHandler(this.AccessTokenTextBox_TextChanged);
            // 
            // DiscordTokenTextBox
            // 
            this.DiscordTokenTextBox.Location = new System.Drawing.Point(151, 65);
            this.DiscordTokenTextBox.Name = "DiscordTokenTextBox";
            this.DiscordTokenTextBox.Size = new System.Drawing.Size(164, 20);
            this.DiscordTokenTextBox.TabIndex = 3;
            this.DiscordTokenTextBox.UseSystemPasswordChar = true;
            this.DiscordTokenTextBox.TextChanged += new System.EventHandler(this.DiscordTokenTextBox_TextChanged);
            // 
            // ClientIdTextBox
            // 
            this.ClientIdTextBox.Location = new System.Drawing.Point(151, 13);
            this.ClientIdTextBox.Name = "ClientIdTextBox";
            this.ClientIdTextBox.Size = new System.Drawing.Size(164, 20);
            this.ClientIdTextBox.TabIndex = 1;
            this.ClientIdTextBox.UseSystemPasswordChar = true;
            this.ClientIdTextBox.TextChanged += new System.EventHandler(this.ClientIdTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Discord Token";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Twitch Client ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(339, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Channels to take clips from";
            // 
            // StartStopButton
            // 
            this.StartStopButton.Location = new System.Drawing.Point(783, 4);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(100, 23);
            this.StartStopButton.TabIndex = 14;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // LogList
            // 
            this.LogList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.LogList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.time,
            this.message});
            this.LogList.FullRowSelect = true;
            this.LogList.GridLines = true;
            this.LogList.HideSelection = false;
            this.LogList.HoverSelection = true;
            this.LogList.Location = new System.Drawing.Point(14, 262);
            this.LogList.Name = "LogList";
            this.LogList.Size = new System.Drawing.Size(869, 251);
            this.LogList.TabIndex = 18;
            this.LogList.UseCompatibleStateImageBehavior = false;
            this.LogList.View = System.Windows.Forms.View.Details;
            // 
            // time
            // 
            this.time.Text = "Time";
            this.time.Width = 160;
            // 
            // message
            // 
            this.message.Text = "Message";
            this.message.Width = 700;
            // 
            // PostSummaryButton
            // 
            this.PostSummaryButton.Location = new System.Drawing.Point(783, 62);
            this.PostSummaryButton.Name = "PostSummaryButton";
            this.PostSummaryButton.Size = new System.Drawing.Size(100, 23);
            this.PostSummaryButton.TabIndex = 16;
            this.PostSummaryButton.Text = "Post Summary";
            this.PostSummaryButton.UseVisualStyleBackColor = true;
            this.PostSummaryButton.Click += new System.EventHandler(this.PostSummaryButton_Click);
            // 
            // AddBreakButton
            // 
            this.AddBreakButton.Location = new System.Drawing.Point(783, 33);
            this.AddBreakButton.Name = "AddBreakButton";
            this.AddBreakButton.Size = new System.Drawing.Size(100, 23);
            this.AddBreakButton.TabIndex = 15;
            this.AddBreakButton.Text = "Add Break";
            this.AddBreakButton.UseVisualStyleBackColor = true;
            this.AddBreakButton.Click += new System.EventHandler(this.AddBreakButton_Click);
            // 
            // AutoStartCheckBox
            // 
            this.AutoStartCheckBox.AutoSize = true;
            this.AutoStartCheckBox.Location = new System.Drawing.Point(640, 8);
            this.AutoStartCheckBox.Name = "AutoStartCheckBox";
            this.AutoStartCheckBox.Size = new System.Drawing.Size(134, 17);
            this.AutoStartCheckBox.TabIndex = 14;
            this.AutoStartCheckBox.Text = "Auto start on ACT boot";
            this.AutoStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChannelsListBox
            // 
            this.ChannelsListBox.FormattingEnabled = true;
            this.ChannelsListBox.Location = new System.Drawing.Point(339, 36);
            this.ChannelsListBox.Name = "ChannelsListBox";
            this.ChannelsListBox.Size = new System.Drawing.Size(237, 82);
            this.ChannelsListBox.TabIndex = 10;
            // 
            // ChannelTextBox
            // 
            this.ChannelTextBox.Location = new System.Drawing.Point(339, 123);
            this.ChannelTextBox.Name = "ChannelTextBox";
            this.ChannelTextBox.Size = new System.Drawing.Size(100, 20);
            this.ChannelTextBox.TabIndex = 11;
            this.ChannelTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChannelTextBox_KeyDown);
            // 
            // AddChannelButton
            // 
            this.AddChannelButton.Location = new System.Drawing.Point(445, 122);
            this.AddChannelButton.Name = "AddChannelButton";
            this.AddChannelButton.Size = new System.Drawing.Size(53, 22);
            this.AddChannelButton.TabIndex = 12;
            this.AddChannelButton.Text = "Add";
            this.AddChannelButton.UseVisualStyleBackColor = true;
            this.AddChannelButton.Click += new System.EventHandler(this.AddChannelButton_Click);
            // 
            // RemoveChannelButton
            // 
            this.RemoveChannelButton.Location = new System.Drawing.Point(504, 122);
            this.RemoveChannelButton.Name = "RemoveChannelButton";
            this.RemoveChannelButton.Size = new System.Drawing.Size(72, 22);
            this.RemoveChannelButton.TabIndex = 13;
            this.RemoveChannelButton.Text = "Remove";
            this.RemoveChannelButton.UseVisualStyleBackColor = true;
            this.RemoveChannelButton.Click += new System.EventHandler(this.RemoveChannelButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(637, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Status:";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.ForeColor = System.Drawing.Color.Red;
            this.StatusLabel.Location = new System.Drawing.Point(673, 31);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(47, 13);
            this.StatusLabel.TabIndex = 20;
            this.StatusLabel.Text = "Stopped";
            // 
            // ResetPullsButton
            // 
            this.ResetPullsButton.Location = new System.Drawing.Point(783, 91);
            this.ResetPullsButton.Name = "ResetPullsButton";
            this.ResetPullsButton.Size = new System.Drawing.Size(100, 23);
            this.ResetPullsButton.TabIndex = 17;
            this.ResetPullsButton.Text = "Reset Pulls";
            this.ResetPullsButton.UseVisualStyleBackColor = true;
            this.ResetPullsButton.Click += new System.EventHandler(this.ResetPullsButton_Click);
            // 
            // TeaMechanicsCheckBox
            // 
            this.TeaMechanicsCheckBox.AutoSize = true;
            this.TeaMechanicsCheckBox.Location = new System.Drawing.Point(417, 228);
            this.TeaMechanicsCheckBox.Name = "TeaMechanicsCheckBox";
            this.TeaMechanicsCheckBox.Size = new System.Drawing.Size(155, 17);
            this.TeaMechanicsCheckBox.TabIndex = 16;
            this.TeaMechanicsCheckBox.Text = "Add TEA mechanics to plot";
            this.TeaMechanicsCheckBox.UseVisualStyleBackColor = true;
            this.TeaMechanicsCheckBox.CheckedChanged += new System.EventHandler(this.TeaMechanicsCheckBox_CheckedChanged);
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ResetPullsButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.RemoveChannelButton);
            this.Controls.Add(this.AddChannelButton);
            this.Controls.Add(this.ChannelTextBox);
            this.Controls.Add(this.ChannelsListBox);
            this.Controls.Add(this.AutoStartCheckBox);
            this.Controls.Add(this.AddBreakButton);
            this.Controls.Add(this.PostSummaryButton);
            this.Controls.Add(this.LogList);
            this.Controls.Add(this.StartStopButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(886, 516);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox DiscordTokenTextBox;
        private System.Windows.Forms.TextBox ClientIdTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AccessTokenTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SummariesChannelTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ClipsChannelTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button StartStopButton;
        private System.Windows.Forms.ListView LogList;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader message;
        private System.Windows.Forms.Button PostSummaryButton;
        private System.Windows.Forms.Button AddBreakButton;
        private System.Windows.Forms.TextBox GreenThresholdTextBox;
        private System.Windows.Forms.CheckBox AutoStartCheckBox;
        private System.Windows.Forms.ListBox ChannelsListBox;
        private System.Windows.Forms.TextBox ChannelTextBox;
        private System.Windows.Forms.Button AddChannelButton;
        private System.Windows.Forms.Button RemoveChannelButton;
        private Label label8;
        private Label StatusLabel;
        private Label label1;
        private Label label9;
        private Button GetCurrentZoneButton;
        private TextBox ZoneTextBox;
        private TextBox ManualClipKeywordTextBox;
        private Label label10;
        private Button ResetPullsButton;
        private CheckBox TeaMechanicsCheckBox;
    }
}
