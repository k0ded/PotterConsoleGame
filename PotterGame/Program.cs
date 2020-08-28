﻿using ConsoleApp;
using PotterGame.Inventories.Items;
using PotterGame.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotterGame
{
    class Program
    {

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        static Player.Player player = new Player.Player();

        static void Main(string[] args)
        {
            // Ser till så att pilarna i inventoryt inte visas som lådor
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleHelper.SetConsoleFont();
            Maximize();
            
        }

        public void startTicking()
        {
            shouldTick = true;
            tick();
        }

        public void stopTicking()
        {
            shouldTick = false;
        }


        bool shouldTick = true;
        void tick()
        {
            while(true)
            {
                if (!shouldTick)
                    break;

                getPlayer().GetContext().tick();

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
            return player;
        }

        internal static Shop getShop(IBaseItem item)
        {
            throw new NotImplementedException();
        }
    }
}
