using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using PotterGame.Inventories;

namespace PotterGame
{
    internal class Program
    {

        #region Kernell Imports
        
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

        public static Program Instance;

        public static Player.Player Player;

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

            Instance = new Program();

            Console.CursorVisible = false;
            Player = new Player.Player();
            Player.PlayerSetup();
            Player.StartMenu();
        }

        /// <summary>
        /// This maximizes the window.
        /// </summary>
        private static void Maximize()
        {
            var p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); 
            //SW_MAXIMIZE = 3
        }
    }
}
