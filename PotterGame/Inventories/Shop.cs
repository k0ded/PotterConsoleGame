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
    class Shop : BaseInventory
    {
        String name;

        public Shop(String name)
        {
            this.name = name;
            content = new List<IBaseItem>();
        }

        //Används när inventoryt ska öppnas. Den andra används för att man ska kunna scrolla.
        public void openInventory()
        {
            OpenInventory(0, 0);
        }

        internal string GetName()
        {
            return name;
        }

        public void addItem(IBaseItem i)
        {
            foreach (IBaseItem item in content)
            {
                if (item == null)
                    break;
                if (item.Name == i.Name)
                {
                    item.Count++;
                    return;
                }
            }
            content.Add(i);
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
            if(Selected.Value <= Program.getPlayer().GetMoney())
            {
                Program.getPlayer().RemoveMoney(Selected.Value);
                Program.getPlayer().GetPlayerInventory().AddItem(Selected);
                Console.WriteLine("+1 " + Selected.Name);
            }
        }

    }
}
