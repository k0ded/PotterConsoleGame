namespace PotterGame.Inventories.Items.ShopItems
{
    public abstract class ShopItem : BaseItem
    {
        protected Shop myShop;

        public override void InteractEvent()
        {
            if (IsOpened)
            {
                myShop.RunInteractAction();
                return;
            }
            myShop.OpenInventory(false);
            IsOpened = true;
        }

        public override void ReturnEvent()
        {
            if (IsOpened)
            {
                myShop.RunBackspaceAction();
            }
        }
    }
}