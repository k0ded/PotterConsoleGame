using System;
using System.Collections.Generic;
using PotterGame.Utils;
using System.Linq;
using System.Text;
using System.Threading;
using PotterGame.Utils.Text;

namespace PotterGame.Player.Story
{
    internal class MainStory : BaseContext
    {

        private int myStory;
        public Exploration Exploration;
        public string Controls { get; } = "[ENTER] - Continue";

        public MainStory()
        {
            Exploration = new Exploration();
            Exploration.Load();
        }

        public override void Start()
        {
            RunStory(0);
        }

        public void RunStory(int i)
        {
            switch (i)
            {
                case 2:
                    RunStoryThree();
                    break;
                case 1:
                    RunStoryTwo();
                    break;
                case 0:
                    RunStoryOne();
                    break;
            }
        }

        private void RunStoryThree()
        {
            myStory = 2;
            ResetPrevious();
        }
        
        // Bug: If you spam space or enter "Get to Olivanders' and buy your wand!" will stay in the middle of the screen.
        // Bug: Currently writes this twice???
        private void RunStoryTwo()
        {
            myStory = 1;
            ResetPrevious();

            Text message = new Text("Get to Olivanders' and buy your wand!");
            TextUtils.SendMessage(message, TextType.LETTER_SLOW);
            Thread.Sleep(2250);
            
            PreviousMissionMessage = message;
            Explore();
            TextUtils.SendMessage(message, TextType.MISSION);
        }

        private void RunStoryOne()
        {
            Console.Clear();
            myStory = 0;
            Program.Player.SeizeInput = true;
            ResetPrevious();

            var letter = new Text[13];
            letter[0] = new Text("Dear Mr. Potter,");
            letter[1] = new Text("               We are pleased to inform you that");
            letter[2] = new Text("you have been accepted at Hogwarts School of");
            letter[3] = new Text("Witchcraft and Wizardry. Please find enclosed a list");
            letter[4] = new Text("of all necessary books and equipment.");
            letter[5] = new Text(" ");
            letter[6] = new Text("Term begins on September 1st, We await your owl by");
            letter[7] = new Text("no later than July 31st.");
            letter[8] = new Text(" ");
            letter[9] = new Text("Yours Sincerely,");
            letter[10] = new Text("Madam McGonagall");
            letter[11] = new Text("Minerva McGonagall");
            letter[12] = new Text("Deputy Headmistress");

            PreviousLetterMessage = letter;
            TextUtils.SendMessage(letter, TextType.LETTER_SLOW, true);
        }
        
        public void Explore()
        {
            Console.Clear();
            PreviousExplorationMessage = Exploration.GetExplorationMessage();
            PreviousExplanationMessage = Exploration.GetExplanationMessage();

            TextUtils.SendMessage(PreviousExplorationMessage, TextType.EXPLORATION);
            TextUtils.SendMessage(PreviousExplanationMessage, TextType.EXPLANATION);
            TextUtils.SendMessage(PreviousMissionMessage, TextType.MISSION);
            Player.SendControls(Exploration.GetControlsMessage());
            Program.Player.SeizeInput = false;
        }

        private void ResetPrevious()
        {
            PreviousCenteredMessage = null;
            PreviousControlsMessage = null;
            PreviousExplorationMessage = null;
            PreviousLetterMessage = null;
            PreviousMissionMessage = null;
        }

        public override void RunInteractAction()
        {
            if (TextUtils.IsWritingMessage())
            {
                TextUtils.FinishLetterMessage();
            }else if (TextUtils.IsFadingIn())
            {
                TextUtils.StopFadeIn();
            }

            if (!Continue) return;
            RunStory(myStory + 1);
            Program.Player.SeizeInput = false;
            Continue = false;
        }

        public override void RunQAction()
        {
            if (Program.Player.SeizeInput)
                return;
            
            if (Exploration.RunQAction())
                Explore();
        }
        public override void RunWAction()
        {
            if (Program.Player.SeizeInput)
                return;

            if (Exploration.RunWAction()) 
                Explore();
        }
        public override void RunEAction()
        {
            if (Program.Player.SeizeInput)
                return;

            if (Exploration.RunEAction()) 
                Explore();
        }
        public override void RunAAction()
        {
            if (Program.Player.SeizeInput)
                return;
            
            if (Exploration.RunAAction())
                Explore();
        }
        public override void RunSAction()
        {
            if (Program.Player.SeizeInput)
                return;

            if (Exploration.RunSAction()) 
                Explore();
        }
        public override void RunDAction()
        {
            if (Program.Player.SeizeInput)
                return;

            if (Exploration.RunDAction()) 
                Explore();
        }
    }
}
