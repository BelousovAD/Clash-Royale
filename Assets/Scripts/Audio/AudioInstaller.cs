namespace Audio
{
    using System.Collections.Generic;
    using Bootstrap;
    using Reflex.Core;
    using UnityEngine;
    using UnityEngine.Audio;

    internal class AudioInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioMixerGroup _musicGroup;
        [SerializeField] private AudioMixerGroup _soundGroup;
        [SerializeField] private PooledAudioSource _prefab;
        [SerializeField] private List<Track> _musics = new();
        [SerializeField] private List<Track> _sounds = new();

        private Music _music;
        private Sound _sound;
        private ContainerBuilder _builder;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            GameObject spawnerObject = new(nameof(AudioSourceSpawner));
            AudioSourceSpawner spawner = spawnerObject.AddComponent<AudioSourceSpawner>();
            DontDestroyOnLoad(spawnerObject);
            spawner.Initialize(_prefab, spawnerObject.transform);
            _music = new Music(_musicGroup, spawner, _musics);
            _sound = new Sound(_soundGroup, spawner, _sounds);

            _builder
                .AddSingleton(_music)
                .AddSingleton(_sound)
                .AddSingleton(new AudioMixerController(_audioMixer, _music, _sound));
            
            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;
            
            _music.Initialize(container.Resolve<SavvyServicesProvider>());
            _sound.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}