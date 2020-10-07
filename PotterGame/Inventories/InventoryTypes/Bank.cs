using System.Collections.Generic;
using PotterGame.Inventories.Items;
using PotterGame.Utils;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    internal class Bank : BaseInventory
    {

        private static int Money { get; set; } = 250;

        public Bank(string aName) : base(
            aName,
            new Text("Bank".PadRight(45).PadLeft(0) + $"({Player.Player.Money})"),
            new Text("     Action".PadRight(45) + $"({Money})")) {}
    }
}
