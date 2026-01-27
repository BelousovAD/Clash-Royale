using Item;

namespace Character
{
    internal class CharacterContainer : Container
    {
        public CharacterContainer(ContainerData data) 
            : base(data)
        { }
        
        public override Item.Item CreateItem(ItemData data, int id = DefaultId) =>
            new Character(data as CharacterData, id);
    }
}