using System.Linq;
using Item;
using Rarity;
using TMPro;
using UnityEngine;

namespace Card
{
    [RequireComponent(typeof(TMP_Text))]
    internal class CardCountPerRarityTextView : MonoBehaviour
    {
        private const ItemType CardType = ItemType.Card;
        
        [SerializeField] private string _format = "{0}";
        [SerializeField] private ItemDataList _fullCardList;
        [SerializeField] private RarityType _rarityType;
        
        private TMP_Text _textField;

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void Start()
        {
            int cardCount = _fullCardList.ItemDatas.Count(item => (item as CardData)!.Rarity == _rarityType);
            _textField.text = string.Format(_format, cardCount);
        }

        private void OnValidate()
        {
            if (_fullCardList is not null && _fullCardList.Type != CardType)
            {
                Debug.LogError($"Require {nameof(_fullCardList)} with {nameof(_fullCardList.Type)}:{CardType}");
                _fullCardList = null;
            }
        }
    }
}