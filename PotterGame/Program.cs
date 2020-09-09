using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

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

        private static Player.Player _player;

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
            _player = new Player.Player();
            _player.Start();
        }

        #region Ticking
        
        private Thread myThread;
        private bool myShouldTick;
        
        /// <summary>
        /// Starts ticking realtime battles
        /// </summary>
        public void StartTicking()
        {
            if(myShouldTick)
                throw new Exception("Cannot start ticking when already ticking. Stop ticking when not needed");
            myShouldTick = true;
            myThread = new Thread(Tick);
            myThread.IsBackground = true;
            myThread.Start();
        }

        /// <summary>
        /// Stops ticking realtime battles
        /// *Used for pausing the game/going into the inventory*
        /// </summary>
        public void StopTicking()
        {
            if (!myShouldTick)
                return;
            myShouldTick = false;
            myThread.Interrupt();
            myThread.Abort();

            myThread = null;
        }
        
        /// <summary>
        /// Ticks realtime battles
        /// </summary>
        private void Tick()
        {
            var i = 0;
            while(myShouldTick)
            {
                GetPlayer().CurrentBattle.Tick();
                
                //TextUtils.SendMessage(new Text("Ticking: " + i, ColorCode.RESET), TextType.DEBUG);
                Thread.Sleep(1000 / 20);
                i++;
            }
        }
        
        #endregion

        /// <summary>
        /// This maximizes the window.
        /// </summary>
        private static void Maximize()
        {
            var p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); 
            //SW_MAXIMIZE = 3
        }

        /// <summary>
        /// Gives you the instance of the Player playing the game to get certain values
        /// </summary>
        /// <returns><c>Player.Player</c>, The player playing the game.</returns>
        public static Player.Player GetPlayer()
        {
            return _player;
        }
    }
}
