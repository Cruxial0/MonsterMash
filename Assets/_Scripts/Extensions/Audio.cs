using _Scripts.MonoBehaviour.CommonFunctionality;
using UnityEngine;

namespace _Scripts.Extensions
{
    public static class Audio
    {
        public static AudioClip GetRandomClip(this MonoBehaviour.CommonFunctionality.Audio a)
        {
            return a.soundClips[Random.Range(0, a.soundClips.Count)];
        }
    }
}