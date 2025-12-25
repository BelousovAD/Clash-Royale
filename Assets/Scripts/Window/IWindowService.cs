namespace Window
{
    using UnityEngine;

    public interface IWindowService
    {
        public void CloseCurrent();
        
        public RectTransform Open(string id, bool needCloseCurrent);
    }
}