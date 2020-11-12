using System;
using PotterGame.Inventories.Items;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public class Menu : BaseInventory
    {
        public Menu() : base("Menu")
        {
            InventoryTextType = TextType.CENTERED;

            //De här lägger till de olika items in i meny inventoryt.
            Content.Add(GetStartItem());
            Content.Add(GetSettingsItem());
            Content.Add(GetExitItem());
        }

        protected override void OpenInventory(int aSelection, int aOffset, TextType aType, bool aSetOpened)
        {
            base.OpenInventory(aSelection, aOffset, aType, aSetOpened);
            TextUtils.SendMessage(new [] {new Text("For the best gameplay experience"), new Text("please maximize your window!")}, TextType.HEADERBAR);
        }

        /// <summary>
        /// Ger dig ett start item som man kan stoppa in i menyn som kör Program.Player.Start()
        /// </summary>
        private GenericItem GetStartItem()
        {
            var item = new GenericItem("Start")
            {
                InteractEventTask = Program.Player.Start
            };
            return item;
        }

        /// <summary>
        /// Gets the settings item, This is a generic item as its not supposed to be pickupable
        /// </summary>
        /// <returns>A generic menu item to go to Settings</returns>
        private GenericItem GetSettingsItem()
        {
            // Creates the GenericItem with the name "Settings"
            var item = new GenericItem("Settings")
            {
                // Sets the InteractEventTask (Enter got pressed) to run some code
                InteractEventTask = () =>
                {
                    // Settings is an inventory so it will be opened as such
                    var settings = new Settings();
                    settings.OpenInventory(true);
                }
            };
            return item;
        }

        /// <summary>
        /// Gets the Exit item that will exit the game.
        /// </summary>
        /// <returns>The GenericItem that exits the game</returns>
        private GenericItem GetExitItem()
        {
            // Creates the GenericItem with the name "Exit"
            var item = new GenericItem("Exit")
            {
                // Sets the InteractEventTask (Enter pressed) to exit the application with the code 0
                InteractEventTask = () => Environment.Exit(0)
            };
            return item;
        }

        /// <summary>
        /// Overrides the GetItemName to make sure it doesn't display the count of the items
        /// </summary>
        /// <param name="aItem"></param>
        /// <param name="aSelected"></param>
        /// <returns></returns>
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