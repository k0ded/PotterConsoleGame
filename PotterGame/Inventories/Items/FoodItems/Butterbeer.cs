namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Butterbeer : BaseItem
    {
        public Butterbeer() : base("Butterbeer", 10) {}
        
        public override void InteractEvent()
        {
            Program.Player.ChangeStamina(35);
            InventoryManager.OpenInventory.RemoveItem(this);
        }

        public override void ReturnEvent()
        {
            IsOpened = false;
        }
    }
}
