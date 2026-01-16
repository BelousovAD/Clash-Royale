using Reflex.Core;
using UnityEngine;

namespace Nickname
{
    internal class EnemyInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private NicknameList _nicknameList;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            Opponent enemy = new (OpponentType.Enemy);
            enemy.Rename(_nicknameList.Datas[Random.Range(0, _nicknameList.Datas.Count)]);

            builder.AddSingleton(enemy);
        }
    }
}