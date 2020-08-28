using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories.Items
{
    public interface IBaseItem
    {
        bool IsOpened { get; set; }
        int Value { get; }
        int Count { get; set; }
        string Name { get; }
        string Controls { get; }

        void InteractEvent();
        void ReturnEvent();
    }
}
