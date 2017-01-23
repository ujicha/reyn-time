using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Advanced_Combat_Tracker;

namespace ReynTime
{
    internal class ReynTimeManager
    {
        #region Constants

        const string YourName = "YOU";

        const string KillName = "Killing";
        const string BattleHighName = "Battle High";
        const string BattleFeverName = "Battle Fever";

        const string SuitonName = "Suiton";
        const string VulnerabilityName = "Vulnerability Up";

        string BattleHighSFXPath = @"";
        string BattleFeverSFXPath = @"";

        readonly string[] FrontlinesZones = new string[]
        {
            "Seal Rock (Seize)",
            "The Fields Of Glory (Shatter)",
            "The Borderland Ruins (Slaughter)",
            "The Borderland Ruins (Secure)"
        };

        #endregion

        #region Properties

        public bool IgnoreImport
        {
            get;
            set;
        }

        public bool PluginEnabled
        {
            get;
            set;
        }

        SoundLibrary Sounds
        {
            get;
            set;
        }

        SoundLibrary Music
        {
            get;
            set;
        }

        IList<MasterSwing> Vulns
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public ReynTimeManager()
        {
            PluginEnabled = true;
            IgnoreImport = true;

            Vulns = new List<MasterSwing>();
        }

        #endregion

        #region Public Methods

        public void LoadSoundLibrary(string dir, int volume)
        {
            Sounds = new SoundLibrary(dir);
            Sounds.Volume = volume;
            Sounds.AddAllInDirectory();
        }

        public void LoadMusicLibrary(string dir, int volume)
        {
            Music = new SoundLibrary(dir);
            Music.Volume = volume;
            Music.AddAllInDirectory();
        }

        public void PlayRandomSound()
        {
            Sounds.PlayRandom();
        }

        public void PlayRandomMusic()
        {
            Music.PlayRandom();
        }

        public void SetSoundVolume(int volume)
        {
            Sounds.Volume = volume;
        }

        public void SetMusicVolume(int volume)
        {
            Music.Volume = volume;
        }

        public void PlayBattleHighSound()
        {
            Sounds.PlayMp3(Path.Combine(Sounds.SoundDir, BattleHighSFXPath));
        }

        public void PlayBattleFeverSound()
        {
            Sounds.PlayMp3(Path.Combine(Sounds.SoundDir, BattleFeverSFXPath));
        }

        public void ReadCombatAction(bool isImport, CombatActionEventArgs actionInfo)
        {
            if (!PluginEnabled)
                return;

            if (isImport && IgnoreImport)
                return;

            FrontlinesEvents(actionInfo);

            // Only play on Suiton buff, avoid playing multiple times
            if (IsAlmostReynTime(actionInfo.combatAction))
                PlayRandomSound();

            if (IsReynTime(actionInfo.combatAction, actionInfo.tags))
            {
                Vulns.Add(actionInfo.combatAction);
                PlayRandomMusic();
            }
        }

        #endregion

        #region Private Methods

        void FrontlinesEvents(CombatActionEventArgs actionInfo)
        {
            if (!IsFrontlinesZone(actionInfo.combatAction.ParentEncounter.ZoneName))
                return;

            // Play a sound when you get a kill
            if (actionInfo.attacker == YourName && actionInfo.theAttackType == KillName)
            {
                PlayRandomSound();  // Potentially change this to a separate sound to prevent confusion with Suiton
                return;
            }

            // Play a sound when you get battle high
            if (actionInfo.victim == YourName && actionInfo.theAttackType == BattleHighName)
            {
                PlayBattleHighSound();
                return;
            }

            // Play a sound when you get battle fever
            if (actionInfo.victim == YourName && actionInfo.theAttackType == BattleFeverName)
            {
                PlayBattleFeverSound();
                return;
            }
        }

        bool IsFrontlinesZone(string zoneName)
        {
            return FrontlinesZones.Contains(zoneName);
        }

        bool IsAlmostReynTime(MasterSwing action)
        {
            if (action == null)
                return false;

            bool isCorrectAction = (action.AttackType == SuitonName);

            // Skip the following calculations when possible
            if (!isCorrectAction)
                return false;

            // The attacker must be an ally (NIN is implied given Suiton)
            // Check for self buff to avoid playing sound twice (this ability also has a damage component)
            bool isBuff = (action.Victim == action.Attacker);
            bool attackerIsAlly = IsAlly(action.Attacker, action.ParentEncounter);

            return (isCorrectAction && isBuff && attackerIsAlly);
        }

        bool IsReynTime(MasterSwing action, Dictionary<string, object> tags)
        {
            if (action == null)
                return false;

            bool isCorrectAction = (action.AttackType == VulnerabilityName);

            // Skip the following calculations when possible
            if (!isCorrectAction)
                return false;

            // The attacker must be allied NIN and the victim must be an enemy
            bool attackerIsAlly = IsAlly(action.Attacker, action.ParentEncounter);
            bool attackerIsNin = containsTag(tags, "Job", "Nin");
            bool victimIsNotAlly = !IsAlly(action.Victim, action.ParentEncounter);
            bool tenSecondsLeft = containsTag(tags, "BuffDuration", 10d);

            return isCorrectAction && attackerIsAlly && attackerIsNin && victimIsNotAlly && tenSecondsLeft;
        }

        bool containsTag(Dictionary<string, object> tags, string key, object value)
        {
            return (tags.ContainsKey(key) && tags[key].Equals(value));
        }

        bool IsAlly(string targetName, EncounterData encounter)
        {
            // Unknown, return false I guess
            if (encounter == null || String.IsNullOrEmpty(targetName))
                return false;

            List<CombatantData> allies = encounter.GetAllies();
            return (allies.Any<CombatantData>(c => c.Name == targetName));
        }

        #endregion
    }
}
