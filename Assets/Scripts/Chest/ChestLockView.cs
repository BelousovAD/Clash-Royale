using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Chest
{
    internal class ChestLockView : ItemView<Chest>
    {
        [SerializeField] private List<GameObject> _lockObjects = new ();
        [SerializeField] private List<GameObject> _unlockingObjects = new ();
        [SerializeField] private List<GameObject> _unlockObjects = new ();

        protected override void Subscribe()
        {
            Item.LockStatusChanged += UpdateView;
            Item.UnlockingStatusChanged += UpdateView;
        }

        protected override void Unsubscribe()
        {
            Item.LockStatusChanged -= UpdateView;
            Item.UnlockingStatusChanged -= UpdateView;
        }

        protected override void UpdateView()
        {
            _lockObjects.ForEach(gameObj =>
                gameObj.SetActive(Item is not null && Item.IsLocked && Item.IsUnlocking == false));
            _unlockingObjects.ForEach(gameObj =>
                gameObj.SetActive(Item is not null && Item.IsLocked && Item.IsUnlocking));
            _unlockObjects.ForEach(gameObj =>
                gameObj.SetActive(Item is not null && Item.IsLocked == false && Item.IsUnlocking == false));
        }
    }
}