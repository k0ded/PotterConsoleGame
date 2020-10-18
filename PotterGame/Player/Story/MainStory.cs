using System;
using System.Text;
using System.Threading;
using PotterGame.Utils.Text;
using PotterGame.Player.Story.Exploring;

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
            SendExplorationMission(new Text("Get to Hogwarts and get sorted!"));
        }
        
        private void RunStoryTwo()
        {
            myStory = 1;
            ResetPrevious();
            SendExplorationMission(new Text("Get to Olivanders' and buy your wand!"));
        }

        private void RunStoryOne()
        {
            Console.Clear();
            myStory = 0;
            Program.Player.SeizeInput = true;
            ResetPrevious();

            var letter = new Text[13];
            letter[0] = new Text("Dear Mr. Potter,");
            letter[1] = new Text("     We are pleased to inform you that");
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

        private void SendExplorationMission(Text message)
        {
            TextUtils.SendMessage(message, TextType.LETTER_SLOW);
            Thread.Sleep(2250);
            
            PreviousMissionMessage = message;
            Explore();
            TextUtils.SendMessage(message, TextType.MISSION);
        }
        
        public void Explore()
        {
            Console.Clear();
            var sb = new StringBuilder();
            var f = (double) Exploration.GetDangerLevel() / 100;
            for (var i = 0; i < (Console.WindowWidth - 10) * f; i++)
            {
                sb.Append("=");
                TextUtils.SendMessage(new Text("ok: " + f + " : " + Console.WindowWidth), TextType.ACTION);
            }
            var headerbar = new Text(sb.ToString(), ColorCode.RED, ColorCode.B_RED);
            
            PreviousExplorationMessage = Exploration.GetExplorationMessage();
            PreviousExplanationMessage = Exploration.GetExplanationMessage();
            PreviousDangerScaleMessage = new []{headerbar, new Text("-//- DANGER BAR -\\\\-", ColorCode.RESET)};

            TextUtils.SendMessage(PreviousDangerScaleMessage, TextType.HEADERBAR);
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
