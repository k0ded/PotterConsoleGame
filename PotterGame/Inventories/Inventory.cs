using System;
using System.Collections.Generic;
using PotterGame.Inventories.Items;

namespace PotterGame.Inventories
{
    internal class Inventory : BaseInventory
    {
        public Inventory(string aName)
        {
            Name = aName;
            Content = new List<IBaseItem>();
            Player = Program.GetPlayer();
        }

    }
}
