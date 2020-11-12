using System;
using PotterGame.Inventories.Items;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public class Settings : BaseInventory
    {
        public Settings() : base("Settings")
        {
            InventoryTextType = TextType.CENTERED;
            
            // De här ser till så att de olika items kommer med in i inventoryt
            Content.Add(GetMaximizeItem());
            Content.Add(GetBackItem());
        }

        private GenericItem GetMaximizeItem()
        {
            var item = new GenericItem("Maximize")
            {
                // När man trycker enter så körs denna biten av kod
                InteractEventTask = delegate
                {
                    Program.Maximize();
                    RunReloadAction();
                    Console.CursorVisible = false;
                }
            };
            return item;
        }

        /// <summary>
        /// Gets the back item that returns you to the menu!
        /// </summary>
        /// <returns>Returns the GenericItem that takes you back to the Menu</returns>
        private BaseItem GetBackItem()
        {
            var item = new GenericItem("Back")
            {
                // Sets the InteractEventTask (Enter is pressed) 
                InteractEventTask = Program.Player.StartMenuReload,
                ReturnEventTask = Program.Player.StartMenuReload,
                IsOpened = true
            };
            return item;
        }

        public override void RunInteractAction()
        {
            Selected?.InteractEvent();
        }

        public override void RunBackspaceAction()
        {
            Selected?.ReturnEvent();
        }

        protected override Text GetItemName(BaseItem aItem, bool aSelected)
        {
            if (aSelected)
                Selected = aItem;
            return aSelected ? new Text(aItem.Name, ColorCode.CYAN) : new Text(aItem.Name);
        }
    }
}