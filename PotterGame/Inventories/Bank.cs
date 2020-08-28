using PotterGame;
using PotterGame.Inventories;
using PotterGame.Inventories.Items;
using PotterGame.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Bank : BaseInventory
    {

        //Player                            (Money)
        //    Bank                          (Money)                      
        //   [Withdraw]
        //   [Deposit]
        
        // Det här är vad inventoryt borde se ut i slutändan.

        private int myMoney = 250;

        public Bank(String name)
        {
            myName = name;
            content = new List<IBaseItem>();
        }

        public override void RunWAction()
        {
            bool canScrollUp = Offset > 0;
            if(canScrollUp && Selection == 1)
            {
                OpenBankInventory(Selection, Offset - 1, myMoney);
                return;
            }
            if(Selection == 0)
            {
                OpenBankInventory(0, Offset, myMoney);
            }
            OpenBankInventory(Selection - 1, Offset, myMoney);

        }

        public override void RunSAction()
        {
            bool canScrollDown = (content.Count - Offset) - 6 > 0;
            if (canScrollDown && Selection == 4)
            {
                OpenBankInventory(Selection, Offset + 1, myMoney);
                return;
            }
            if (Selection == 5 || Selection == content.Count - 1)
            {
                OpenBankInventory(Selection, Offset, myMoney);
                return;
            }
            OpenBankInventory(Selection + 1, Offset, myMoney);

        }

    }
}
