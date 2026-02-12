using System.Linq;

namespace Item
{
    public class SaveableContainer : Container
    {
        public SaveableContainer(ContainerData data)
            : base(data) =>
            ContentChanged += Save;

        public override void Dispose()
        {
            ContentChanged -= Save;
            base.Dispose();
        }

        protected override SerializableData GetSerializableData() =>
            Services.Preferences.LoadJson(Type.ToString(), base.GetSerializableData());

        private void Save()
        {
            SerializableData saveData = new ()
            {
                Capacity = Capacity,
                ItemSubtypes = Items.Select(item => item.Subtype).ToList(),
            };
            
            Services.Preferences.SaveJson(Type.ToString(), saveData);
        }
    }
}