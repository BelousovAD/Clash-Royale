namespace Bootstrap
{
    using System;
    using System.Collections.Generic;
    using MirraGames.SDK;
    using Savvy.Container;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    internal class Loader : MonoSavvy
    {
        [SerializeField] private string _sceneToLoad;
        [SerializeField] private List<MonoBehaviour> _loaders = new();
        
        private void Start()
        {
            MirraSDK.WaitForProviders(() =>
            {
                Load();
                SceneManager.LoadScene(_sceneToLoad);
            });
        }

        private void Load()
        {
            foreach (MonoBehaviour monoBehaviour in _loaders)
            {
                if (monoBehaviour is ILoadable loader)
                {
                    loader.Load();
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Unexpected loader that is null or does not inherit {nameof(ILoadable)}");
                }
            }
        }

        private void OnValidate()
        {
            foreach (MonoBehaviour monoBehaviour in _loaders)
            {
                if (monoBehaviour is not null && monoBehaviour is not ILoadable)
                {
                    Error($"Elements in {nameof(_loaders)} must inherit {nameof(ILoadable)}");
                }
            }
        }
    }
}