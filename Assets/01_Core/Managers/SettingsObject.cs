using System;
using UnityEngine;
using AudioType = _04_UI.Screens.Settings.AudioType;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "new Settings", menuName = "Game/Core/Settings")]
    public class SettingsObject : ScriptableObject
    {
        [Header("Audio")] 
        public int masterVolume;
        public int musicVolume;
        public int soundVolume;
        public int voiceVolume;

        public void SetVolumeFromType(int target, AudioType type)
        {
            switch (type)
            {
                case AudioType.Music:
                    musicVolume = target;
                    break;
                case AudioType.Sounds:
                    soundVolume = target;
                    break;
                case AudioType.Voice:
                    voiceVolume = target;
                    break;
                default:
                    masterVolume = target;
                    break;
            }
        }
        
        public float GetVolumeFromType(AudioType type)
        {
            float volume = type switch
            {
                AudioType.Music => musicVolume,
                AudioType.Sounds => soundVolume,
                AudioType.Voice => voiceVolume,
                _ => masterVolume
            };
            return volume;
        }
    }
}