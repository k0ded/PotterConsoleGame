using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using PotterGame.Inventories.Items;
using PotterGame.Utils;

namespace PotterGame.Inventories
{
    public class Menu : BaseInventory
    {

        public Menu()
        {
            Name = "Menu";
            Content = new List<BaseItem>();
            Player = Program.Player;
            Header = new Text("");
            HeaderFoot = new Text("");
            myTextType = TextType.CENTERED;

            Content.Add(GetStartItem());
            Content.Add(GetExitItem());
        }

        private GenericItem GetStartItem()
        {
            var item = GenericItem.CreateItem("Start");
            item.InteractEventTask = new Task(Program.Player.Start);
            return item;
        }
        
        private GenericItem GetExitItem()
        {
            var item = GenericItem.CreateItem("Exit");
            item.InteractEventTask = new Task(delegate
            {
                Environment.Exit(0);
            });
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