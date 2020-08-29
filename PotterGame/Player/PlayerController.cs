using PotterGame.Utils;
using System;

namespace PotterGame.Player
{
    public static class PlayerController
    {

        /// <summary>
        /// Handles the input of the player.
        /// </summary>
        public static void MakeSelection()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new Text("", 12, 12, 12, false).Message);
            var key = Console.ReadKey(true).Key;

            if (Program.GetPlayer().IsInventoryOpen)
            {
                var openInventory = Program.GetPlayer().OpenInventory;

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
            else
            {
                var story = Program.GetPlayer().Context;

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
                        story.RunSAction();
                        break;
                    case ConsoleKey.Q:
                        story.RunQAction();
                        break;
                    case ConsoleKey.I:
                        story.RunInventoryAction();
                        break;
                }
            }
        }

    }
}
