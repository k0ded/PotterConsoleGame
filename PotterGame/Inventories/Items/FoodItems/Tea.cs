using System;

namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Tea : BaseItem
    {

        public Tea() : base("Tea", 5)
        {
        }
            
        public override void InteractEvent()
        {
            Program.Player.ChangeStamina(15);
            InventoryManager.OpenInventory.RemoveItem(this);
        }

        public override void ReturnEvent()
        {
            
        }
    }
}
