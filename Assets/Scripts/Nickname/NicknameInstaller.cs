using Reflex.Core;
using UnityEngine;

namespace Nickname
{
    internal class NicknameInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new Player());
    }
}