using System;
using Core;
using UnityEngine;

namespace Game
{
    public class MusicManager : AbstractManager<MusicManager>
    {
        private float targetVolume = 1;

        [SerializeField] private AudioClip musicClip;
        [SerializeField] private AudioSource source;

        public void SetVolume(float volume)
        {
            targetVolume = volume;
        }

        public void ShutOffMusic()
        {
            targetVolume = 0;
            source.volume = 0;
        }

        public void ShutOnMusic()
        {
            targetVolume = 1;
            source.volume = 1;
        }

        private void Update()
        {
            source.volume = Mathf.Lerp(source.volume, targetVolume, Time.deltaTime * 5);
        }
    }
}