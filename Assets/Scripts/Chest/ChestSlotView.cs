using Item;
using UnityEngine;

namespace Chest
{
    internal class ChestSlotView : ItemView<Chest>
    {
        [SerializeField] private GameObject _icon;
        [SerializeField] private GameObject _description;
        
        protected override void UpdateView()
        {
            _icon.SetActive(Item is not null);
            _description.SetActive(Item is null);
        }
    }
}