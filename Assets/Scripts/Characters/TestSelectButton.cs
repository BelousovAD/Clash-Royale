using Common;
using UnityEngine;

namespace Characters
{
    public class TestSelectButton : AbstractButton
    {
        [SerializeField] private CharacterProviderSpawnCaller _caller;
        [SerializeField] private string _pickedCard;

        protected override void Awake()
        {
            base.Awake();
            _caller = (CharacterProviderSpawnCaller)FindFirstObjectByType(typeof(CharacterProviderSpawnCaller));
        }

        protected override void HandleClick()
        {
            _caller.CallSpawn(_pickedCard);
        }
    }
}
