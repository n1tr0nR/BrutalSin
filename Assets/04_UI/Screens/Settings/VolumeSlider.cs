using System;
using System.Globalization;
using Game;
using Game.Settings;
using UI;
using UnityEngine;
using UnityEngine.Audio;

namespace _04_UI.Screens.Settings
{
    public class VolumeSlider : SliderCopy, ILoadListener
    {
        public AudioType type;
        public new string name;
        private AudioMixerGroup targetGroup;

        private void Start()
        {
            targetGroup = GetGroupFromType();

            target.onValueChanged.AddListener(OnSliderChanged);
            Invoke(nameof(LoadAllData), 0.2F);
        }

        private void LoadAllData()
        {
            
        }

        private void OnSliderChanged(float value)
        {
            value = Mathf.Round(value / 5f) * 5f;
            text.text = value.ToString(CultureInfo.InvariantCulture);

            var perc = value / 100f;
            var db = Mathf.Lerp(-80f, 0f, perc);

            var manager = SettingsManager.Instance;
            manager.settingsObject.SetVolumeFromType((int)db, type);
            targetGroup.audioMixer.SetFloat(name, manager.settingsObject.GetVolumeFromType(type));
        }

        private AudioMixerGroup GetGroupFromType()
        {
            var var = type switch
            {
                AudioType.Music => MusicManager.Instance.music,
                AudioType.Sounds => MusicManager.Instance.sound,
                AudioType.Voice => MusicManager.Instance.voice,
                _ => MusicManager.Instance.master
            };
            return var;
        }

        public void OnLoadSettings(SettingsObject settings)
        {
            if (targetGroup == null)
            {
                targetGroup = GetGroupFromType();
            }
            
            SetFromType(settings.GetVolumeFromType(type));
        }

        private void SetFromType(float volume)
        {
            var perc = Mathf.InverseLerp(-80f, 0f, volume);
            var value = perc * 100f;

            target.value = value;
            text.text = value.ToString(CultureInfo.InvariantCulture);
            targetGroup.audioMixer.SetFloat(name, volume);
            
            value = Mathf.Round(value / 5f) * 5f;
            text.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public enum AudioType
    {
        Master,
        Music,
        Sounds,
        Voice
    }
}