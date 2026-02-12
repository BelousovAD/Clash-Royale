using MirraGames.SDK;
using UnityEngine;

namespace Gameplay
{
    public class TimeScaleSetter : MonoBehaviour
    {
        [SerializeField][Min(0f)] private float _timeScale;
        [SerializeField] private bool _setOnEnable;
        [SerializeField] private bool _setOnDisable;

        private void OnEnable()
        {
            if (_setOnEnable)
            {
                MirraSDK.Time.Scale = _timeScale;
            }
        }

        private void OnDisable()
        {
            if (_setOnDisable)
            {
                MirraSDK.Time.Scale = _timeScale;
            }
        }
    }
}