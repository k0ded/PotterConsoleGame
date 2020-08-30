using PotterGame.Utils;
using System.Linq;
using System.Threading;

namespace PotterGame.Player.Story
{
    internal class MainStory : BaseContext
    {

        private int myStory;
        private readonly Exploration myExploration = new Exploration();
        public string Controls { get; } = "[ENTER] - Continue";

        public override void Start()
        {
            RunStory(myStory);
        }

        private void RunStory(int i)
        {
            switch (i)
            {
                case 1:
                    RunStoryTwo();
                    break;
                case 0:
                    RunStoryOne();
                    break;
                default:
                    break;
            }
        }

        private void RunStoryTwo()
        {
            myStory = 1;
            ResetPrevious();

            var mission = new Text[1];
            mission[0] = new Text("Get to Diagon Alley!");
            TextUtils.SendMessage(mission, TextType.LETTER_SLOW);
            Thread.Sleep(2250);

            Explore();

            PreviousMissionMessage = mission[0];
            TextUtils.SendMessage(mission[0], TextType.MISSION);
            

            //runStoryThree();
        }

        private void Explore()
        {
            if (PreviousExplorationMessage != null)
            {
                // Makes sure there are no null variables.
                var prevMessage = PreviousExplorationMessage.Where(m => m != null).ToArray();
                
                // Removes previous text by setting it all to whitespaces.
                for (var i = 0; i < prevMessage.Length; i++)
                {
                    var msg = prevMessage[i].OriginalMessage.ToCharArray().Aggregate("", (current, character) => string.Concat(current, ' '));
                    prevMessage[i] = new Text(msg);
                }
                TextUtils.SendMessage(prevMessage, TextType.EXPLORATION);
            }
            
            if (PreviousExplanationMessage != null)
            {
                // Makes sure there are no null variables.
                var prevMessage = PreviousExplanationMessage.Where(m => m != null).ToArray();
                
                // Removes previous text by setting it all to whitespaces.
                for (var i = 0; i < prevMessage.Length; i++)
                {
                    var msg = prevMessage[i].OriginalMessage.ToCharArray().Aggregate("", (current, character) => string.Concat(current, ' '));
                    prevMessage[i] = new Text(msg);
                }
                TextUtils.SendMessage(prevMessage, TextType.EXPLANATION);
            }
            
            PreviousExplorationMessage = myExploration.GetExplorationMessage();
            PreviousExplanationMessage = myExploration.GetExplanationMessage();
            
            TextUtils.SendMessage(PreviousExplorationMessage, TextType.EXPLORATION);
            TextUtils.SendMessage(PreviousExplanationMessage, TextType.EXPLANATION);
            Program.GetPlayer().SeizeInput = false;
        }

        private void RunStoryOne()
        {
            myStory = 0;
            Program.GetPlayer().SeizeInput = true;
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
            Program.GetPlayer().SeizeInput = false;
            Continue = false;

        }

        public override void RunQAction()
        {
            if(Program.GetPlayer().SeizeInput) return;
            if (myExploration.RunQAction())
                Explore();
        }
        public override void RunWAction()
        {
            if(Program.GetPlayer().SeizeInput) return;
            if (myExploration.RunWAction()) 
                Explore();
        }
        public override void RunEAction()
        {
            if(Program.GetPlayer().SeizeInput) return;
            if (myExploration.RunEAction()) 
                Explore();
        }
        
        public override void RunInventoryAction()
        {
            if (Program.GetPlayer().SeizeInput) return;
            base.RunInventoryAction();
        }

    }
}
