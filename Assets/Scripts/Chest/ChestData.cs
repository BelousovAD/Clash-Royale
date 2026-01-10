using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Chest
{
    [CreateAssetMenu(fileName = nameof(ChestData), menuName = nameof(Chest) + "/" + nameof(ChestData))]
    internal class ChestData : ItemData
    {
        [SerializeField] private List<Chance> _chances;
        
        public IReadOnlyList<Chance> Chances => _chances;
    }
}