using PotterGame.Inventories.Items;
using PotterGame.Inventories.Items.BankItems;
using PotterGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using PotterGame.Player;

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
        public void OpenInventory(int aSelection, int aOffset)
        {
            Console.Clear();
            if (Player == null)
                Player = Program.GetPlayer();
            Selection = aSelection;
            Offset = aOffset;

            var inventory = new Text[Math.Min(Content.Count + 4, 11)];
            var canScrollDown = (Content.Count - Offset) - (Console.WindowHeight - 5) > 0;
            var canScrollUp = Offset > 0;
            
            inventory[0] = new Text($"{Name}                           ({Player.Money})", ColorCode.RESET);
            inventory[1] = new Text("     Item                            Price");
            inventory[2] = new Text(canScrollUp ? "           ↑" : "            ");

            for (var i = 0; i < Math.Min(Content.Count - Offset, 6); i++)
            {
                var item = Content.ElementAt(i);
                inventory[i + 3] = new Text(GetItemName(item, Selection == i));
                inventory[i + 4] = new Text(canScrollDown ? "           ↓" : "            ");
            }
            PotterGame.Player.Player.SendPaused();
            PotterGame.Player.Player.SendControls(Controls);
            PotterGame.Player.Player.SendInventory(inventory);
        }

        /// <summary>
        /// Sends an updated inventory without clearing the console.
        /// Removes the flicker effect when scrolling through the items.
        /// </summary>
        /// <param name="aSelection">The selected row in the inventory</param>
        /// <param name="aOffset">The amount the inventory has scrolled down</param>
        private void ReloadInventory(int aSelection, int aOffset)
        {
            Player.InventoryOpened(this);
            Selection = aSelection;
            Offset = aOffset;

            var inventory = new Text[Math.Min(Content.Count + 4, 11)];
            var canScrollDown = (Content.Count - Offset) - (Console.WindowHeight - 5) > 0;
            var canScrollUp = Offset > 0;

            inventory[0] = new Text($"{Name}                           ({Player.Money})", ColorCode.RESET);
            inventory[1] = new Text("     Item                            Price");
            inventory[2] = new Text(canScrollUp ? "           ↑" : "            ");
            for (var i = 0; i < Math.Min(Content.Count - Offset, 6); i++)
            {
                var item = Content.ElementAt(i);
                inventory[i + 3] = new Text(GetItemName(item, Selection == i));
                inventory[i + 4] = new Text(canScrollDown ? "           ↓" : "            ");
            }
            PotterGame.Player.Player.SendInventory(inventory);
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
            Player.InventoryOpened(this);
            Selection = aSelection;
            Offset = aOffset;

            var inventory = new Text[Math.Min(2 + Content.Count + 2, 11)];

            inventory[0] = new Text($"Player                               ({Player.Money})");
            inventory[1] = new Text($"     Bank                            ({aBankMoney})");
            inventory[2] = new Text(GetItemName(new WithdrawItem(), Selection == 0).Substring(36));
            inventory[3] = new Text(GetItemName(new DepositItem(), Selection == 1).Substring(36));

            PotterGame.Player.Player.SendInventory(inventory);
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
            Player.InventoryOpened(this);
            Selection = aSelection;
            Offset = aOffset;

            var inventory = new Text[Math.Min(Content.Count + 4, 11)];

            inventory[0] = new Text($"Player                               ({Player.Money})");
            inventory[1] = new Text($"     Bank                            ({aBankMoney})");
            inventory[2] = new Text(GetItemName(new WithdrawItem(), Selection == 0).Substring(36));
            inventory[3] = new Text(GetItemName(new DepositItem(), Selection == 1).Substring(36));
            
            PotterGame.Player.Player.SendInventory(inventory);
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
            var prefix = "     ";
            var maxSuffix = "                              ";
            var itemName = $"[{aItem.Name}]";
            if (!aSelected)
                return new Text(
                    prefix + itemName + maxSuffix.Substring(0, maxSuffix.Length - aItem.Name.Length) +
                    $"({aItem.Value})", 128, 128, 128, true).Message;
            prefix = "  >> ";
            maxSuffix = " <<                           ";
            Selected = aItem;
            return new Text(prefix + itemName + maxSuffix.Substring(0, maxSuffix.Length - aItem.Name.Length) + $"({aItem.Value})", ColorCode.WHITE).Message;

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
        /// Lets you make another selection.
        /// </summary>
        private static void MakeSelection()
        {
            PlayerController.MakeSelection();
        }

        /// <summary>
        /// Exits out of the inventory or selected <c>BaseItem</c>
        /// </summary>
        public void RunBackspaceAction()
        {
            if(Selected.IsOpened)
            {
                Selected.ReturnEvent();
                OpenInventory(Selection, Offset);
                MakeSelection();
                return;
            }
            Player.CloseInventory();
            MakeSelection();
            
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
            MakeSelection();
            
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
            MakeSelection();
            
        }
        
        /// <summary>
        /// Control actions -> override in Inventory
        /// </summary>
        public virtual void RunInteractAction()
        {
            Selected.InteractEvent();
            MakeSelection();
            
        }

        public void RunReloadAction()
        {
            ReloadInventory(Selection, Offset);
            MakeSelection();
            
        }
    }
}
