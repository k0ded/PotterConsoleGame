using System;
using System.Text;
using PotterGame.Inventories.Items;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public class Settings : BaseInventory
    {
        private double volume = 1;
        
        public Settings() : base("Settings")
        {
            InventoryTextType = TextType.CENTERED;
            
            Content.Add(GetMusicInteractable());
            Content.Add(GetMusicSlider());
            Content.Add(GetMaximizeItem());
            Content.Add(GetBackItem());
        }

        private BaseItem GetMusicSlider()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < (Console.WindowWidth/2 - 4) * volume; i++)
            {
                builder.Append("=");
            }

            var item = new GenericItem(builder.ToString()) {IsOpened = true};
            return item;
        }

        private BaseItem GetMusicInteractable()
        {
            var item = new GenericItem("Music Volume");
            item.InteractEventTask = () =>
            {
                //Increase volume
                volume = Math.Min(1, volume + 0.1);
                Content[1] = GetMusicSlider();
                ReloadInventory(0, 0, false);
                
                //Set volume
                
            };

            item.ReturnEventTask = () =>
            {
                //Decrease volume
                volume = Math.Max(0, volume - 0.1);
                Content[1] = GetMusicSlider();
                ReloadInventory(0, 0, false);
                
                //Set volume
                
            };
            item.Controls = "BACKSPACE - Volume down      ENTER - Volume up";
            item.IsOpened = true;
            return item;
        }
        
        private GenericItem GetMaximizeItem()
        {
            var item = new GenericItem("Maximize")
            {
                InteractEventTask = delegate
                {
                    Program.Maximize();
                    OpenInventory(2, 0, true);
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