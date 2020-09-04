using System;
using System.Collections.Generic;
using PotterGame.Inventories.Items;

namespace PotterGame.Inventories
{
    internal class Shop : BaseInventory
    {

        public Shop(string aName)
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

        public override void RunInteractAction()
        {
            if (Selected.Value > Program.GetPlayer().Money) return;
            if (!Program.GetPlayer().RemoveMoney(Selected.Value)) return;
            
            Program.GetPlayer().PlayerInventory.AddItem(Selected);
            Console.WriteLine("+1 " + Selected.Name);
        }

    }
}
