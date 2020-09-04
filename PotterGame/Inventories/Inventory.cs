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
            Content = new List<BaseItem>();
            Player = Program.GetPlayer();
        }

        public override void OpenInventory()
        {
            if (Player == null)
                Player = Program.GetPlayer();
            Player.InventoryOpened(this);
            OpenInventory(0,0);
        }
    }
}
