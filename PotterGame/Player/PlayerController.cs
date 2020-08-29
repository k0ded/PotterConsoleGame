﻿using PotterGame.Inventories;
using PotterGame.Player.Story;
using PotterGame.Utils;
using System;
using System.Threading;

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
            ConsoleKey key = Console.ReadKey(true).Key;

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
                BaseContext Story = Program.getPlayer().GetContext();

                if (key.Equals(ConsoleKey.Enter))
                {
                    Story.RunInteractAction();
                }
                else if (key.Equals(ConsoleKey.Backspace))
                {
                    Story.RunBackspaceAction();
                }
                else if (key.Equals(ConsoleKey.W))
                {
                    Story.RunWAction();
                }
                else if (key.Equals(ConsoleKey.E))
                {
                    Story.RunSAction();
                }
                else if(key.Equals(ConsoleKey.Q))
                {
                    Story.RunQAction();
                }
            }

            MakeSelection();
        }

    }
}
