using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using Advanced_Combat_Tracker;

/*
 Next versions:
 - Play a special quote if vuln isn't used 10s (11-12s to be safe) after Suiton
 - Default sound/music directories based on ACT location
 */

namespace ReynTime
{
    public partial class ReynTime : UserControl, IActPluginV1
    {
        #region Members

        const string DEFAULT_SOUND_DIR = @"ReynTime\Sound";
        const string DEFAULT_MUSIC_DIR = @"ReynTime\Music";
        const string SETTINGS_FILE = @"Config\ReynTime.config.xml";

        string settingsFile;
        SettingsSerializer xmlSettings;

        #endregion

        #region Properties

        ReynTimeManager Manager
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public ReynTime()
        {
            settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, SETTINGS_FILE);

            InitializeComponent();
        }

        #endregion

        #region IActPluginV1 Interface

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            ActGlobals.oFormActMain.AfterCombatAction += oFormActMain_AfterCombatAction;

            xmlSettings = new SettingsSerializer(this);
            LoadSettings();

            Manager = new ReynTimeManager();
            Manager.LoadSoundLibrary(soundDirectory.Text, soundVolumeSlider.Value);
            Manager.LoadMusicLibrary(musicDirectory.Text, musicVolumeSlider.Value);

            pluginScreenSpace.Controls.Add(this);
            Dock = DockStyle.Fill;

            pluginStatusText.Text = "What time is it?\r\nhttps://www.youtube.com/watch?v=OMrdRaiCUug";
        }

        public void DeInitPlugin()
        {
            ActGlobals.oFormActMain.AfterCombatAction -= oFormActMain_AfterCombatAction;

            SaveSettings();
        }

        #endregion

        #region Event Handlers

        void oFormActMain_AfterCombatAction(bool isImport, CombatActionEventArgs actionInfo)
        {
            Manager.ReadCombatAction(isImport, actionInfo);
        }

        void soundVolumeSlider_Scroll(object sender, EventArgs e)
        {
            Manager.SetSoundVolume(soundVolumeSlider.Value);
            soundVolumeLevelLabel.Text = soundVolumeSlider.Value.ToString();
        }

        void musicVolumeSlider_Scroll(object sender, EventArgs e)
        {
            Manager.SetMusicVolume(musicVolumeSlider.Value);
            musicVolumeLevelLabel.Text = musicVolumeSlider.Value.ToString();
        }

        void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Manager.PluginEnabled = chkEnabled.Checked;
        }

        void chkIgnoreImport_CheckedChanged(object sender, EventArgs e)
        {
            Manager.IgnoreImport = chkIgnoreImport.Checked;
        }

        void testSound_Click(object sender, EventArgs e)
        {
            Manager.PlayRandomSound();
        }

        void testMusic_Click(object sender, EventArgs e)
        {
            Manager.PlayRandomMusic();
        }

        void soundDirectory_Leave(object sender, EventArgs e)
        {
            Manager.LoadSoundLibrary(soundDirectory.Text, soundVolumeSlider.Value);
        }

        void musicDirectory_Leave(object sender, EventArgs e)
        {
            Manager.LoadMusicLibrary(musicDirectory.Text, musicVolumeSlider.Value);
        }

        #endregion

        #region Settings

        void LoadSettings()
        {
            AddSettings(xmlSettings);
            LoadSettingsFromFile();
            SetDefaultControlValues();
        }

        void AddSettings(SettingsSerializer xmlSettings)
        {
            xmlSettings.AddControlSetting("Enabled", chkEnabled);
            xmlSettings.AddControlSetting("IgnoreImport", chkIgnoreImport);
            xmlSettings.AddControlSetting("SoundVolume", soundVolumeSlider);
            xmlSettings.AddControlSetting("MusicVolume", musicVolumeSlider);
            xmlSettings.AddControlSetting("SoundDirectory", soundDirectory);
            xmlSettings.AddControlSetting("MusicDirectory", musicDirectory);
        }

        void LoadSettingsFromFile()
        {
            if (!File.Exists(settingsFile))
                return;

            try
            {
                using (FileStream fs = new FileStream(settingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlTextReader xReader = new XmlTextReader(fs);

                    while (xReader.Read())
                    {
                        if (xReader.NodeType == XmlNodeType.Element)
                        {
                            if (xReader.LocalName == "Settings")
                                xmlSettings.ImportFromXml(xReader);
                        }
                    }

                    xReader.Close();
                }
            }
            catch
            {
                // Ignore loading errors
            }
        }

        void SetDefaultControlValues()
        {
            soundVolumeLevelLabel.Text = soundVolumeSlider.Value.ToString();
            musicVolumeLevelLabel.Text = musicVolumeSlider.Value.ToString();

            if (String.IsNullOrWhiteSpace(soundDirectory.Text))
                soundDirectory.Text = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), DEFAULT_SOUND_DIR);

            if (String.IsNullOrWhiteSpace(musicDirectory.Text))
                musicDirectory.Text = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), DEFAULT_MUSIC_DIR);
        }

        void SaveSettings()
        {
            try
            {
                using (FileStream fs = new FileStream(settingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    XmlTextWriter xmlWriter = new XmlTextWriter(fs, Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.Indentation = 2;
                    xmlWriter.IndentChar = ' ';

                    xmlWriter.WriteStartDocument(true);
                    xmlWriter.WriteStartElement("Config");  // <Config>

                    xmlWriter.WriteStartElement("Settings");    // <SettingsSerializer>
                    xmlSettings.ExportToXml(xmlWriter); // Fill the SettingsSerializer XML
                    xmlWriter.WriteEndElement();    // </SettingsSerializer>

                    xmlWriter.WriteEndElement();    // </Config>
                    xmlWriter.WriteEndDocument();

                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            catch
            {
                // Ignore saving errors
            }
        }

        #endregion
    }
}