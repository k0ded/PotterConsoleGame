using System;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items.ShopItems.OlivandersItems.Wands;
using PotterGame.Utils;

namespace PotterGame.Inventories.Items.ShopItems
{
    public class OlivandersItem : BaseShopItem
    {
        public OlivandersItem()
        {
            Shop = new Shop("Olivanders' Wands");
            Shop.AddItem(GetWandItem());
        }

        private GenericItem GetWandItem()
        {
            var wand = new GenericItem("Get your wand");
            var r = new Random();
            wand.InteractEventTask = () =>
            {
                if (Program.Player.HasWand)
                {
                    TextUtils.SendMessage(new Text("You already have a wand!"), TextType.EXPLANATION);
                }
                var wandWood = r.Next(0, 9);
                var wandCore = r.Next(0, 3);
                var wandStruct = new Wand
                {
                    Wood = (WandWoods) wandWood,
                    Core = (WandCores) wandCore
                };
            };
            
            return wand;
        }
    }
}