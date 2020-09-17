using System;

namespace PotterGame.Player
{
    public static class PlayerController
    {
        /// <summary>
        /// Handles the players input.
        /// </summary>
        public static void MakeSelection()
        {
            while (true)
            {
                var key = Console.ReadKey(true).Key;

                if (Program.Player.IsInventoryOpen)
                {
                    var openInventory = Program.Player.OpenInventory;

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
                else if(!Program.Player.CurrentBattle.IsBattling)
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
                        case ConsoleKey.I:
                            story.RunInventoryAction();
                            break;
                    }
                }
                else
                {
                    var currentBattle = Program.Player.CurrentBattle;
                    
                    // DOESNT QUEUE THE KEY RRESS IF STUNNED
                    if (Program.Player.IsStunned())
                        continue;
                    
                    // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
                    switch (key)
                    {
                        case ConsoleKey.W:
                            currentBattle.UseProtego();
                            break;
                        case ConsoleKey.E:
                            currentBattle.UsePetrificus();
                            break;
                        case ConsoleKey.Q:
                            currentBattle.UseStupefy();
                            break;
                        case ConsoleKey.I:
                            currentBattle.OpenPlayerInventory();
                            break;
                    }
                }
            }
        }
    }
}
