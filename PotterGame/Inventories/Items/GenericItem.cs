using System.Threading.Tasks;

namespace PotterGame.Inventories.Items
{
    public class GenericItem : BaseItem
    {
        public static GenericItem CreateItem(string aName)
        {
            return new GenericItem(aName);
        }
        public Task InteractEventTask { get; set; }
        public Task ReturnEventTask { get; set; }
        
        public GenericItem(string aName)
        {
            Name = aName;
        }

        public override void InteractEvent()
        {
            InteractEventTask?.Start();
        }

        public override void ReturnEvent()
        {
            ReturnEventTask?.Start();
        }
    }
}