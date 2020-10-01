using System.Collections.Generic;
using PotterGame.Inventories.Items;
using PotterGame.Utils;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    internal class Bank : BaseInventory
    {

        private int Money { get; set; } = 250;

        public Bank(string name)
        {
            Name = name;
            Content = new List<BaseItem>();
            Header = new Text("Bank".PadRight(45).PadLeft(0) + $"({Player.Player.Money})");
            HeaderFoot = new Text("     Action".PadRight(45) + $"({Money})");
            
        }
    }
}
