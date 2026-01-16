using System.Collections.Generic;
using Reflex.Core;
using UnityEngine;

namespace Nickname
{
    internal class EnemyInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private List<string> _nicknames = new ();
        
        public void InstallBindings(ContainerBuilder builder)
        {
            Opponent enemy = new (OpponentType.Enemy);
            enemy.Rename(_nicknames[Random.Range(0, _nicknames.Count)]);

            builder.AddSingleton(enemy);
        }
    }
}