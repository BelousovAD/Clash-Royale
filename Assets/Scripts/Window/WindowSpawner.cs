namespace Window
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    internal class WindowSpawner : MonoBehaviour, IWindowSpawner
    {
        [SerializeField] private List<Window> _windowPrefabs = new();
        
        public Window Spawn(string id)
        {
            Window window = _windowPrefabs.Find(window => window.Id == id);

            if (window is null)
            {
                throw new InvalidOperationException(
                    $"Can't open window with ID:{id}. It's not founded in prefabs list");
            }

            window = Instantiate(window, transform);

            return window;
        }
    }
}