using UnityEngine;

namespace UnitSpawn
{
    public class Indicator : MonoBehaviour
    {
        public Vector3 Position { get; private set; }

        public void SetPositionToSpawn(Vector3 position) =>
            Position = position;
    }
}