using System.Collections.Generic;
using UnityEngine;

namespace Nickname
{
    [CreateAssetMenu(fileName = nameof(NicknameList), menuName = nameof(Nickname) + "/" + nameof(NicknameList))]
    internal class NicknameList : ScriptableObject
    {
        [SerializeField] private List<string> _datas = new ();

        public IReadOnlyList<string> Datas => _datas;
    }
}