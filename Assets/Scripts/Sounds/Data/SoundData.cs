using UnityEngine;

namespace DSS.Sounds
{
    [CreateAssetMenu( fileName = "SoundData", menuName = "DSS/Sounds/SoundData", order = 1)]
    public class SoundData : ScriptableObject
    {
        public string Name;
        public AudioClip Clip;
        
        [Range(0f, 1f)] public float Volume;

        public bool HasPitch;
        [Range(-3f, 3f)] public float PitchValue;
    }
}