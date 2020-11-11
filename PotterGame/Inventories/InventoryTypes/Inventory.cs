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
            new Text("Player".PadRight(45) + "(%money%)"),
            new Text("     Item".PadRight(45) + "Value" + "Count".PadLeft(10))) { }

        protected override Text GetItemName(BaseItem aItem, bool aSelected)
        {
            return base.GetItemName(aItem, aSelected) + $"({aItem.Count})".PadLeft(10);
        }

        public override void OpenInventory(bool aSetOpened)
        {
            OpenInventory(0,0, aSetOpened);
        }
    }
}
