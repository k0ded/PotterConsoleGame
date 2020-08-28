using PotterGame;
using PotterGame.Inventories;
using PotterGame.Inventories.Items;
using PotterGame.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Inventory : BaseInventory
    {

        //Inventory                       💰 (Money)
        //     Item                           Value  Count
        //           ↑
        //     []                              (0)    (1)
        //  >> [Charms Book] <<                (50)   (1)
        //     [Butterbeer]                    (5)    (3)
        //     [Ticket from Ollivanders]       (0)    (1)
        //     [Charms Book]                   (50)   (1)
        //     [Butterbeer]                    (5)    (7)
        //           ↓

        // Det här är vad inventoryt borde se ut i slutändan.

        public Inventory(String aName)
        {
            myName = aName;
            content = new List<IBaseItem>();
        }

        //Används när inventoryt ska öppnas. Den andra används för att man ska kunna scrolla.
        public void openInventory()
        {
            OpenInventory(0, 0);
        }

        public override void RunWAction()
        {
            bool canScrollUp = Offset > 0;
            if(canScrollUp && Selection == 1)
            {
                OpenInventory(Selection, Offset - 1);
                return;
            }
            if(Selection == 0)
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

    }
}
