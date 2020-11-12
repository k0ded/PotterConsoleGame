using System;
using System.Collections.Generic;
using System.Linq;
using PotterGame.Inventories.Items;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public abstract class BaseInventory
    {
        private string Controls { get; } = "[W/S] - Scroll        [ENTER] - Interact        [BACKSPACE] - Back";
        
        protected List<BaseItem> Content;
        private int mySelection;
        private int myOffset;
        protected BaseItem Selected;
        protected Text Header { get; }
        protected Text HeaderFoot { get; }
        protected TextType InventoryTextType = TextType.INVENTORY;

        protected string Name = "DEFAULT";

        /// <summary>
        /// Opens a regular inventory.
        /// </summary>
        /// <param name="aSelection">The selected row in the inventory</param>
        /// <param name="aOffset">The amount the inventory has scrolled down</param>
        /// <param name="aSetOpened">
        /// Asks if you want to set the open inventory to this specific one.
        /// Should be false if you're in subinventory of a normal inventory.
        /// </param>
        protected void OpenInventory(int aSelection, int aOffset, bool aSetOpened)
        {
            OpenInventory(aSelection, aOffset, InventoryTextType, aSetOpened);
        }

        public virtual void OpenInventory(bool aSetOpened)
        {
            OpenInventory(0,0, aSetOpened);
        }

        /// <summary>
        /// Sends an updated inventory without clearing the console.
        /// Removes the flicker effect when scrolling through the items.
        /// </summary>
        /// <param name="aSelection">The selected row in the inventory</param>
        /// <param name="aOffset">The amount the inventory has scrolled down</param>
        /// <param name="aSetOpened">
        /// Asks if you want to set the open inventory to this specific one.
        /// Should be false if you're in subinventory of a normal inventory.
        /// </param>
        protected void ReloadInventory(int aSelection, int aOffset, bool aSetOpened)
        {
            OpenInventory(aSelection, aOffset, InventoryTextType, aSetOpened);
        }

        protected BaseInventory(string name, Text aHeader = null, Text aHeaderFoot = null)
        {
            Name = name;
            Header = aHeader;
            HeaderFoot = aHeaderFoot;
            Content = new List<BaseItem>();
        }

        /// <summary>
        /// Generic Inventory Opener
        /// </summary>
        /// <param name="aSelection">Which Item's selected</param>
        /// <param name="aOffset">How much the inventory has scrolled down</param>
        /// <param name="aType">Tells what type text should be sent in</param>
        /// <param name="aSetOpened">
        /// Asks if you want to set the open inventory to this specific one.
        /// Should be false if you're in subinventory of a normal inventory.
        /// </param>
        protected virtual void OpenInventory(int aSelection, int aOffset, TextType aType, bool aSetOpened)
        {
            Console.Clear();
            if(aSetOpened)
                Program.Player.InventoryOpened(this);
            mySelection = aSelection;
            myOffset = aOffset;

            var inventory = new Text[Math.Min(Content.Count + 4, 11)];
            var canScrollDown = Content.Count - myOffset - Console.WindowHeight - 5 > 0;
            var canScrollUp = myOffset > 0;
            inventory[0] = Header?.Replace("%money%", Program.Player.Money.ToString());
            inventory[1] = HeaderFoot;
            inventory[2] = new Text(canScrollUp ? "           ↑" : "            "); // Sätter en pil uppåt om man kan scrolla uppåt.
            for (var i = 0; i < Math.Min(Content.Count - myOffset, 6); i++) // Placerar alla items på deras plats 0 är högst
            {
                var item = Content.ElementAt(i);
                inventory[i + 3] = GetItemName(item, mySelection == i);
                inventory[i + 4] = new Text(canScrollDown ? "           ↓" : "            "); // Sätter en pil neråt om du kan scrolla neråt.
            }
            
            Player.Player.SendControls(Controls);
            TextUtils.SendMessage(inventory, aType);
        }

        /// <summary>
        /// If item is selected its <c>ColorCode.RESET</c> and it has markers
        /// around its name. Otherwise its RGB: 128,128,128 without markers.
        /// </summary>
        /// 
        /// <param name="aItem">Item to format</param>
        /// <param name="aSelected">True if the <paramref name="aItem"/> is selected</param>
        /// <returns><c>string</c> Formatted for use in the Inventory</returns>
        protected virtual Text GetItemName(BaseItem aItem, bool aSelected)
        {
            var itemName = $"[{aItem.Name}]";
            if (!aSelected)
                return new Text(("   " + itemName + "   ").PadRight(45) + $"({aItem.Value})", 128, 128, 128, true);
            const string prefix = ">> ";
            const string suffix = " <<";
            Selected = aItem;
            return new Text((prefix + itemName + suffix).PadRight(45) + $"({aItem.Value})", ColorCode.WHITE);
        }

        /// <summary>
        /// Add an Item to the inventory.
        /// 
        /// If the same Item already exists within the inventory increment the count.
        /// otherwise add it to the content list.
        /// </summary>
        /// <param name="aItem">Item to be added</param>
        public void AddItem(BaseItem aItem)
        {
            if(!TryChangeCount(aItem, 1))
                Content.Add(aItem);
        }
        
        public void RemoveItem(BaseItem aItem)
        {
            TryChangeCount(aItem, -1);
        }

        private bool TryChangeCount(BaseItem aItem, int amount)
        {
            // Den här kollar igenom om ett item har samma item namn i Content
            // Anledningen till att man inte kan använda contains eller find är för att
            // Count kommer vara olika på de olika objekten och kommer därför jämnföras med namn.
            foreach (var item in Content.Where(item => item.Name == aItem.Name))
            {
                item.Count += amount;
                if (item.Count <= 0)
                    Content.Remove(item);
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Exits out of the inventory or selected <c>BaseItem</c>
        /// </summary>
        public virtual void RunBackspaceAction()
        {
            // är Selected open så är selected oftast en shop och tar därför över events från.
            if(Selected != null && Selected.IsOpened)
            {
                Selected.ReturnEvent();
                OpenInventory(mySelection, myOffset, false);
            }else
                Program.Player.CloseInventory();
        }

        /// <summary>
        /// Scroll up in the inventory.
        /// </summary>
        public virtual void RunWAction()
        {
            var canScrollUp = myOffset > 0;
            if(canScrollUp && mySelection == 1)
            {
                ReloadInventory(mySelection, myOffset - 1, false);
            } else if(mySelection == 0)
            {
                ReloadInventory(0, myOffset, false);
            }else
                ReloadInventory(mySelection - 1, myOffset, false);
        }

        /// <summary>
        /// Scroll down in the inventory
        /// </summary>
        public virtual void RunSAction()
        {
            var canScrollDown = Content.Count - myOffset - (Console.WindowHeight - 5) > 0;
            
            // Den här bestämmer om du ska scrolla ner, byta selectionen så att den går ner lite eller bara stanna.
            if (canScrollDown && mySelection == 4)
            {
                ReloadInventory(mySelection, myOffset + 1, false);
            }else if (mySelection == Console.WindowHeight - 5 || mySelection == Content.Count - 1)
            {
                ReloadInventory(mySelection, myOffset, false);
            }else
                ReloadInventory(mySelection + 1, myOffset, false);
        }
        
        /// <summary>
        /// Control actions -> override in Inventory
        /// </summary>
        public virtual void RunInteractAction()
        {
            Selected?.InteractEvent();
        }

        public virtual void RunReloadAction()
        {
            ReloadInventory(mySelection, myOffset, false);
        }
    }
}
