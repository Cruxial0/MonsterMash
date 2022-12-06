using _Scripts.MonoBehaviour.CommonFunctionality;
using UnityEngine;

namespace _Scripts.Extensions
{
    public static class Audio
    {
        /// <summary>
        /// Gets a random AudioClip from the passed in Audio object
        /// </summary>
        /// <param name="a">Audio to get clips from</param>
        /// <returns>A random AudioClip</returns>
        public static AudioClip GetRandomClip(this MonoBehaviour.CommonFunctionality.Audio a)
        {
            return a.soundClips[Random.Range(0, a.soundClips.Count)];
        }
    }
}