using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace PotterGame.Utils
{
    public static class TextUtils
    {
        private static readonly LetterWriter Writer = new LetterWriter();
        private static readonly ControlsFader Fader = new ControlsFader();

        /// <summary>
        /// Sends a list of <c>Text</c> objects to be displayed on the screen in
        /// the <c>TextType</c> format.
        /// </summary>
        /// <param name="aMessage">A List of <c>Text</c> objects to be displayed on the screen!</param>
        /// <param name="aType">A <c>TextType</c> which decides what type of format to use!</param>
        public static void SendMessage(IReadOnlyList<Text> aMessage, TextType aType)
        {
            switch (aType)
            {
                case TextType.CENTERED:
                    SendCenteredMessage(aMessage);
                    break;
                case TextType.EXPLORATION:
                    SendExplorationMessage(aMessage);
                    break;
                case TextType.INVENTORY:
                    SendInventoryMessage(aMessage);
                    break;
                case TextType.CONTROLS:
                    SendControlsMessage(aMessage);
                    break;
                case TextType.LETTER_SLOW:
                    SendLetterMessage(aMessage, true);
                    break;
                case TextType.LETTER_INSTANT:
                    SendLetterMessage(aMessage, false);
                    break;
                case TextType.MISSION:
                    SendMissionMessage(aMessage);
                    break;
                default:
                    Console.WriteLine("CHECK CODE, TEXT TYPE IS NULL OR NOT ADDED IN TEXT UTILS");
                    break;
            }
        }

        /// <summary>
        /// Sends a one-line message in the <c>TextType</c> format!
        /// </summary>
        /// <param name="aMessage">A <c>Text</c> object to be displayed on the screen!</param>
        /// <param name="aType">A <c>TextType</c> which decides what type of format to use!</param>
        public static void SendMessage(Text aMessage, TextType aType)
        {
            SendMessage(new[] { aMessage }, aType);
        }

        /// <summary>
        /// Sends a message on screen in the "Centered" format
        /// Which is a message in the middle of the screen.
        /// </summary>
        /// 
        /// <param name="aMessage">A List of <c>Text</c> objects to display in the Console</param>
        private static void SendCenteredMessage(IReadOnlyList<Text>  aMessage)
        {
            for (var i = 0; i < aMessage.Count; i++)
            {
                var x = Console.WindowWidth / 2 - aMessage[i].OriginalMessage.Length / 2;
                var y = Console.WindowHeight / 2 - aMessage.Count / 2 + i;
                Console.SetCursorPosition(x, y);
                Console.WriteLine(aMessage[i].Message);
            }

        }
        
        /// <summary>
        /// Sends a message on screen in the "Exploration" format
        /// Which is a message in the middle of the screen with
        /// All of the text starting at the same x position
        /// </summary>
        /// 
        /// <param name="aMessage">A List of <c>Text</c> objects to display in the Console</param>
        private static void SendExplorationMessage(IReadOnlyList<Text> aMessage)
        {

            var largestStringLength = aMessage.Select(m => m.OriginalMessage.Length).Prepend(0).Max();

            for (var i = 0; i < aMessage.Count; i++)
            {

                var x = Console.WindowWidth / 2 - largestStringLength / 2;
                var y = Console.WindowHeight / 2 - aMessage.Count / 2 + i;

                Console.SetCursorPosition(x, y);
                Console.WriteLine(aMessage[i].Message + "                                 ");
            }

        }

        /// <summary>
        /// Sends a message on screen in the "Inventory" format
        /// Which is a message on the left hand side of the screen
        /// Going top to bottom.
        /// </summary>
        /// 
        /// <param name="aMessage">A List of <c>Text</c> objects to display in the Console</param>
        private static void SendInventoryMessage(IReadOnlyList<Text> aMessage)
        {
            
            for (var i = 0; i < aMessage.Count; i++)
            {
                
                const int x = 2;
                var y = Math.Min(i, Console.WindowHeight - 2);

                Console.SetCursorPosition(x, y);
                Console.WriteLine(aMessage[i].Message);
            
            }
        
        }

        /// <summary>
        /// Sends a message on screen in the "Controls" format
        /// Which is a message in the middle of the screen
        /// right at the bottom
        /// </summary>
        /// 
        /// <param name="aControls">A List of <c>Text</c> objects to display in the Console</param>
        private static void SendControlsMessage(IReadOnlyList<Text> aControls)
        {
            
            if (aControls.Count > 1)
            {
                Console.WriteLine(" MORE THAN ONE LINE OF CONTROLS!!!!! ");
                return;
            }

            var x = Console.WindowWidth / 2 - aControls[0].OriginalMessage.Length / 2;
            const int y = 62;

            Console.SetCursorPosition(x, y);
            Console.Write(aControls[0].Message);
        
        }

        /// <summary>
        /// Sends a message on screen in the "Letter" format
        /// Which is a message from left to right centered on the
        /// longest <c>string</c>
        /// </summary>
        /// 
        /// <param name="aLetter">A List of <c>Text</c> objects to display in the Console</param>
        /// <param name="aSlow">TRUE: Character by character. FALSE: Instant.</param>
        private static void SendLetterMessage(IReadOnlyList<Text> aLetter, bool aSlow)
        {
            
            var largestStringLength = aLetter.Select(m => m.OriginalMessage.Length).Prepend(0).Max();

            if (aSlow)
            {
                Writer.StartWritingLetter(aLetter);
                return;
            }


            for (var i = 0; i < aLetter.Count; i++)
            {

                var x = Console.WindowWidth / 2 - largestStringLength / 2;
                var y = Console.WindowHeight / 2 - aLetter.Count / 2 + i;

                Console.SetCursorPosition(x, y);
                Console.WriteLine(aLetter[i].Message);

            }
        }

        /// <summary>
        /// Skips the writing of the letter and shows the full message!
        /// </summary>
        public static void FinishLetterMessage()
        {
            Writer.FinishLetterMessage();
        }

        /// <summary>
        /// Fades a Text from black into desired RGB values!
        /// </summary>
        /// 
        /// <param name="aText">The Text that should be Faded in</param>
        /// <param name="aRed">Red value in RGB values.</param>
        /// <param name="aGreen">Green value in RGB values.</param>
        /// <param name="aBlue">Blue value in RGB values</param>
        /// <param name="aFadeInMilliseconds">Time in Milliseconds for the fade complete.</param>
        public static void FadeInControlMessage(Text aText, int aRed, int aGreen, int aBlue, int aFadeInMilliseconds)
        {
            Fader.FadeInControls(aText,aRed, aGreen, aBlue, aFadeInMilliseconds);
        }

        /// <summary>
        /// Stops the Fading of the <c>ControlsFader</c>.
        /// If its not Fading anything its just gonna ignore the call.
        /// </summary>
        public static void StopFadeIn()
        {

            Fader.StopFadeIn();

        }

        /// <summary>
        /// Sends a message in the bottom left part of the screen,
        /// This place is reserved for different Missions
        /// </summary>
        /// 
        /// <param name="aMission">NOTE: This should never be longer than one Text object!</param>
        private static void SendMissionMessage(IReadOnlyList<Text> aMission)
        {

            if (aMission.Count > 1)
                throw new ArgumentException("Too many lines of text!");
            const int x = 2;
            var y = Console.WindowHeight - 1;

            Console.SetCursorPosition(x, y);
            Console.Write(aMission[0].Message);
            
        }

        /// <summary>
        /// This gets the state of <c>LetterWriter</c> and
        /// checks if the fading is active
        /// </summary>
        /// 
        /// <returns>Boolean, True if its currently writing a letter. Otherwise it will always be False</returns>
        public static bool IsWritingMessage()
        {
            return Writer.IsWritingMessage;
        }

        /// <summary>
        /// This gets the state of <c>ControlsFader</c> and
        /// checks if the fading is active
        /// </summary>
        /// 
        /// <returns>Boolean, True if controls are fading in. Otherwise it will always be False</returns>
        public static bool IsFadingIn()
        {
            return Fader.IsFadingIn;
        }

    }


    /// <summary>
    /// Class <c>ControlsFader</c> is in this
    /// File because it wont be accessed anywhere else, Classes were made
    /// to manage bulky code!
    /// </summary>

    internal class ControlsFader {

            internal void StopFadeIn()
        {
            myFadeInWorker.CancelAsync();
        }

        private readonly BackgroundWorker myFadeInWorker = new BackgroundWorker();
        private Text myControls;
        private int myRed;
        private int myGreen;
        private int myBlue;
        private int myFadeInMilliseconds;
        internal bool IsFadingIn;
        public void FadeInControls(Text aText, int aRed, int aGreen, int aBlue, int fadeInMilliseconds)
        {
            // Makes sure all of the variables are in the right format as to not crash the program
            var argumentException = new ArgumentException("Number must be non-negative and less than 256");
            if(aRed > 255 || aRed < 0)
                throw argumentException;
            if (aGreen > 255 || aGreen < 0)
                throw argumentException;
            if(aBlue > 255 || aBlue < 0)
                throw argumentException;
            
            myControls = aText;
            myRed = aRed;
            myGreen = aGreen;
            myBlue = aBlue;
            myFadeInMilliseconds = fadeInMilliseconds;

            myFadeInWorker.DoWork += FadeInControls;
            myFadeInWorker.WorkerSupportsCancellation = true;
            myFadeInWorker.RunWorkerAsync();
                IsFadingIn = true;
        }
        private void FadeInControls(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var i = 0;
            while (worker != null && !worker.CancellationPending)
            {
                var r = myRed * i / 256;
                var g = myGreen * i / 256;
                var b = myBlue * i / 256;
                
                TextUtils.SendMessage(new[] { new Text(myControls.OriginalMessage, r, g, b, true) }, TextType.CONTROLS);
                Thread.Sleep(myFadeInMilliseconds / 256);
                if (i >= 256)
                    return;
                i++;
            }
            IsFadingIn = false;
        }

    }

    /// <summary>
    /// Class <c>LetterWriter</c> is in this
    /// File because it wont be accessed anywhere else, Classes were made
    /// to manage bulky code!
    /// </summary>
    internal class LetterWriter
    {
        internal void FinishLetterMessage()
        {
            myLetterMessageWorker.CancelAsync();
            TextUtils.SendMessage(myFinishedLetter, TextType.LETTER_INSTANT);
        }

        // Background worker is used to make sure you can skip the writing part in case
        // You've already seen it or dont want to read through it all.
        private readonly BackgroundWorker myLetterMessageWorker = new BackgroundWorker();
        private IReadOnlyList<Text> myFinishedLetter;
        internal bool IsWritingMessage;

        public void StartWritingLetter(IReadOnlyList<Text> aFinishedLetter)
        {
            
            IsWritingMessage = true;
            myFinishedLetter = aFinishedLetter;

            myLetterMessageWorker.DoWork += WriteLetter;
            myLetterMessageWorker.WorkerSupportsCancellation = true;
            myLetterMessageWorker.RunWorkerAsync();
            
        }
        private void WriteLetter(object sender, DoWorkEventArgs e)
        {
            
            Console.Clear();
            var worker = sender as BackgroundWorker;
            var i = 0;

            var largestStringLength = myFinishedLetter.Select(m => m.OriginalMessage.Length).Prepend(0).Max();

            while (worker != null && !worker.CancellationPending)
            {

                var x = Console.WindowWidth / 2 - largestStringLength / 2;
                var y = Console.WindowHeight / 2 - myFinishedLetter.Count / 2 + i;
                
                Console.SetCursorPosition(x, y);
                Console.Write(myFinishedLetter[i].Message.Replace(myFinishedLetter[i].OriginalMessage, ""));
                
                foreach (var c in myFinishedLetter[i].OriginalMessage)
                {
                    switch (c)
                    {
                        case ' ':
                            Console.Write(c);
                            Thread.Sleep(1000 / 40);
                            continue;
                        case '.':
                            Console.Write(c);
                            Thread.Sleep(1000);
                            continue;
                        case ',':
                            Console.Write(c);
                            Thread.Sleep(750);
                            continue;
                        default:
                            Console.Write(c);
                            Thread.Sleep(1000 / 20);
                            break;
                    }
                }
                
                Thread.Sleep(1000 / 4);
                
                if (i >= myFinishedLetter.Count)
                    return;
                i++;
            }
            
            IsWritingMessage = false;
            TextUtils.FadeInControlMessage(new Text("[ENTER] - Continue"), 0, 0, 200, 7500);
            Program.GetPlayer().Context.Continue = true;
        }
    }
}
