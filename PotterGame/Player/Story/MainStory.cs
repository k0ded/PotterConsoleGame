using System;
using System.Collections.Generic;
using PotterGame.Utils;
using System.Linq;
using System.Text;
using System.Threading;

namespace PotterGame.Player.Story
{
    internal class MainStory : BaseContext
    {

        private int myStory;
        private Exploration myExploration;
        public string Controls { get; } = "[ENTER] - Continue";

        public MainStory()
        {
            myExploration = new Exploration();
            myExploration.Load();
        }

        public void RunStory(int i)
        {
            switch (i)
            {
                case 1:
                    RunStoryTwo();
                    break;
                case 0:
                    RunStoryOne();
                    break;
            }
        }

        
        // Bug: If you spam space "Get to Diagon Alley!" will stay in the middle of the screen.
        private void RunStoryTwo()
        {
            myStory = 1;
            ResetPrevious();

            var mission = new Text[1];
            mission[0] = new Text("Get to Diagon Alley!");
            TextUtils.SendMessage(mission, TextType.LETTER_SLOW);
            Thread.Sleep(2250);
            
            PreviousMissionMessage = mission[0];
            Explore();
            TextUtils.SendMessage(mission[0], TextType.MISSION);
            

            //runStoryThree();
        }

        private void Explore()
        {
            Console.Clear();
            PreviousExplorationMessage = myExploration.GetExplorationMessage();
            PreviousExplanationMessage = myExploration.GetExplanationMessage();

            TextUtils.SendMessage(PreviousExplorationMessage, TextType.EXPLORATION);
            TextUtils.SendMessage(PreviousExplanationMessage, TextType.EXPLANATION);
            TextUtils.SendMessage(PreviousMissionMessage, TextType.MISSION);
            Player.SendControls(myExploration.GetControlsMessage());
            Program.Player.SeizeInput = false;
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
            TextUtils.SendMessage(letter, TextType.LETTER_SLOW);
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
            
            if (myExploration.RunQAction())
                Explore();
        }
        public override void RunWAction()
        {
            if (Program.Player.SeizeInput)
                return;

            if (myExploration.RunWAction()) 
                Explore();
        }
        public override void RunEAction()
        {
            if (Program.Player.SeizeInput)
                return;

            if (myExploration.RunEAction()) 
                Explore();
        }
        public override void RunAAction()
        {
            if (Program.Player.SeizeInput)
                return;
            
            if (myExploration.RunAAction())
                Explore();
        }
        public override void RunSAction()
        {
            if (Program.Player.SeizeInput)
                return;

            if (myExploration.RunSAction()) 
                Explore();
        }
        public override void RunDAction()
        {
            if (Program.Player.SeizeInput)
                return;

            if (myExploration.RunDAction()) 
                Explore();
        }
    }
}
