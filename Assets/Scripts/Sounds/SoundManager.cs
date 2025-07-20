using LTX.Singletons;
using UnityEngine;

namespace DSS.Sounds
{
    public class SoundManager : SceneSingleton<SoundManager>
    {
        [SerializeField] private AudioSource audioSourcePrefab;
        
        public void PlaySound(SoundData soundData)
        {
            if (soundData.Clip == null) return;
            
            AudioSource source = Instantiate(audioSourcePrefab, transform);
            
            source.clip = soundData.Clip;
            source.volume = soundData.Volume;
            
            if (soundData.HasPitch)
            {
                source.pitch = soundData.HasPitch ? 1 + Random.Range(-soundData.PitchValue, soundData.PitchValue) : 1;
            }

            source.Play();
            Destroy(source.gameObject, soundData.Clip.length);
        }
    }
}