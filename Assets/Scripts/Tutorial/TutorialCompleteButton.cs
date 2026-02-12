using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Tutorial
{
    internal class TutorialCompleteButton : AbstractButton
    {
        [SerializeField][Min(1)] private int _stage = 1;
        
        private Tutorial _tutorial;

        [Inject]
        private void Initialize(Tutorial tutorial) =>
            _tutorial = tutorial;
        
        protected override void HandleClick() =>
            _tutorial.Complete(_stage);
    }
}