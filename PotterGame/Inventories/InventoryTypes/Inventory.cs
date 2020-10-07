using System.Collections.Generic;
using PotterGame.Inventories.Items;
using PotterGame.Utils;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    internal class Inventory : BaseInventory
    {
        public Inventory(string aName) : base(
            aName, 
            new Text("Player".PadRight(45).PadLeft(0) + $"({Player.Player.Money})"),
            new Text("     Item".PadRight(45) + "Price")) {}

        public override void OpenInventory(bool aSetOpened)
        {
            OpenInventory(0,0, aSetOpened);
        }
    }
}
