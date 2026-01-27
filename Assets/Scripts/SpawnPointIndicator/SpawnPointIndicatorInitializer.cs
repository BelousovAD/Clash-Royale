using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace SpawnPointIndicator
{
    internal class SpawnPointIndicatorInitializer : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;

        [Inject]
        private void Initialize(SpawnPointIndicator indicator) =>
            indicator.Initialize(_rawImage);
    }
}