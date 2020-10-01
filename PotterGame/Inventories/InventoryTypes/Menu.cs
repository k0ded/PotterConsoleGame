using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PotterGame.Inventories.Items;
using PotterGame.Utils;

namespace PotterGame.Inventories.InventoryTypes
{
    public class Menu : BaseInventory
    {

        public Menu()
        {
            Name = "Menu";
            Content = new List<BaseItem>();
            Header = new Text("");
            HeaderFoot = new Text("");
            InventoryTextType = TextType.CENTERED;

            Content.Add(GetStartItem());
            Content.Add(GetExitItem());
        }

        private GenericItem GetStartItem()
        {
            var item = new GenericItem("Start");
            item.InteractEventTask = Program.Player.Start;
            return item;
        }

        private GenericItem GetLoadItem()
        {
            var item = new GenericItem("Load");
            item.InteractEventTask = delegate
            {
                
            };
            return item;
        }
        
        private GenericItem GetExitItem()
        {
            var item = new GenericItem("Exit");
            item.InteractEventTask = delegate
            {
                Environment.Exit(0);
            };
            return item;
        }

        protected override Text GetItemName(BaseItem aItem, bool aSelected)
        {
            var itemName = $"[{aItem.Name}]";
            if (!aSelected)
                return new Text(itemName, 128, 128, 128, true);
            const string prefix = ">> ";
            const string suffix = " <<";
            Selected = aItem;
            return new Text(prefix + itemName + suffix, ColorCode.WHITE);
        }

        public override void RunBackspaceAction(){}
    }
}