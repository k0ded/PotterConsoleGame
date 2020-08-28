using PotterGame.Inventories;
using PotterGame.Utils;
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
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new Text("", 12, 12, 12, false).Message);
            ConsoleKey key = Console.ReadKey().Key;

            if (Program.getPlayer().IsInventoryOpen)
            {
                BaseInventory openInventory = Program.getPlayer().OpenInventory;

                if (key.Equals(ConsoleKey.Enter))
                {
                    openInventory.RunInteractAction();
                }else if(key.Equals(ConsoleKey.Backspace))
                {
                    openInventory.RunBackspaceAction();
                }else if(key.Equals(ConsoleKey.W))
                {
                    openInventory.RunWAction();
                }else if(key.Equals(ConsoleKey.S))
                {
                    openInventory.RunSAction();
                }
                else
                {
                    openInventory.RunReloadAction();
                }
            }
            else
            {

            }

            MakeSelection();
        }

    }
}
