using System.Collections.Generic;
using UnityEngine;

namespace Chest
{
    [CreateAssetMenu(fileName = nameof(ChestChanceData), menuName = nameof(Chest) + "/" + nameof(ChestChanceData))]
    public class ChestChanceData : ScriptableObject
    {
        [SerializeField] private List<Chance> _chances;
        
        public IReadOnlyList<Chance> Chances => _chances;
    }
}