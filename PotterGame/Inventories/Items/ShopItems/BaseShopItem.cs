using PotterGame.Inventories.InventoryTypes;

namespace PotterGame.Inventories.Items.ShopItems
{
    public class BaseShopItem : BaseItem
    {
        public Shop Shop;

        public BaseShopItem(string aName) : base(aName, 0) {}
        
        public override void InteractEvent()
        {
            if (IsOpened)
            {
                Shop.RunInteractAction();
            }
            else
            {
                Shop.OpenInventory(false);
                IsOpened = true;
            }
        }

        public override void ReturnEvent()
        {
            if (IsOpened)
            {
                Shop.RunBackspaceAction();
            }
        }
    }
}