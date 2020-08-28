using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories.Items
{
    class Butterbeer : IBaseItem
    {
        public int Value { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }

        public void InteractEvent()
        {
            string[] butterbeer = { "BUTTERBEER - DEBUG" };
            Program.getPlayer().SendMenu(butterbeer);

        }
    }
}
