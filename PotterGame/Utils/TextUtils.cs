using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PotterGame.Utils
{
    public class TextUtils
    {

        public void SendCenteredMessage(Text[] aMessage)
        {
            for (var i = 0;i < aMessage.Length; i++)
            {
                int x = Console.WindowWidth / 2 - aMessage[i].OriginalMessage.Length / 2;
                int y = Console.WindowHeight / 2 - aMessage.Length / 2 + i;
                Console.SetCursorPosition(x, y);
                Console.WriteLine(aMessage[i].Message);
            }

        }

        public void SendExplorationMessage(Text[] aMessage)
        {
            int LargestStringLength = 0;
            foreach (Text m in aMessage)
            {
                LargestStringLength = m.OriginalMessage.Length > LargestStringLength ? m.OriginalMessage.Length : LargestStringLength;
            }

            for (var i = 0; i < aMessage.Length; i++)
            {
                int x = Console.WindowWidth / 2 - LargestStringLength / 2;
                int y = Console.WindowHeight / 2 - aMessage.Length / 2 + i;
                Console.SetCursorPosition(x, y);
                Console.WriteLine(aMessage[i].Message);
            }

        }

        public void SendInventoryMessage(Text[] aMessage)
        {
            for (var i = 0; i < aMessage.Length; i++)
            {
                int x = 2;
                int y = Math.Min(i, Console.WindowHeight - 2);
                Console.SetCursorPosition(x, y);
                Console.WriteLine(aMessage[i].Message);
            }
        }

        public void SendControlsMessage(Text aControls)
        {
            int x = Console.WindowWidth / 2 - aControls.OriginalMessage.Length / 2;
            int y = 62;
            Console.SetCursorPosition(x, y);
            Console.Write(aControls.Message);
        }

        public void SendLetterMessage(Text[] aLetter)
        {
            Console.Clear();
            int LargestStringLength = 0;
            foreach (Text m in aLetter)
            {
                LargestStringLength = m.OriginalMessage.Length > LargestStringLength ? m.OriginalMessage.Length : LargestStringLength;
            }

            for (var i = 0; i < aLetter.Length; i++)
            {
                int x = Console.WindowWidth / 2 - LargestStringLength / 2;
                int y = Console.WindowHeight / 2 - aLetter.Length / 2 + i;
                Console.SetCursorPosition(x, y);
                Console.Write(aLetter[i].Message.Replace(aLetter[i].OriginalMessage, ""));
                foreach(char c in aLetter[i].OriginalMessage)
                {
                    if(c.Equals(' '))
                    {
                        Console.Write(c);
                        Thread.Sleep(1000 / 40);
                        continue;
                    }
                    if (c.Equals('.'))
                    {
                        Console.Write(c);
                        Thread.Sleep(1000);
                        continue;
                    }
                    if (c.Equals(','))
                    {
                        Console.Write(c);
                        Thread.Sleep(750);
                        continue;
                    }
                    Console.Write(c);
                    Thread.Sleep(1000 / 20);
                }
                Thread.Sleep(1000 / 4);
            }
            
        }

        public void SendMissionMessage(Text aMission)
        {
            int x = 2;
            int y = Console.WindowHeight - 1;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(aMission.Message);
        }

        internal void StopFadeIn()
        {
            worker.CancelAsync();
        }

        BackgroundWorker worker = new BackgroundWorker();
        private string myControls;
        private int myRed;
        private int myGreen;
        private int myBlue;
        private int myFadeInMiliseconds;
        public void FadeInControls(string aControls, int aRed, int aGreen, int aBlue, int fadeInMiliseconds)
        {
            myControls = aControls;
            myRed = aRed;
            myGreen = aGreen;
            myBlue = aBlue;
            myFadeInMiliseconds = fadeInMiliseconds;
            
            worker.DoWork += worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int i = 0;
            while (!worker.CancellationPending)
            {
                int r = myRed * i / 256;
                int g = myGreen * i / 256;
                int b = myBlue * i / 256;
                SendControlsMessage(new Text(myControls, r, g, b, true));
                Thread.Sleep(myFadeInMiliseconds / 256);
                if (i >= 256)
                    return;
                i++;
            }
        }
    }
}
