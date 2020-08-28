using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories.Items
{
    public interface IBaseItem
    {
        int Value { get; set; }
        int Count { get; set; }
        string Name { get; set; }

        void InteractEvent();
    }
}
