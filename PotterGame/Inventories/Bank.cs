using System.Collections.Generic;
using PotterGame.Inventories.Items;

namespace PotterGame.Inventories
{
    internal class Bank : BaseInventory
    {

        private int Money { get; set; } = 250;

        public Bank(string name)
        {
            Name = name;
            Content = new List<BaseItem>();
            Player = Program.GetPlayer();
        }
        
        public override void OpenInventory()
        {
            if (Player == null)
                Player = Program.GetPlayer();
            Player.InventoryOpened(this);
            OpenInventory(0,0);
        }

        /// <summary>
        /// Scroll up in the inventory.
        /// </summary>
        public override void RunWAction()
        {
            var canScrollUp = Offset > 0;
            if(canScrollUp && Selection == 1)
            {
                ReloadBankInventory(Selection, Offset - 1, Money);
                return;
            }
            if(Selection == 0)
            {
                ReloadBankInventory(0, Offset, Money);
            }
            ReloadBankInventory(Selection - 1, Offset, Money);

        }

        /// <summary>
        /// Scroll down in the inventory.
        /// </summary>
        public override void RunSAction()
        {
            var canScrollDown = (Content.Count - Offset) - 6 > 0;
            if (canScrollDown && Selection == 4)
            {
                ReloadBankInventory(Selection, Offset + 1, Money);
                return;
            }
            if (Selection == 5 || Selection == Content.Count - 1)
            {
                ReloadBankInventory(Selection, Offset, Money);
                return;
            }
            ReloadBankInventory(Selection + 1, Offset, Money);

        }

    }
}
