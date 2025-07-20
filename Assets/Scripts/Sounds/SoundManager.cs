using LTX.Singletons;
using UnityEngine;

namespace DSS.Sounds
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        public void PlaySound(SoundData soundData)
        {
            if (soundData.Clip == null) return;

            GameObject go = new GameObject("Sound: " + soundData.Name);
            AudioSource audioSource = go.AddComponent<AudioSource>();
            
            Instantiate(go, transform);
            
            audioSource.clip = soundData.Clip;
            audioSource.volume = soundData.Volume;
            audioSource.pitch = soundData.HasPitch ? Random.Range(-soundData.PitchValue, soundData.PitchValue) : 1;

            audioSource.Play();
            Destroy(go, soundData.Clip.length);
        }
    }
}