using System.Collections.Generic;
using PotterGame.Inventories.Items;
using PotterGame.Utils;

namespace PotterGame.Inventories.InventoryTypes
{
    public class Shop : BaseInventory
    {

        public Shop(string aName)
        {
            Name = aName;
            Content = new List<BaseItem>();
            Header = new Text(Name.PadRight(45).PadLeft(0) + $"({Player.Player.Money})");
            HeaderFoot = new Text("     Item".PadRight(45) + "Price");
        }

        public override void RunInteractAction()
        {
            if (Selected.Value > Player.Player.Money) return;
            if (!Program.Player.RemoveMoney(Selected.Value)) return;
            
            InventoryManager.PlayerInventory.AddItem(Selected.Clone());
            TextUtils.SendMessage(new Text("+1 " + Selected.Name), TextType.DEBUG);
        }

        public override void RunBackspaceAction()
        {
            if (InventoryManager.OpenInventory == this)
            {
                base.RunBackspaceAction();
                return;
            }
            InventoryManager.OpenInventory.OpenInventory(true);
        }
    }
}
