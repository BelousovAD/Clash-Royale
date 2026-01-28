using UnityEngine;

namespace RayPointer
{
    public class Indicator : MonoBehaviour
    {
        public Vector3 PositionToSpawn { get; private set; }

        public void SetPositionToSpawn(Vector3 postion)
        {
            PositionToSpawn = postion;
        }
    }
}