using PotterGame.Inventories.InventoryTypes;

namespace PotterGame.Inventories.Items.ShopItems
{
    public class BaseShopItem : BaseItem
    {
        public Shop Shop;

        public override void InteractEvent()
        {
            if (IsOpened)
            {
                Shop.RunInteractAction();
                return;
            }
            Shop.OpenInventory(false);
            IsOpened = true;
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