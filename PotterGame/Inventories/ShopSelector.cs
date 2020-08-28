using ConsoleApp;
using PotterGame;
using PotterGame.Inventories;
using PotterGame.Inventories.Items;
using PotterGame.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class ShopSelector : BaseInventory
    {
        String name;

        Shop selected;

        public ShopSelector(String name)
        {
            this.name = name;
        }

        //Används när inventoryt ska öppnas. Den andra används för att man ska kunna scrolla.
        public void OpenInventory()
        {
            OpenInventory(0, 0);
        }

        public override void RunWAction()
        {
            bool canScrollUp = Offset > 0;
            if (canScrollUp && Selection == 1)
            {
                OpenInventory(Selection, Offset - 1);
                return;
            }
            if (Selection == 0)
            {
                OpenInventory(0, Offset);
            }
            OpenInventory(Selection - 1, Offset);

        }

        public override void RunSAction()
        {
            bool canScrollDown = (content.Count - Offset) - 6 > 0;
            if (canScrollDown && Selection == 4)
            {
                OpenInventory(Selection, Offset + 1);
                return;
            }
            if (Selection == 5 || Selection == content.Count - 1)
            {
                OpenInventory(Selection, Offset);
                return;
            }
            OpenInventory(Selection + 1, Offset);

        }

        public override void RunInteractAction()
        {
            selected.openInventory();
        }

    }
}
