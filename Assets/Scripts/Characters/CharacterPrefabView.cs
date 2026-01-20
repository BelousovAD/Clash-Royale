using Item;

namespace Characters
{
    public class CharacterPrefabView : ItemView<Character>
    {
        protected override void UpdateView()
        {
            if (Item is null)
            {
                return;
            }
            
            Instantiate(Item.Prefab);
        }
    }
}