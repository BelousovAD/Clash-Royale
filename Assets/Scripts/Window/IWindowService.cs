using UnityEngine;

namespace Window
{
    public interface IWindowService
    {
        public void CloseCurrent();

        public RectTransform Open(string id, bool needCloseCurrent);
    }
}