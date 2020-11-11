using PotterGame.Inventories.InventoryTypes;
using PotterGame.Utils.Dungeons;

namespace PotterGame.Inventories.Items.ShopItems
{
    public class GringottsItem : BaseShopItem
    {
        public GringottsItem() : base("Gringotts Bank")
        {
            Shop = new Shop("Gringotts Bank", true, false);
            var tItem = DungeonManager.GetDungeonItem(Constants.tDungeon);
            var gItem = DungeonManager.GetDungeonItem(Constants.gDungeon);
            Shop.AddItem(tItem);
            Shop.AddItem(gItem);
        }
    }
}