using System;

namespace PotterGame.Inventories.Items
{
    public class GenericItem : BaseItem
    {
        public Action InteractEventTask { get; set; }
        public Action ReturnEventTask { get; set; }
        
        public GenericItem(string aName) : base(aName, 0) {}

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