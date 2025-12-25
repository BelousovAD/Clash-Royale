namespace Audio
{
    using System.Collections;
    using Spawn;
    using UnityEngine;
    using UnityEngine.Audio;

    [RequireComponent(typeof(AudioSource))]
    public class PooledAudioSource : PooledComponent
    {
        private AudioSource _audioSource;
        private WaitForSeconds _wait;

        private void Awake() =>
            _audioSource = GetComponent<AudioSource>();

        public void Initialize(AudioMixerGroup group, AudioClip clip)
        {
            _audioSource.outputAudioMixerGroup = group;
            _audioSource.clip = clip;
            _wait = new WaitForSeconds(clip.length);
        }

        public void Play() =>
            StartCoroutine(PlayRoutine());

        private IEnumerator PlayRoutine()
        {
            _audioSource.Play();

            yield return _wait;
            
            Release();
        }
    }
}
