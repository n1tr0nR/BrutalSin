using UnityEngine;
using UnityEngine.Audio;

namespace _06_Audio
{
    public static class AudioHelper
    {
        public static void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume, float pitchDifference, AudioMixerGroup group)
        {
            var gameObject = new GameObject("One shot audio")
            {
                transform =
                {
                    position = position
                }
            };
            
            var audioSource = (AudioSource) gameObject.AddComponent(typeof (AudioSource));
            audioSource.clip = clip;
            audioSource.spatialBlend = 1f;
            audioSource.volume = volume;
            audioSource.pitch = Random.Range(1.0F - pitchDifference, 1.0F + pitchDifference);
            audioSource.outputAudioMixerGroup = group;
            audioSource.Play();
            Object.Destroy(gameObject, clip.length * (Time.timeScale < 0.009999999776482582 ? 0.01f : Time.timeScale));
        }
    }
}