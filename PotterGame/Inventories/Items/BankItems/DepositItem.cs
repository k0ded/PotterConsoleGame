﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories.Items.BankItems
{
    public class DepositItem : BaseItem
    {
        public DepositItem() : base("Deposit", 0)
        {
            Controls = "    [ENTER] - Deposit                       [BACKSPACE] - Back    ";
        }
        
        public override void InteractEvent()
        {
            
        }

        public override void ReturnEvent()
        {
            
        }
    }
}
