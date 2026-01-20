using Item;

namespace Character
{
    internal class CharacterContainer : Container
    {
        public CharacterContainer(ContainerData data) 
            : base(data)
        { }
        
        protected override Item.Item CreateItem(ItemData data, int id) =>
            new Character(data as CharacterData, id);
    }
}