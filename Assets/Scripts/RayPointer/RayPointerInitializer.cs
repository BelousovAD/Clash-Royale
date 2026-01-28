using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace RayPointer
{
    internal class RayPointerInitializer : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;

        [Inject]
        private void Initialize(global::RayPointer.RayPointer indicator) =>
            indicator.Initialize(_rawImage);
    }
}