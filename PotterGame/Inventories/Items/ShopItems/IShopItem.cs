namespace PotterGame.Inventories.Items.ShopItems
{
    public abstract class IShopItem : BaseItem
    {
        protected Shop myShop;

        public override void InteractEvent()
        {
            throw new System.NotImplementedException();
        }

        public override void ReturnEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}