using System.Globalization;
using System.Linq;
using Item;
using Rarity;
using UnityEngine;

namespace Chest
{
    internal class ChanceTextView : ItemTextView
    {
        private const string FloatFormat = "F2";
        
        [SerializeField] private RarityType _rarityType;
        
        private new Chest Item => base.Item as Chest;
        
        public override void UpdateView()
        {
            string percent = string.Empty;
            
            if (Item is not null)
            {
                percent = Item.Chances
                    .First(chance => chance.Rarity == _rarityType).Percent
                    .ToString(FloatFormat, CultureInfo.InvariantCulture);
            }

            TextField.text = string.Format(Format, percent);
        }
    }
}