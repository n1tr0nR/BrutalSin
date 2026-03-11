using UnityEngine;

namespace _01_Core
{
    public class AnimationTrigger : MonoBehaviour
    {
        public void PlaySound(AudioClip clip)
        {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }
    }
}