using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = nameof(ItemData), menuName = nameof(Item) + "/" + nameof(ItemData))]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _type;
        [SerializeField] private Sprite _icon;

        public string Type => _type;
        
        public Sprite Icon => _icon;
    }
}