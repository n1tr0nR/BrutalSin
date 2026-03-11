using System;
using _04_UI.Screens.Settings;
using Core;
using Game.Settings;
using UnityEngine;

namespace Game
{
    public class SettingsManager : AbstractManager<SettingsManager>
    {
        public SettingsObject settingsObject;

        private void Start()
        {
            Load();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void Save()
        {
            var settings = new ES3Settings(ES3.EncryptionType.None, "thePasswordIsGone"); //TODO: Debug Encryption on none, switch it back to AES
            ES3.Save("masterVolume", settingsObject.masterVolume, "settings/settings.ntrn", settings);
            ES3.Save("musicVolume", settingsObject.musicVolume, "settings/settings.ntrn", settings);
            ES3.Save("soundVolume", settingsObject.soundVolume, "settings/settings.ntrn", settings);
            ES3.Save("voiceVolume", settingsObject.voiceVolume, "settings/settings.ntrn", settings);
            
            Debug.Log("Saved assets at 'settings/settings.ntrn'");
        }
        
        private void Load(){
            //Update the SettingsObject:
            var settings = new ES3Settings(ES3.EncryptionType.None, "thePasswordIsGone"); //TODO: Debug Encryption on none, switch it back to AES

            var master = ES3.Load("masterVolume", "settings/settings.ntrn", 0, settings);
            var music  = ES3.Load("musicVolume", "settings/settings.ntrn", 0, settings);
            var sound  = ES3.Load("soundVolume", "settings/settings.ntrn", 0, settings);
            var voice  = ES3.Load("voiceVolume", "settings/settings.ntrn", 0, settings);

            settingsObject.masterVolume = master;
            settingsObject.musicVolume = music;
            settingsObject.soundVolume = sound;
            settingsObject.voiceVolume = voice;
            
            Debug.Log("Loaded assets at 'settings/settings.ntrn'");
            
            //Set Values:
            MusicManager.Instance.master.audioMixer.SetFloat("Master", master);
            MusicManager.Instance.music.audioMixer.SetFloat("Music", music);
            MusicManager.Instance.sound.audioMixer.SetFloat("Sound", sound);
            MusicManager.Instance.voice.audioMixer.SetFloat("Voice", voice);

            foreach (var mono in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
            {
                if (mono is ILoadListener listener)
                {
                    listener.OnLoadSettings(settingsObject);
                }
            }
        }
    }
}