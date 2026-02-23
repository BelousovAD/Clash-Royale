using System;
using System.Collections.Generic;
using UnityEngine;

namespace Window
{
    internal class WindowSpawner : MonoBehaviour, IWindowSpawner
    {
        [SerializeField] private List<Window> _windowPrefabs = new ();

        public Window Spawn(string id)
        {
            Window window = _windowPrefabs.Find(window => window.Id == id);
            window = Instantiate(window ?? throw new InvalidOperationException($"Can't open window {id}"), transform);

            return window;
        }
    }
}