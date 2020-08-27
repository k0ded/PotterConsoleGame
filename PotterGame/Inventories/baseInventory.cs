using PotterGame.Inventories.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories
{
    class baseInventory
    {
        public  List<baseItem> content;

        protected void SendMenu(string[] message)
        {
            Program.getPlayer().SendMenu(message);
        }

        public void AddItem(baseItem i)
        {
            foreach (baseItem item in content)
            {
                if (item == null)
                    break;
                if (item.GetName() == i.GetName())
                {
                    item.SetCount(item.GetCount() + 1);
                    return;
                }
            }
            content.Add(i);
        }

        public virtual void RunInteractAction()
        {

        }

        public virtual void RunBackspaceAction()
        {
            Program.getPlayer().CloseInventory();
        }

        public virtual void RunWAction()
        {

        }

        public virtual void RunSAction()
        {

        }
    }
}
