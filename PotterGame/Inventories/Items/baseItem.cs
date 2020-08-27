using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories.Items
{
    abstract class baseItem
    {
        int value = 0;
        int count = 1;
        string name;

        public int GetPrice()
        {
            return value;
        }

        public int GetCount()
        {
            return count;
        }

        public void SetCount(int i)
        {
            count = i;
        }

        public string GetName()
        {
            return name;
        }

        public abstract void InteractEvent();
    }
}
