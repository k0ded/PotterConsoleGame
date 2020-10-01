using PotterGame.Inventories.InventoryTypes;
using PotterGame.Player.Story;

namespace PotterGame.Inventories.Items.ShopItems
{
    public class OlivandersItem : BaseShopItem
    {
        public OlivandersItem()
        {
            Shop = new Shop("Olivanders' Wands");
            GenericItem item = new GenericItem("Wand");
            item.InteractEventTask = DecideWandInteractEvent;
        }

        private void DecideWandInteractEvent()
        {
            if (Program.Player.PlayerWand != null)
                SendWandTraits();
            else 
                BuyWand();
        }

        private void BuyWand()
        {
            
            
            
            
            
            
            if(Program.Player.Context is MainStory)
                ((MainStory)Program.Player.Context).RunStory(2);
        }

        private void SendWandTraits()
        {
            
        }
    }
}