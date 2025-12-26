using System;
using MirraGames.SDK;
using UnityEngine;

namespace Web
{
    public class GameplayStateSender : MonoBehaviour
    {
        [SerializeField] private GameState _state;
        [SerializeField] private bool _sendOnEnable;
        [SerializeField] private bool _sendOnDisable;

        private enum GameState
        {
            Start = 0,
            Stop = 1,
        }

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
    }
}