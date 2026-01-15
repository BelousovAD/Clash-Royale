using UnityEngine;

namespace Reward
{
    [CreateAssetMenu(fileName = nameof(RewardData), menuName = nameof(Reward) + "/" + nameof(RewardData))]
    internal class RewardData : ScriptableObject
    {
        [SerializeField] private RewardType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField][Min(0)] private int _winAmount;
        [SerializeField][Min(0)] private int _loseAmount;

        public Sprite Icon => _icon;

        public int LoseAmount => _winAmount;

        public RewardType Type => _type;
        
        public int WinAmount => _winAmount;
    }
}