sing UnityEngine;

namespace FSM
{
    internal class DistanceMeter
    {
        public float DistanceCalculation(Vector3 currentPosition, Vector3 targetPosition)
        {
            return Vector3.Distance(currentPosition, targetPosition);
        }
    }
}