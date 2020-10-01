using System.Collections.Generic;
using PotterGame.Inventories.Items;
using PotterGame.Utils;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    internal class Inventory : BaseInventory
    {
        public Inventory(string aName)
        {
            Name = aName;
            Content = new List<BaseItem>();
            Header = new Text("Player".PadRight(45).PadLeft(0) + $"({Player.Player.Money})");
            HeaderFoot = new Text("     Item".PadRight(45) + "Price");
        }

        public override void OpenInventory(bool aSetOpened)
        {
            OpenInventory(0,0, aSetOpened);
        }
    }
}
