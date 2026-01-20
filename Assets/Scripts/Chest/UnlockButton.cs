using Common;
using Item;
using UnityEngine;

namespace Chest
{
    internal class UnlockButton : AbstractButton
    {
        [SerializeField] private ItemProvider _itemProvider;
        
        private Chest Chest => _itemProvider.Item as Chest;
        
        protected override void HandleClick() =>
            Chest?.StartUnlocking();
    }
}