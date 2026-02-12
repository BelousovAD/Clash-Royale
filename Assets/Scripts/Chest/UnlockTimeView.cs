using Item;

namespace Chest
{
    internal class UnlockTimeView : ItemTextView<Chest>
    {
        private const int Min2Sec = 60;

        protected override void UpdateView()
        {
            if (Item is null)
            {
                TextField.text = string.Empty;
            }
            else
            {
                int minutes = Item.UnlockTime / Min2Sec;
                int seconds = Item.UnlockTime % Min2Sec;
                TextField.text = string.Format(Format, minutes, seconds);
            }
        }
    }
}