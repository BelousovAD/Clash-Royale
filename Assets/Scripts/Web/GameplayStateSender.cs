namespace Web
{
    using System;
    using MirraGames.SDK;
    using UnityEngine;

    public class GameplayStateSender : MonoBehaviour
    {
        [SerializeField] private GameState _state;
        [SerializeField] private bool _sendOnEnable;
        [SerializeField] private bool _sendOnDisable;

        private void OnEnable()
        {
            if (_sendOnEnable)
            {
                SendState();
            }
        }
        
        private void OnDisable()
        {
            if (_sendOnDisable)
            {
                SendState();
            }
        }

        public void SendState()
        {
            switch (_state)
            {
                case GameState.Start:
                    MirraSDK.Analytics.GameplayStart();
                    break;
                case GameState.Stop:
                    MirraSDK.Analytics.GameplayStop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum GameState
        {
            Start = 0,
            Stop
        }
    }
}