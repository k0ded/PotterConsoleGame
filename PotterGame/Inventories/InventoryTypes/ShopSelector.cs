using PotterGame.Inventories.Items.ShopItems;
using PotterGame.Utils;

namespace PotterGame.Inventories.InventoryTypes
{
    public class ShopSelector : BaseInventory
    {
        public ShopSelector(string aName)
        {
            Name = aName;
            Header = new Text(Name.PadRight(45).PadLeft(0) + $"({Player.Player.Money})");
            HeaderFoot = new Text("     Item".PadRight(45) + "Price");
        }

        public override void RunInteractAction()
        {
            if(Selected is BaseShopItem item)
                item.InteractEvent();
        }

    }
}
