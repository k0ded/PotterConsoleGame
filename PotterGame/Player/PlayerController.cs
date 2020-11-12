using System;
using PotterGame.Inventories;

namespace PotterGame.Player
{
    public static class PlayerController
    {
        /// <summary>
        /// Handles the players input.
        /// </summary>
        public static void MakeSelection()
        {
            while (true) // Den här loopen körs förevigt då den används för alla olika inputs
            {
                var key = Console.ReadKey(true).Key;
                
                
                // Inventory input
                if (InventoryManager.IsInventoryOpen)
                {
                    var openInventory = InventoryManager.OpenInventory;

                    // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            openInventory.RunInteractAction();
                            break;
                        case ConsoleKey.Backspace:
                            openInventory.RunBackspaceAction();
                            break;
                        case ConsoleKey.W:
                            openInventory.RunWAction();
                            break;
                        case ConsoleKey.S:
                            openInventory.RunSAction();
                            break;
                        default:
                            openInventory.RunReloadAction();
                            break;
                    }
                }
                // Exploration input
                else
                {
                    var story = Program.Player.Context;

                    // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            story.RunInteractAction();
                            break;
                        case ConsoleKey.Backspace:
                            story.RunBackspaceAction();
                            break;
                        case ConsoleKey.W:
                            story.RunWAction();
                            break;
                        case ConsoleKey.E:
                            story.RunEAction();
                            break;
                        case ConsoleKey.Q:
                            story.RunQAction();
                            break;
                        case ConsoleKey.A:
                            story.RunAAction();
                            break;
                        case ConsoleKey.S:
                            story.RunSAction();
                            break;
                        case ConsoleKey.D:
                            story.RunDAction();
                            break;
                        case ConsoleKey.I:
                            story.RunInventoryAction();
                            break;
                    }
                }
            }
        }
    }
}
