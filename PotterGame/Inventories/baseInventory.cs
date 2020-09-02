using PotterGame.Inventories.Items;
using PotterGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotterGame.Inventories
{
    public class BaseInventory
    {
        private string Controls { get; } = "[W/S] - Scroll        [ENTER] - Interact        [BACKSPACE] - Back";
        
        protected List<IBaseItem> Content;
        protected int Selection;
        protected int Offset;
        protected IBaseItem Selected;

        protected string Name = "DEFAULT";
        protected Player.Player Player { get; set; }

        /// <summary>
        /// Opens a regular inventory.
        /// </summary>
        /// <param name="aSelection">The selected row in the inventory</param>
        /// <param name="aOffset">The amount the inventory has scrolled down</param>
        protected void OpenInventory(int aSelection, int aOffset)
        {
            var header = new Text("Player".PadRight(45).PadLeft(0) + $"({Player.Money})");
            var headerFoot = new Text("     Item".PadRight(45) + "Price");
            OpenInventory(header, headerFoot, aSelection, aOffset);
        }

        public virtual void OpenInventory()
        {
            if (Player == null)
                Player = Program.GetPlayer();
            Player.InventoryOpened(this);
            OpenInventory(0,0);
        }

        /// <summary>
        /// Sends an updated inventory without clearing the console.
        /// Removes the flicker effect when scrolling through the items.
        /// </summary>
        /// <param name="aSelection">The selected row in the inventory</param>
        /// <param name="aOffset">The amount the inventory has scrolled down</param>
        private void ReloadInventory(int aSelection, int aOffset)
        {
            var header = new Text("Player".PadRight(45) + $"({Player.Money})");
            var headerFoot = new Text("     Item".PadRight(45) + "Price");
            OpenInventory(header, headerFoot, aSelection, aOffset);
        }

        /// <summary>
        /// Opens an inventory of the Bank type. Changes the header.
        /// </summary>
        /// 
        /// <param name="aSelection">The selected row in the inventory</param>
        /// <param name="aOffset">The amount the inventory has scrolled down</param>
        /// <param name="aBankMoney">The amount of money the bank has</param>
        protected void OpenBankInventory(int aSelection, int aOffset, int aBankMoney)
        {
            Console.Clear();
            var header = new Text("Player".PadRight(45) + $"({Player.Money})");
            var headerFoot = new Text("     Bank".PadRight(45) + $"({aBankMoney})");
            OpenInventory(header, headerFoot, aSelection, aOffset);
        }

        /// <summary>
        /// Sends an updated inventory without clearing the console.
        /// Removes the flicker effect when scrolling through the items.
        /// </summary>
        /// <param name="aSelection">The selected row in the inventory</param>
        /// <param name="aOffset">The amount the inventory has scrolled down</param>
        /// <param name="aBankMoney">The amount of money the bank has</param>
        protected void ReloadBankInventory(int aSelection, int aOffset, int aBankMoney)
        {
            var header = new Text("Player".PadRight(45) + $"({Player.Money})");
            var headerFoot = new Text("     Bank".PadRight(45) + $"({aBankMoney})");
            OpenInventory(header, headerFoot, aSelection, aOffset);
        }

        /// <summary>
        /// Generic Inventory Opener
        /// </summary>
        /// <param name="aHeader">A Non Null <c>Text</c> object displayed on the first line of the inventory menu</param>
        /// <param name="aHeaderFoot">A Non Null <c>Text</c> object displayed on the second line of the inventory menu</param>
        /// <param name="aSelection">Which Item's selected</param>
        /// <param name="aOffset">How much the inventory has scrolled down</param>
        private void OpenInventory(Text aHeader, Text aHeaderFoot, int aSelection, int aOffset)
        {
            Console.Clear();
            if (Player == null)
                Player = Program.GetPlayer();
            Selection = aSelection;
            Offset = aOffset;

            var inventory = new Text[Math.Min(Content.Count + 4, 11)];
            var canScrollDown = (Content.Count - Offset) - (Console.WindowHeight - 5) > 0;
            var canScrollUp = Offset > 0;

            inventory[0] = aHeader;
            inventory[1] = aHeaderFoot;
            inventory[2] = new Text(canScrollUp ? "           ↑" : "            ");
            for (var i = 0; i < Math.Min(Content.Count - Offset, 6); i++)
            {
                var item = Content.ElementAt(i);
                inventory[i + 3] = new Text(GetItemName(item, Selection == i));
                inventory[i + 4] = new Text(canScrollDown ? "           ↓" : "            ");
            }
            
            PotterGame.Player.Player.SendPaused();
            PotterGame.Player.Player.SendControls(Controls);
            TextUtils.SendMessage(inventory, TextType.INVENTORY);
        }

        /// <summary>
        /// If item is selected its <c>ColorCode.RESET</c> and it has markers
        /// around its name. Otherwise its RGB: 128,128,128 without markers.
        /// </summary>
        /// 
        /// <param name="aItem">Item to format</param>
        /// <param name="aSelected">True if the <paramref name="aItem"/> is selected</param>
        /// <returns><c>string</c> Formatted for use in the Inventory</returns>
        private string GetItemName(IBaseItem aItem, bool aSelected)
        {
            var itemName = $"[{aItem.Name}]";
            if (!aSelected)
                return new Text(itemName.PadRight(30) + $"({aItem.Value})", 128, 128, 128, true).Message;
            const string prefix = ">> ";
            const string suffix = " <<";
            Selected = aItem;
            return new Text((prefix + itemName + suffix).PadRight(30) + $"({aItem.Value})", ColorCode.WHITE).Message;

        }

        /// <summary>
        /// Add an Item to the inventory.
        /// 
        /// If the same Item already exists within the inventory increment the count.
        /// otherwise add it to the content list.
        /// </summary>
        /// <param name="aItem">Item to be added</param>
        public void AddItem(IBaseItem aItem)
        {
            foreach (var item in Content)
            {
                if (item == null)
                {
                    Content = new List<IBaseItem>(1000);   
                    break;
                }

                if (item.Name != aItem.Name) continue;
                item.Count++;
                return;
            }
            Content.Add(aItem);
        }
        /// <summary>
        /// Exits out of the inventory or selected <c>BaseItem</c>
        /// </summary>
        public void RunBackspaceAction()
        {
            if(Selected != null && Selected.IsOpened)
            {
                Selected.ReturnEvent();
                OpenInventory(Selection, Offset);
                return;
            }

            if (Player.CurrentBattle.Enemy != null)
            {
                return;
            }
            TextUtils.SendMessage(new Text("CLOSING INVENTORY"), TextType.DEBUG);
            Player.CloseInventory();
            PotterGame.Player.Player.SendContext(Player.Context);
        }

        /// <summary>
        /// Scroll up in the inventory.
        /// </summary>
        public virtual void RunWAction()
        {
            var canScrollUp = Offset > 0;
            if(canScrollUp && Selection == 1)
            {
                ReloadInventory(Selection, Offset - 1);
                return;
            }
            if(Selection == 0)
            {
                ReloadInventory(0, Offset);
                return;
            }
            ReloadInventory(Selection - 1, Offset);

        }

        /// <summary>
        /// Scroll down in the inventory
        /// </summary>
        public virtual void RunSAction()
        {
            var canScrollDown = (Content.Count - Offset) - (Console.WindowHeight - 5) > 0;
            if (canScrollDown && Selection == 4)
            {
                ReloadInventory(Selection, Offset + 1);
                return;
            }
            if (Selection == Console.WindowHeight - 5 || Selection == Content.Count - 1)
            {
                ReloadInventory(Selection, Offset);
                return;
            }
            ReloadInventory(Selection + 1, Offset);

        }
        
        /// <summary>
        /// Control actions -> override in Inventory
        /// </summary>
        public virtual void RunInteractAction()
        {
            Selected.InteractEvent();

        }

        public void RunReloadAction()
        {
            ReloadInventory(Selection, Offset);

        }
    }
}
