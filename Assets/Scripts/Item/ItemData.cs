using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = nameof(ItemData), menuName = nameof(Item) + "/" + nameof(ItemData))]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private string _subtype;
        [SerializeField] private Sprite _icon;

        public ItemType Type => _type;

        public string Subtype => _subtype;
        
        public Sprite Icon => _icon;
    }
}