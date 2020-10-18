using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories.Items.BankItems
{
    public class WithdrawItem : BaseItem
    {
        public WithdrawItem() : base("Withdraw", 0)
        {
            Controls = "    [ENTER] - Withdraw                      [BACKSPACE] - Back    ";
        }

        public override void InteractEvent()
        {
            
        }

        public override void ReturnEvent()
        {
            
        }
    }
}
