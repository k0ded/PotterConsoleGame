using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using PotterGame.Utils.Dungeons;
using PotterGame.Utils.Text;

namespace PotterGame
{
    internal class Program
    {

        #region DllImports
        
        /// <summary>
        /// Makes ANSI-Color Codes Possible - STOLEN FROM GOOGLE
        /// </summary>
        private const int StdOutputHandle = -11;
        private const uint EnableVirtualTerminalProcessing = 0x0004;
        private const uint DisableNewlineAutoReturn = 0x0008;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        
        #endregion

        public static Player.Player Player;
        public static DungeonManager Manager;

        private static Stopwatch myStopwatch = new Stopwatch();

        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.BufferHeight = Console.WindowHeight;

            // These lines maximize the window and allows ANSI Colors to be displayed! - Stolen from Google
            Maximize();
            var iStdOut = GetStdHandle(StdOutputHandle);
            if (!GetConsoleMode(iStdOut, out var outConsoleMode))
            {
                Console.WriteLine("failed to get output console mode");
                Console.ReadKey();
                return;
            }
            outConsoleMode |= EnableVirtualTerminalProcessing | DisableNewlineAutoReturn;
            if (!SetConsoleMode(iStdOut, outConsoleMode))
            {
                Console.WriteLine($"failed to set output console mode, error code: {GetLastError()}");
                Console.ReadKey();
                return;
            }

            Console.CursorVisible = false;
            Manager = new DungeonManager();
            Player = new Player.Player();
            Restart();
        }

        /// <summary>
        /// Den här funktionen startar själva spelet när allt som behövs är instantierat
        /// </summary>
        public static void Restart()
        {
            myStopwatch.Start();
            Player.StartMenu();
        }

        /// <summary>
        /// This maximizes the window.
        /// </summary>
        public static void Maximize()
        {
            var p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); 
            //SW_MAXIMIZE = 3
        }

        public static void SendWinMessage()
        {
            Console.Clear();
            myStopwatch.Stop();
            Player.SeizeInput = true;
            var winMessage = new [] {
                new Text("You win!"),
                new Text($"It only took you: {myStopwatch.Elapsed.ToString()}")
            };
            TextUtils.SendMessage(winMessage, TextType.CENTERED);
            TextUtils.SendMessage(new Text("Press Enter to continue"), TextType.CONTROLS);
            Console.ReadKey();
            Restart();
        }
    }
}
