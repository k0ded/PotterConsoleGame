using System;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items.ShopItems.OlivandersItems.Wands;
using PotterGame.Player.Story;

namespace PotterGame.Inventories.Items.ShopItems
{
    public class OlivandersItem : BaseShopItem
    {
        public OlivandersItem() : base("Olivanders' Wands")
        {
            Shop = new Shop("Olivanders' Wands", true);
            GenericItem item = new GenericItem("Wand");
            item.InteractEventTask = DecideWandInteractEvent;
            Shop.AddItem(item);
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
            Random r = new Random();
            var wand = new Wand();
            wand.Core = (WandCores) r.Next(0,4);
            wand.Wood = (WandWoods) r.Next(0,10);
            Program.Player.PlayerWand = wand;

            if(Program.Player.Context is MainStory)
                ((MainStory)Program.Player.Context).RunStory(2);
        }

        private void SendWandTraits()
        {
            
        }
    }
}