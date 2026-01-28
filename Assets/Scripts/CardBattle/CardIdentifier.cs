using System;
using UnityEngine;

namespace CardBattle
{
    internal class CardIdentifier : MonoBehaviour
    {
        public event Action Picked;
        public event Action Droped;
        
        public void CardPicked()
        {
            Picked?.Invoke();
        }  
        
        public void CardDroped()
        {
            Droped?.Invoke();
        }
    }
}