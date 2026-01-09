using System;
using System.Collections.Generic;
using System.Linq;
using MirraGames.SDK;
using MirraGames.SDK.Common;
using Spawn;
using UnityEngine;

namespace Leaderboard
{
    internal class LeaderboardSpawner : SiblingsSpawner
    {
        private readonly List<PooledComponent> _spawned = new ();

        [SerializeField] private string _id;
        [SerializeField][Min(1)] private int _maxCount;
        [SerializeField][Min(1)] private int _topCount;
        [SerializeField] private GameObject _infoObject;

        private void OnEnable() =>
            UpdateLeaderboard();

        private void OnDisable() =>
            Clear();

        private void UpdateLeaderboard()
        {
            Clear();
            MirraSDK.Achievements.GetLeaderboard(_id, leaderboard =>
            {
                _infoObject.SetActive(leaderboard.players.Length == 0);
                Spawn(Cut(leaderboard));
            });
        }

        private void Clear()
        {
            foreach (PooledComponent pooledComponent in _spawned)
            {
                pooledComponent.Release();
            }

            _spawned.Clear();
        }

        private PlayerScore[] Cut(MirraGames.SDK.Common.Leaderboard leaderboard)
        {
            if (leaderboard.players.Length <= _maxCount)
            {
                return leaderboard.players;
            }

            List<PlayerScore> players = new ();
            int currentPlayerIndex = -1;
            string displayName = MirraSDK.Player.DisplayName;

            for (int i = 0; i < leaderboard.players.Length; i++)
            {
                if (leaderboard.players[i].displayName != displayName)
                {
                    continue;
                }

                currentPlayerIndex = i;
                break;
            }

            if (currentPlayerIndex >= _maxCount)
            {
                for (int i = 0; i < _topCount; i++)
                {
                    players.Add(leaderboard.players[i]);
                }

                int excludePlayerCount = leaderboard.players.Length - _maxCount;

                for (int i = _topCount + excludePlayerCount; i < leaderboard.players.Length; i++)
                {
                    players.Add(leaderboard.players[i]);
                }
            }
            else
            {
                players.AddRange(leaderboard.players.Take(_maxCount));
            }

            return players.ToArray();
        }

        private void Spawn(PlayerScore[] playerScores)
        {
            for (int i = 0; i < playerScores.Length; i++)
            {
                PlayerScore player = playerScores[i];
                PooledComponent pooledComponent = Spawn();
                pooledComponent.GetComponent<LeaderboardItem>().Initialize(
                    player.position,
                    player.profilePictureUrl,
                    player.displayName,
                    player.score,
                    i < _topCount,
                    MirraSDK.Player.DisplayName == player.displayName);
                _spawned.Add(pooledComponent);
            }
        }
    }
}