using System;

namespace PotterGame.Inventories.Items
{
    public class GenericItem : BaseItem
    {
        public static GenericItem CreateItem(string aName)
        {
            return new GenericItem(aName);
        }
        public Action InteractEventTask { get; set; }
        public Action ReturnEventTask { get; set; }
        
        public GenericItem(string aName)
        {
            Name = aName;
        }

        public override void InteractEvent()
        {
            InteractEventTask?.Invoke();
        }

        public override void ReturnEvent()
        {
            ReturnEventTask?.Invoke();
        }
    }
}