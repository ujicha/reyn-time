using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Advanced_Combat_Tracker;

namespace ReynTime
{
    internal class ReynTimeManager
    {
        #region Constants

        const string SUITON_ABILITY_NAME = "Suiton";
        const string VULN_ATTACK_TYPE = "Vulnerability Up";

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

        public void ReadCombatAction(bool isImport, CombatActionEventArgs actionInfo)
        {
            if (!PluginEnabled)
                return;

            if (isImport && IgnoreImport)
                return;

            // Only play on Suiton buff, avoid playing multiple times
            if (isAlmostReynTime(actionInfo.combatAction))
                PlayRandomSound();

            if (isReynTime(actionInfo.combatAction, actionInfo.tags))
            {
                Vulns.Add(actionInfo.combatAction);
                PlayRandomMusic();
            }
        }

        #endregion

        #region Private Methods

        bool isAlmostReynTime(MasterSwing action)
        {
            if (action == null)
                return false;

            bool isCorrectAction = (action.AttackType == SUITON_ABILITY_NAME);

            // Skip the following calculations when possible
            if (!isCorrectAction)
                return false;

            // The attacker must be an ally (NIN is implied given Suiton)
            // Check for self buff to avoid playing sound twice (this ability also has a damage component)
            bool isBuff = (action.Victim == action.Attacker);
            bool attackerIsAlly = isAlly(action.Attacker, action.ParentEncounter);

            return (isCorrectAction && isBuff && attackerIsAlly);
        }

        bool isReynTime(MasterSwing action, Dictionary<string, object> tags)
        {
            if (action == null)
                return false;

            bool isCorrectAction = (action.AttackType == VULN_ATTACK_TYPE);

            // Skip the following calculations when possible
            if (!isCorrectAction)
                return false;

            // The attacker must be allied NIN and the victim must be an enemy
            bool attackerIsAlly = isAlly(action.Attacker, action.ParentEncounter);
            bool attackerIsNin = containsTag(tags, "Job", "Nin");
            bool victimIsNotAlly = !isAlly(action.Victim, action.ParentEncounter);
            bool tenSecondsLeft = containsTag(tags, "BuffDuration", 10d);

            return isCorrectAction && attackerIsAlly && attackerIsNin && victimIsNotAlly && tenSecondsLeft;
        }

        bool containsTag(Dictionary<string, object> tags, string key, object value)
        {
            return (tags.ContainsKey(key) && tags[key].Equals(value));
        }

        bool isAlly(string targetName, EncounterData encounter)
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
