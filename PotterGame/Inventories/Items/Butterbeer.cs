using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories.Items
{
    class Butterbeer : IBaseItem
    {
        public bool IsOpened { get; set; } = false;

        public int Value { get; } = 7;
        public int Count { get; set; } = 1;
        public string Name { get; } = "Butterbeer";
        public string Controls { get; } =   "    [ENTER] - Consume                       [BACKSPACE] - Back    ";

        public void InteractEvent()
        {
                   
        }

        public void ReturnEvent()
        {
            IsOpened = false;
        }
    }
}
