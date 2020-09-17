using System;
using System.Collections.Generic;
using PotterGame.Inventories.Items;
using PotterGame.Utils;

namespace PotterGame.Inventories
{
    public class Shop : BaseInventory
    {

        public Shop(string aName)
        {
            Name = aName;
            Content = new List<BaseItem>();
            Player = Program.Player;
            Header = new Text(Name.PadRight(45).PadLeft(0) + $"({Player.Money})");
            HeaderFoot = new Text("     Item".PadRight(45) + "Price");
        }

        public override void RunInteractAction()
        {
            if (Selected.Value > Program.Player.Money) return;
            if (!Program.Player.RemoveMoney(Selected.Value)) return;
            
            Program.Player.PlayerInventory.AddItem(Selected.Clone());
            TextUtils.SendMessage(new Text("+1 " + Selected.Name), TextType.DEBUG);
        }

        public override void RunBackspaceAction()
        {
            if (Program.Player.OpenInventory == this)
            {
                base.RunBackspaceAction();
                return;
            }
            Program.Player.OpenInventory.OpenInventory(true);
        }
    }
}
