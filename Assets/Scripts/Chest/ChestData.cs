using System.Collections.Generic;
using Item;
using Rarity;
using UnityEngine;

namespace Chest
{
    [CreateAssetMenu(fileName = nameof(ChestData), menuName = nameof(Chest) + "/" + nameof(ChestData))]
    public class ChestData : ItemData
    {
        [SerializeField] private RarityType _rarity;
        [SerializeField] private GameObject _prefab;
        [SerializeField][Min(0)] private int _unlockTime;
        [SerializeField] private List<Chance> _chances;
        
        public IReadOnlyList<Chance> Chances => _chances;

        public GameObject Prefab => _prefab;
        
        public RarityType Rarity => _rarity;

        public int UnlockTime => _unlockTime;
    }
}