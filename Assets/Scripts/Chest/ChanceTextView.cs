using System.Globalization;
using System.Linq;
using Item;
using Rarity;
using UnityEngine;

namespace Chest
{
    internal class ChanceTextView : ItemTextView<Chest>
    {
        private const string FloatFormat = "F0";
        
        [SerializeField] private RarityType _rarityType;

        protected override void UpdateView()
        {
            if (Item is null)
            {
                return;
            }

            string percent = Item.Chances
                .First(chance => chance.Rarity == _rarityType).Percent
                .ToString(FloatFormat, CultureInfo.InvariantCulture);
            TextField.text = string.Format(Format, percent);
        }
    }
}