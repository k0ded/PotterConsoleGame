using ConsoleApp;
using PotterGame.Inventories.Items;
using PotterGame.Utils;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace PotterGame
{
    class Program
    {

        // Det här gör ANSI färger & maximerat fönster möjligt! - Snott från Google
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        

        public static TextUtils TextUtils = new TextUtils();
        private static Player.Player myPlayer;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.BufferHeight = Console.WindowHeight;


            // Det här gör ANSI färger & maximerat fönster möjligt! - Snott från Google
            Maximize();
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
            {
                Console.WriteLine("failed to get output console mode");
                Console.ReadKey();
                return;
            }
            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            if (!SetConsoleMode(iStdOut, outConsoleMode))
            {
                Console.WriteLine($"failed to set output console mode, error code: {GetLastError()}");
                Console.ReadKey();
                return;
            }

            Program p = new Program();

            Console.CursorVisible = false;

            myPlayer = new Player.Player();
            myPlayer.Start();
        }

        public void StartTicking()
        {
            shouldTick = true;
            Tick();
        }

        public void StopTicking()
        {
            shouldTick = false;
        }


        bool shouldTick = true;
        private void Tick()
        {
            while(true)
            {
                if (!shouldTick)
                    break;

                getPlayer().GetContext().Tick();

                Thread.Sleep(1000 / 128);
            }
        }

        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }

        public static Player.Player getPlayer()
        {
            return myPlayer;
        }

        internal static Shop getShop(IBaseItem item)
        {
            throw new NotImplementedException();
        }
    }
}
