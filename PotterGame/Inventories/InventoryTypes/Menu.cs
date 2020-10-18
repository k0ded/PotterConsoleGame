using System;
using PotterGame.Inventories.Items;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public class Menu : BaseInventory
    {

        public Menu() : base(
            "Menu"
            )
        {
            InventoryTextType = TextType.CENTERED;

            Content.Add(GetStartItem());
            Content.Add(GetMaximizeItem());
            Content.Add(GetExitItem());
        }

        protected override void OpenInventory(int aSelection, int aOffset, TextType aType, bool aSetOpened)
        {
            base.OpenInventory(aSelection, aOffset, aType, aSetOpened);
            TextUtils.SendMessage(new [] {new Text("For the best gameplay experience"), new Text("please maximize your window!")}, TextType.HEADERBAR);
        }

        private GenericItem GetStartItem()
        {
            var item = new GenericItem("Start")
            {
                InteractEventTask = Program.Player.Start
            };
            return item;
        }

        private GenericItem GetMaximizeItem()
        {
            var item = new GenericItem("Maximize")
            {
                InteractEventTask = delegate
                {
                    Program.Maximize();
                    OpenInventory(1, 0, true);
                    Console.CursorVisible = false;
                }
            };
            return item;
        }

        private GenericItem GetExitItem()
        {
            var item = new GenericItem("Exit")
            {
                InteractEventTask = delegate
                {
                    Environment.Exit(0);
                }
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