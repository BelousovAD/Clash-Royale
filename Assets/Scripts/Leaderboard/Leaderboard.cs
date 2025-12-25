namespace Leaderboard
{
    using System;
    using Currency;
    using MirraGames.SDK;

    internal class Leaderboard : IDisposable
    {
        private readonly string _id;
        private Money _money;

        public Leaderboard(string id) =>
            _id = id;

        public void Initialize(Money money)
        {
            _money = money;
            _money.Changed += SaveScore;
        }

        public void Dispose() =>
            _money.Changed -= SaveScore;

        private void SaveScore() =>
            MirraSDK.Achievements.SetScore(_id, _money.Value);
    }
}