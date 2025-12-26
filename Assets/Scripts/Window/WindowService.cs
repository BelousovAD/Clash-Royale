using System.Collections.Generic;
using UnityEngine;

namespace Window
{
    internal class WindowService : IWindowService
    {
        private readonly IWindowSpawner _spawner;
        private readonly Stack<Window> _windowsHistory = new ();
        private readonly Dictionary<string, Window> _spawnedWindows = new ();

        public WindowService(IWindowSpawner spawner) =>
            _spawner = spawner;

        public void CloseCurrent()
        {
            if (_windowsHistory.Count > 0)
            {
                Window window = _windowsHistory.Pop();
                window.SetInteractable(false);
                window.SetActive(false);

                if (_windowsHistory.Count > 0)
                {
                    window = _windowsHistory.Peek();
                    window.SetActive(true);
                    window.SetInteractable(true);
                }
            }
        }

        public RectTransform Open(string id, bool needCloseCurrent = false)
        {
            if (_windowsHistory.Count > 0)
            {
                Window lastWindow = _windowsHistory.Peek();

                if (lastWindow.Id == id)
                {
                    return lastWindow.transform as RectTransform;
                }

                lastWindow.SetInteractable(false);

                if (needCloseCurrent)
                {
                    lastWindow.SetActive(false);
                }
            }

            if (_spawnedWindows.TryGetValue(id, out Window window) == false)
            {
                window = _spawner.Spawn(id);
                _spawnedWindows.Add(id, window);
            }

            _windowsHistory.Push(window);
            window.SetActive(true);
            window.SetInteractable(true);

            return window.transform as RectTransform;
        }
    }
}