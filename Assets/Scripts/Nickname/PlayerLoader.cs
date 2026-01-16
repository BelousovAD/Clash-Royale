using System.Collections.Generic;
using System.Linq;
using Bootstrap;
using Reflex.Attributes;
using UnityEngine;

namespace Nickname
{
    internal class PlayerLoader : MonoBehaviour, ILoadable
    {
        private Player _player;

        [Inject]
        private void Initialize(IEnumerable<Opponent> opponents) =>
            _player = opponents.First(opponent => opponent is Player) as Player;

        public void Load() =>
            _player.Load();
    }
}