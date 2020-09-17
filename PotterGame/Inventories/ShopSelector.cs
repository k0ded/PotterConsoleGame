using PotterGame.Inventories.Items.ShopItems;
using PotterGame.Utils;

namespace PotterGame.Inventories
{
    internal class ShopSelector : BaseInventory
    {
        public ShopSelector(string aName)
        {
            Name = aName;
            Player = Program.Player;
            Header = new Text(Name.PadRight(45).PadLeft(0) + $"({Player.Money})");
            HeaderFoot = new Text("     Item".PadRight(45) + "Price");
        }

        public override void RunInteractAction()
        {
            ((ShopItem) Selected).InteractEvent();
        }

    }
}
