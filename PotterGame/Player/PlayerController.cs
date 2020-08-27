using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Player
{
    class PlayerController
    {

        public PlayerController()
        {
            MakeSelection();
        }

        public void MakeSelection()
        {
            ConsoleKey key = Console.ReadKey().Key;

            if(Program.getPlayer().IsInventoryOpen())
            {
                if(key.Equals(ConsoleKey.Enter))
                {
                    Program.getPlayer().GetOpenInventory().RunInteractAction();
                }else if(key.Equals(ConsoleKey.Backspace))
                {
                    Program.getPlayer().GetOpenInventory().RunBackspaceAction();
                }else if(key.Equals(ConsoleKey.W))
                {
                    Program.getPlayer().GetOpenInventory().RunWAction();
                }else if(key.Equals(ConsoleKey.S))
                {
                    Program.getPlayer().GetOpenInventory().RunSAction();
                }
            }
            else
            {

            }

            MakeSelection();
        }

    }
}
