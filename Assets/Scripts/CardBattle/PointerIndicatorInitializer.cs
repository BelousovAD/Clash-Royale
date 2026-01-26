using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CardBattle
{
    internal class PointerIndicatorInitializer : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;

        [Inject]
        private void Initialize(PointerIndicator indicator) =>
            indicator.Initialize(_rawImage);
    }
}