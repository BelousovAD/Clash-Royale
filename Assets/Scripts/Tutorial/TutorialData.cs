using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = nameof(TutorialData), menuName = nameof(TutorialOpener) + "/" + nameof(TutorialData))]
    internal class TutorialData : ScriptableObject
    {
        [SerializeField] private string _saveKey = "Tutorial";
        [SerializeField][Min(1)] private int _level = 1;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _descriptionKey = "TutorialDescription";

        public string SaveKey => _saveKey;

        public int Level => _level;

        public Sprite Icon => _icon;

        public string DescriptionKey => _descriptionKey;
    }
}