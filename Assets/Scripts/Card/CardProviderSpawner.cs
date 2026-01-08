using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    internal class CardProviderSpawner : ItemProviderSpawner
    {
        [SerializeField] private ToggleGroup _group;
        
        protected override void InitializeProvider(ItemProvider itemProvider)
        {
            base.InitializeProvider(itemProvider);
            itemProvider.GetComponent<Toggle>().group = _group;
        }
    }
}