using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories.Items.BankItems
{
    public class WithdrawItem : BaseItem
    {
        public bool IsOpened { get; set; }

        public int Value { get; } = 0;
        public int Count { get; set; } = 1;

        public string Name { get; } = "Deposit";
        public string Controls { get; } = "    [ENTER] - Withdraw                      [BACKSPACE] - Back    ";

        public override void InteractEvent()
        {
            
        }

        public override void ReturnEvent()
        {
            
        }
    }
}
