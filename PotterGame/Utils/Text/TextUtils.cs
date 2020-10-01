﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace PotterGame.Utils.Text
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
        /// <exception cref="ArgumentException">Exception gets thrown when the list is either empty or only contains null values</exception>
        public static void SendMessage(IReadOnlyList<Text> aMessage, TextType aType)
        {
            // Makes sure the message is Non Null!
            IReadOnlyList<Text> message = aMessage.Where(m => m != null).ToArray();
            
            // If the message is null throw exception
            if (message.Count == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(aMessage));
            
            switch (aType)
            {
                case TextType.DEBUG:
                    SendDebugMessage(message[0]);
                    break;
                case TextType.CENTERED:
                    SendCenteredMessage(message);
                    break;
                case TextType.EXPLORATION:
                    SendExplorationMessage(message);
                    break;
                case TextType.INVENTORY:
                    SendInventoryMessage(message);
                    break;
                case TextType.CONTROLS:
                    SendControlsMessage(message);
                    break;
                case TextType.MISSION:
                    SendMissionMessage(message);
                    break;
                case TextType.EXPLANATION:
                    SendExplanationMessage(message[0], message[1]);
                    break;
                case TextType.LETTER_SLOW:
                    SendLetterMessage(message, true);
                    break;
                case TextType.LETTER_INSTANT:
                    SendLetterMessage(message, false);
                    break;
                default:
                    Console.WriteLine("CHECK CODE, TEXT TYPE IS NULL OR NOT ADDED IN TEXT UTILS");
                    break;
            }
        }

        /// <summary>
        /// Sends a one-line message in the <c>TextType</c> format!
        /// </summary>
        /// <param name="aMessage">A NonNull <c>Text</c> object to display in the Console</param>
        /// <param name="aType">A <c>TextType</c> which decides what type of format to use!</param>
        public static void SendMessage(Text aMessage, TextType aType)
        {
            SendMessage(new[] { aMessage }, aType);
        }
        
        
        
        // These send messages in different ways.
        
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
        
        private static void SendExplorationMessage(IReadOnlyList<Text> aMessage)
        {
            var largestStringLength = aMessage.Select(m => m.OriginalMessage.Length).Prepend(0).Max();

            for (var i = 0; i < aMessage.Count; i++)
            {
                var xFactor = i == 0 ? aMessage[i].OriginalMessage.Length / 2 : largestStringLength / 2;
                var x = Console.WindowWidth / 2 - xFactor;
                var y = Console.WindowHeight / 2 - aMessage.Count / 2 + i;

                Console.SetCursorPosition(x, y);
                Console.WriteLine(aMessage[i].Message);
            }

        }
        
        private static void SendExplanationMessage(Text aTitle, Text aMessage)
        {
            const int maxCharactersPerLine = 40;
            var amountOfBreakLines = 0;

            var indexOfLastBreakLine = 0;
            var previousCharacters = 0;

            var finishedTextBuilder = new StringBuilder(aMessage.OriginalMessage);
            for (var i = 0; i < aMessage.OriginalMessage.Length; i++)
            {
                if (aMessage.OriginalMessage[i] == ' ')
                {
                    indexOfLastBreakLine = i;
                }
                
                if (i - (previousCharacters) <= maxCharactersPerLine) continue;
                finishedTextBuilder.Replace(" ", "§", indexOfLastBreakLine, 1);
                previousCharacters = i;
                amountOfBreakLines++;
            }
            
            var titleX = Console.WindowWidth - (maxCharactersPerLine / 2 + aTitle.OriginalMessage.Length / 2) - 10;
            var x = Console.WindowWidth - maxCharactersPerLine - 10;
            var y = Console.WindowHeight / 2 - amountOfBreakLines / 2;

            // Needed to put the message in the right position
            var textList = finishedTextBuilder.ToString().Split('§');

            if(titleX < x)
                throw new ArgumentException("Title too long");
            
            // Paste the title of the explanation message
            Console.SetCursorPosition(titleX, y - 1);
            Console.WriteLine(aTitle.Message);
            
            // Paste the explanation message.
            for (var i = 0; i < textList.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(textList[i]);
            }
        }
        
        private static void SendInventoryMessage(IReadOnlyList<Text> aMessage)
        {
            _debugLine = 2;
            for (var i = 0; i < aMessage.Count; i++)
            {
                
                const int x = 2;
                var y = Math.Min(i, Console.WindowHeight - 2);

                Console.SetCursorPosition(x, y);
                if(aMessage[i] != null)
                    Console.WriteLine(aMessage[i].Message);
            
            }
        
        }

        private static int _debugLine = 2;
        private static void SendDebugMessage(Text aMessage)
        {
            const int x = 2;
            var y = Math.Min(_debugLine, Console.WindowHeight - 2);

            Console.SetCursorPosition(x,y);
            Console.WriteLine(aMessage.OriginalMessage);
            
            _debugLine++;
        }
        
        private static void SendControlsMessage(IReadOnlyList<Text> aControls)
        {
            
            if (aControls.Count > 1)
            {
                Console.WriteLine(" MORE THAN ONE LINE OF CONTROLS!!!!! ");
                return;
            }

            var x = Console.WindowWidth / 2 - aControls[0].OriginalMessage.Length / 2;
            var y = Console.WindowHeight - 4;

            Console.SetCursorPosition(x, y);
            Console.Write(aControls[0].Message);
        
        }


        /// <summary>
        /// Sends a message on screen in the "Letter" format
        /// Which is a message from left to right centered on the
        /// longest <c>string</c>
        /// </summary>
        /// 
        /// <param name="aLetter">A List of NonNull <c>Text</c> objects to display in the Console</param>
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
        /// Sends a message in the bottom left part of the screen,
        /// This place is reserved for different Missions
        /// </summary>
        /// 
        /// <param name="aMission">NOTE: This should never be longer than one NonNull Text object!</param>
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
        /// Fades a Text from black into desired RGB values!
        /// </summary>
        /// 
        /// <param name="aText">A NonNull Text Object that should be Faded in</param>
        /// <param name="aRed">Red value in RGB values.</param>
        /// <param name="aGreen">Green value in RGB values.</param>
        /// <param name="aBlue">Blue value in RGB values</param>
        /// <param name="aFadeInMilliseconds">Time in Milliseconds for the fade complete.</param>
        public static void FadeInControlMessage(Text aText, int aRed, int aGreen, int aBlue, int aFadeInMilliseconds)
        {
            Fader.FadeInControls(aText, aRed, aGreen, aBlue, aFadeInMilliseconds);
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

    #region Async Messages

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

        /// <summary>
        /// Starts 
        /// </summary>
        /// <param name="aFinishedLetter"></param>
        public void StartWritingLetter(IReadOnlyList<Text> aFinishedLetter)
        {
            IsWritingMessage = true;
            myFinishedLetter = aFinishedLetter;

            myLetterMessageWorker.DoWork += WriteLetter;
            myLetterMessageWorker.WorkerSupportsCancellation = true;
            myLetterMessageWorker.RunWorkerAsync();
            
        }
        
        /// <summary>
        /// Handles the writing of the SLOW_LETTER type
        /// </summary>
        private void WriteLetter(object sender, DoWorkEventArgs e)
        {
            
            Console.Clear();
            var worker = sender as BackgroundWorker;

            var largestStringLength = myFinishedLetter.Select(m => m.OriginalMessage.Length).Prepend(0).Max();

            //while (worker != null && !worker.CancellationPending)
            for(var i = 0; i < myFinishedLetter.Count && worker != null && !worker.CancellationPending; i++)
            {

                var x = Console.WindowWidth / 2 - largestStringLength / 2;
                var y = Console.WindowHeight / 2 - myFinishedLetter.Count / 2 + i;
                
                Console.SetCursorPosition(x, y);
                Console.Write(myFinishedLetter[i].Message.Replace(myFinishedLetter[i].OriginalMessage, ""));
                
                // Gets each individual character and prints it to the screen but wont fire if a cancellation is pending.
                foreach (var c in myFinishedLetter[i].OriginalMessage.TakeWhile(c => !worker.CancellationPending))
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
            }
            
            IsWritingMessage = false;
            TextUtils.FadeInControlMessage(new Text("[ENTER] - Continue"), 0, 0, 200, 7500);
            Program.Player.Context.Continue = true;
        }
    }
    
    #endregion
    
}