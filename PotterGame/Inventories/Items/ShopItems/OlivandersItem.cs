using System;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items.ShopItems.OlivandersItems.Wands;
using PotterGame.Player.Story;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.Items.ShopItems
{
    public class OlivandersItem : BaseShopItem
    {
        public OlivandersItem() : base("Olivanders' Wands")
        {
            Shop = new Shop("Olivanders' Wands", true);
            var item = new GenericItem("Wand") {InteractEventTask = DecideWandInteractEvent};
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
            
            // Enums har en int värde så jag använder den för att slumpa stav.
            wand.Core = (WandCores) r.Next(0,4);
            wand.Wood = (WandWoods) r.Next(0,10);
            Program.Player.PlayerWand = wand;

            // Ser till så att input går till rätt ställe!
            InventoryManager.OpenInventory = InventoryManager.PlayerInventory;
            InventoryManager.IsInventoryOpen = false;
            
            if(Program.Player.Context is MainStory)
                ((MainStory)Program.Player.Context).RunStory(2);
        }

        private void SendWandTraits()
        {
            Text[] traits = {
                new Text("Wand"),
                new Text("Core: " + Program.Player.PlayerWand.Value.Core),
                new Text("Wood: " + Program.Player.PlayerWand.Value.Wood),
            };
            
            TextUtils.SendMessage(traits, TextType.EXPLANATION);
            
        }
    }
}