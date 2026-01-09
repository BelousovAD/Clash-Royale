using Common;
using UnityEngine;

namespace Item
{
    public class SelectButton : AbstractButton
    {
        [SerializeField] private ItemProvider _itemProvider;
        
        protected override void HandleClick() =>
            _itemProvider.Item?.Select();
    }
}