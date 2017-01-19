using System;
using System.Windows.Forms;

namespace ReynTime
{
    public partial class ReynTime
    {
        CheckBox chkEnabled;
        CheckBox chkIgnoreImport;
        GroupBox volumeGroup;
        TrackBar soundVolumeSlider;
        Label soundVolumeLabel;
        TrackBar musicVolumeSlider;
        Label musicVolumeLabel;
        Label musicVolumeLevelLabel;
        Label soundVolumeLevelLabel;
        Button testMusic;
        Button testSound;
        TextBox musicDirectory;
        TextBox soundDirectory;
        Label musicDirectoryLabel;
        Label soundDirectoryLabel;

        #region Designer Code

        private void InitializeComponent()
        {
            this.chkIgnoreImport = new System.Windows.Forms.CheckBox();
            this.soundVolumeSlider = new System.Windows.Forms.TrackBar();
            this.volumeGroup = new System.Windows.Forms.GroupBox();
            this.musicDirectory = new System.Windows.Forms.TextBox();
            this.soundDirectory = new System.Windows.Forms.TextBox();
            this.musicDirectoryLabel = new System.Windows.Forms.Label();
            this.testMusic = new System.Windows.Forms.Button();
            this.soundDirectoryLabel = new System.Windows.Forms.Label();
            this.testSound = new System.Windows.Forms.Button();
            this.musicVolumeLevelLabel = new System.Windows.Forms.Label();
            this.soundVolumeLevelLabel = new System.Windows.Forms.Label();
            this.musicVolumeLabel = new System.Windows.Forms.Label();
            this.soundVolumeLabel = new System.Windows.Forms.Label();
            this.musicVolumeSlider = new System.Windows.Forms.TrackBar();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.soundVolumeSlider)).BeginInit();
            this.volumeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musicVolumeSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // chkIgnoreImport
            // 
            this.chkIgnoreImport.AutoSize = true;
            this.chkIgnoreImport.BackColor = System.Drawing.SystemColors.Control;
            this.chkIgnoreImport.Checked = true;
            this.chkIgnoreImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreImport.Location = new System.Drawing.Point(14, 40);
            this.chkIgnoreImport.Name = "chkIgnoreImport";
            this.chkIgnoreImport.Size = new System.Drawing.Size(126, 17);
            this.chkIgnoreImport.TabIndex = 1;
            this.chkIgnoreImport.Text = "Ignore Imported Data";
            this.chkIgnoreImport.UseVisualStyleBackColor = false;
            this.chkIgnoreImport.CheckedChanged += new System.EventHandler(this.chkIgnoreImport_CheckedChanged);
            // 
            // soundVolumeSlider
            // 
            this.soundVolumeSlider.LargeChange = 10;
            this.soundVolumeSlider.Location = new System.Drawing.Point(53, 19);
            this.soundVolumeSlider.Maximum = 100;
            this.soundVolumeSlider.Name = "soundVolumeSlider";
            this.soundVolumeSlider.Size = new System.Drawing.Size(104, 45);
            this.soundVolumeSlider.TabIndex = 2;
            this.soundVolumeSlider.TickFrequency = 10;
            this.soundVolumeSlider.Value = 50;
            this.soundVolumeSlider.Scroll += new System.EventHandler(this.soundVolumeSlider_Scroll);
            // 
            // volumeGroup
            // 
            this.volumeGroup.Controls.Add(this.musicDirectory);
            this.volumeGroup.Controls.Add(this.soundDirectory);
            this.volumeGroup.Controls.Add(this.musicDirectoryLabel);
            this.volumeGroup.Controls.Add(this.testMusic);
            this.volumeGroup.Controls.Add(this.soundDirectoryLabel);
            this.volumeGroup.Controls.Add(this.testSound);
            this.volumeGroup.Controls.Add(this.musicVolumeLevelLabel);
            this.volumeGroup.Controls.Add(this.soundVolumeLevelLabel);
            this.volumeGroup.Controls.Add(this.musicVolumeLabel);
            this.volumeGroup.Controls.Add(this.soundVolumeLabel);
            this.volumeGroup.Controls.Add(this.musicVolumeSlider);
            this.volumeGroup.Controls.Add(this.soundVolumeSlider);
            this.volumeGroup.Location = new System.Drawing.Point(14, 63);
            this.volumeGroup.Name = "volumeGroup";
            this.volumeGroup.Size = new System.Drawing.Size(487, 187);
            this.volumeGroup.TabIndex = 3;
            this.volumeGroup.TabStop = false;
            this.volumeGroup.Text = "Volume";
            // 
            // musicDirectory
            // 
            this.musicDirectory.Location = new System.Drawing.Point(95, 151);
            this.musicDirectory.Name = "musicDirectory";
            this.musicDirectory.Size = new System.Drawing.Size(386, 20);
            this.musicDirectory.TabIndex = 5;
            this.musicDirectory.Leave += new System.EventHandler(this.musicDirectory_Leave);
            // 
            // soundDirectory
            // 
            this.soundDirectory.Location = new System.Drawing.Point(98, 70);
            this.soundDirectory.Name = "soundDirectory";
            this.soundDirectory.Size = new System.Drawing.Size(383, 20);
            this.soundDirectory.TabIndex = 6;
            this.soundDirectory.Leave += new System.EventHandler(this.soundDirectory_Leave);
            // 
            // musicDirectoryLabel
            // 
            this.musicDirectoryLabel.AutoSize = true;
            this.musicDirectoryLabel.Location = new System.Drawing.Point(6, 154);
            this.musicDirectoryLabel.Name = "musicDirectoryLabel";
            this.musicDirectoryLabel.Size = new System.Drawing.Size(83, 13);
            this.musicDirectoryLabel.TabIndex = 8;
            this.musicDirectoryLabel.Text = "Music Directory:";
            // 
            // testMusic
            // 
            this.testMusic.Location = new System.Drawing.Point(204, 114);
            this.testMusic.Name = "testMusic";
            this.testMusic.Size = new System.Drawing.Size(75, 23);
            this.testMusic.TabIndex = 9;
            this.testMusic.Text = "Test &Music";
            this.testMusic.UseVisualStyleBackColor = true;
            this.testMusic.Click += new System.EventHandler(this.testMusic_Click);
            // 
            // soundDirectoryLabel
            // 
            this.soundDirectoryLabel.AutoSize = true;
            this.soundDirectoryLabel.Location = new System.Drawing.Point(6, 73);
            this.soundDirectoryLabel.Name = "soundDirectoryLabel";
            this.soundDirectoryLabel.Size = new System.Drawing.Size(86, 13);
            this.soundDirectoryLabel.TabIndex = 7;
            this.soundDirectoryLabel.Text = "Sound Directory:";
            // 
            // testSound
            // 
            this.testSound.Location = new System.Drawing.Point(204, 19);
            this.testSound.Name = "testSound";
            this.testSound.Size = new System.Drawing.Size(75, 23);
            this.testSound.TabIndex = 8;
            this.testSound.Text = "Test &Sound";
            this.testSound.UseVisualStyleBackColor = true;
            this.testSound.Click += new System.EventHandler(this.testSound_Click);
            // 
            // musicVolumeLevelLabel
            // 
            this.musicVolumeLevelLabel.AutoSize = true;
            this.musicVolumeLevelLabel.Location = new System.Drawing.Point(163, 114);
            this.musicVolumeLevelLabel.Name = "musicVolumeLevelLabel";
            this.musicVolumeLevelLabel.Size = new System.Drawing.Size(0, 13);
            this.musicVolumeLevelLabel.TabIndex = 7;
            // 
            // soundVolumeLevelLabel
            // 
            this.soundVolumeLevelLabel.AutoSize = true;
            this.soundVolumeLevelLabel.Location = new System.Drawing.Point(163, 19);
            this.soundVolumeLevelLabel.Name = "soundVolumeLevelLabel";
            this.soundVolumeLevelLabel.Size = new System.Drawing.Size(0, 13);
            this.soundVolumeLevelLabel.TabIndex = 6;
            // 
            // musicVolumeLabel
            // 
            this.musicVolumeLabel.AutoSize = true;
            this.musicVolumeLabel.Location = new System.Drawing.Point(6, 114);
            this.musicVolumeLabel.Name = "musicVolumeLabel";
            this.musicVolumeLabel.Size = new System.Drawing.Size(38, 13);
            this.musicVolumeLabel.TabIndex = 5;
            this.musicVolumeLabel.Text = "Music:";
            // 
            // soundVolumeLabel
            // 
            this.soundVolumeLabel.AutoSize = true;
            this.soundVolumeLabel.Location = new System.Drawing.Point(6, 19);
            this.soundVolumeLabel.Name = "soundVolumeLabel";
            this.soundVolumeLabel.Size = new System.Drawing.Size(41, 13);
            this.soundVolumeLabel.TabIndex = 4;
            this.soundVolumeLabel.Text = "Sound:";
            // 
            // musicVolumeSlider
            // 
            this.musicVolumeSlider.LargeChange = 10;
            this.musicVolumeSlider.Location = new System.Drawing.Point(53, 114);
            this.musicVolumeSlider.Maximum = 100;
            this.musicVolumeSlider.Name = "musicVolumeSlider";
            this.musicVolumeSlider.Size = new System.Drawing.Size(104, 45);
            this.musicVolumeSlider.TabIndex = 3;
            this.musicVolumeSlider.TickFrequency = 10;
            this.musicVolumeSlider.Value = 50;
            this.musicVolumeSlider.Scroll += new System.EventHandler(this.musicVolumeSlider_Scroll);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Location = new System.Drawing.Point(14, 17);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(119, 17);
            this.chkEnabled.TabIndex = 4;
            this.chkEnabled.Text = "Reyn Time Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // ReynTime
            // 
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.volumeGroup);
            this.Controls.Add(this.chkIgnoreImport);
            this.Name = "ReynTime";
            this.Size = new System.Drawing.Size(504, 534);
            ((System.ComponentModel.ISupportInitialize)(this.soundVolumeSlider)).EndInit();
            this.volumeGroup.ResumeLayout(false);
            this.volumeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musicVolumeSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}