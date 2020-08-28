using PotterGame.Inventories.Items;
using PotterGame.Inventories.Items.BankItems;
using PotterGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotterGame.Inventories
{
    class BaseInventory
    {
        public string Controls { get; } = "[W/S] - Scroll        [ENTER] - Interact        [BACKSPACE] - Back";

        public  List<IBaseItem> content;

        public int Selection = 0;
        public int Offset = 0;
        public IBaseItem Selected;
       
        protected String myName;

        private Player.Player myPlayer = Program.getPlayer();

        public void OpenInventory(int aSelection, int aOffset)
        {
            myPlayer.OpenInventory(this);
            Selection = aSelection;
            Offset = aOffset;
            
            Text[] inventory = new Text[Math.Min(2 + content.Count + 2, 11)];
            bool canScrollDown = (content.Count - Offset) - (Console.WindowHeight - 5) > 0;
            bool canScrollUp = Offset > 0;

            inventory[0] = new Text($"{myName}                                💰 ".Substring(myName.Length, myName.Length * 2) + $"({myPlayer.GetMoney()})");
            inventory[1] = new Text("     Item                           Price");
            inventory[2] = new Text(canScrollUp ? "           ↑" : "            ");

            for (var i = 0; i < Math.Min(inventory.Length - Offset, 6); i++)
            {
                IBaseItem item = content.ElementAt(i);
                inventory[i + 3] = new Text(GetItemName(item, Selection == i));
                inventory[i + 4] = new Text(canScrollDown ? "           ↓" : "            ");
            }

            myPlayer.SendInventory(inventory);
        }

        public void OpenBankInventory(int aSelection, int aOffset, int aBankMoney)
        {
            myPlayer.OpenInventory(this);
            Selection = aSelection;
            Offset = aOffset;

            Text[] inventory = new Text[Math.Min(2 + content.Count + 2, 11)];
            bool canScrollDown = (content.Count - Offset) - (Console.WindowHeight - 5) > 0;
            bool canScrollUp = Offset > 0;

            string myMoneyBag = new Text("💰", ColorCode.YELLOW, true).Message;

            inventory[0] = new Text($"Player                            {myMoneyBag} ({myPlayer.GetMoney()})");
            inventory[1] = new Text($"     Bank                         {myMoneyBag} ({aBankMoney})");
            inventory[2] = new Text(GetItemName(new WithdrawItem(), Selection == 0).Substring(36));
            inventory[3] = new Text(GetItemName(new DepositItem(), Selection == 1).Substring(36));

            myPlayer.SendInventory(inventory);
        }

        private String GetItemName(IBaseItem aItem, bool aSelected)
        {
            String prefix = new Text("     ", ColorCode.BLACK, true);
            String maxSuffix = "                              ";
            String itemName = $"[{aItem.Name}]";
            if (aSelected)
            {
                prefix = new Text("  >> ", ColorCode.WHITE, false).Message;
                maxSuffix = " <<                           ";
                Selected = aItem;
            }

            String s = "     ";
            s = s.Substring(0, Math.Max(s.Length - String.Concat(aItem.Value).Length, 0));

            return prefix + itemName + maxSuffix.Substring(0, maxSuffix.Length - aItem.Name.Length) + $"({aItem.Value})";
        }

        public void AddItem(IBaseItem aItem)
        {
            foreach (IBaseItem item in content)
            {
                if (item == null)
                    break;
                if (item.Name == aItem.Name)
                {
                    item.Count++;
                    return;
                }
            }
            content.Add(aItem);
        }

        public virtual void RunInteractAction()
        {
            Selected.InteractEvent();
        }

        public virtual void RunBackspaceAction()
        {
            if(Selected.IsOpened)
            {
                Selected.ReturnEvent();
                OpenInventory(Selection, Offset);
                return;
            }
            myPlayer.CloseInventory();
        }

        public virtual void RunWAction()
        {

        }

        public virtual void RunSAction()
        {

        }
    }
}
