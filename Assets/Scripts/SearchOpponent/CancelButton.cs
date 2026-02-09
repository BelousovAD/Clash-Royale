using Common;
using UnityEngine;

namespace SearchOpponent
{
    internal class CancelButton : AbstractButton
    {
        [SerializeField] private FakeOpponentSearch _opponentSearch;
        
        protected override void HandleClick() =>
            _opponentSearch.Cancel();
    }
}