using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Chest
{
    internal class ChestLockView : ItemView
    {
        [SerializeField] private List<GameObject> _lockObjects = new ();
        [SerializeField] private List<GameObject> _unlockingObjects = new ();
        [SerializeField] private List<GameObject> _unlockObjects = new ();
        
        protected new Chest Item { get; private set; }
        
        public override void UpdateView()
        {
            _lockObjects.ForEach(gameObj =>
                gameObj.SetActive(Item is not null && Item.IsLocked && Item.IsUnlocking == false));
            _unlockingObjects.ForEach(gameObj =>
                gameObj.SetActive(Item is not null && Item.IsLocked && Item.IsUnlocking));
            _unlockObjects.ForEach(gameObj =>
                gameObj.SetActive(Item is not null && Item.IsLocked == false && Item.IsUnlocking == false));
        }
        
        protected override void UpdateSubscriptions()
        {
            Unsubscribe();
            Item = base.Item as Chest;
            Subscribe();
            base.UpdateSubscriptions();
        }

        private void Subscribe()
        {
            if (Item is not null)
            {
                Item.LockStatusChanged += UpdateView;
                Item.UnlockingStatusChanged += UpdateView;
            }
        }

        private void Unsubscribe()
        {
            if (Item is not null)
            {
                Item.LockStatusChanged -= UpdateView;
                Item.UnlockingStatusChanged -= UpdateView;
            }
        }
    }
}