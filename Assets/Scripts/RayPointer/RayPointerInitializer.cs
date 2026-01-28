using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace SpawnPointIndicator
{
    internal class RayPointerInitializer : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;

        [Inject]
        private void Initialize(RayPointer indicator) =>
            indicator.Initialize(_rawImage);
    }
}