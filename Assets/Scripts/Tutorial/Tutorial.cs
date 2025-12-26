using UnityEngine;

namespace Tutorial
{
    internal class Tutorial : MonoBehaviour
    {
        private TutorialData _data;

        public Sprite Icon => _data.Icon;

        public string DescriptionKey => _data.DescriptionKey;

        public void Initialize(TutorialData data) =>
            _data = data;
    }
}