using MirraGames.SDK;
using MirraGames.SDK.Common;

namespace Nickname
{
    internal class Player
    {
        private readonly IPlayer _data;

        public Player() =>
            _data = MirraSDK.Player;

        public string DisplayName => _data.DisplayName;

        public bool IsLoggedIn => _data.IsLoggedIn;
    }
}