using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PotterGame.Player.Story.Exploring;
using PotterGame.Utils.Text;

namespace PotterGame.Player.Story
{
    internal class MainStory : BaseContext
    {

        private int myStory;
        public Exploration Exploration;

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
            myStory = i;
            ResetPrevious();
            switch (i)
            {
                case 2:
                    HogwartsSortingStory();
                    break;
                case 1:
                    OllivandersMissionStory();
                    break;
                case 0:
                    HogwartsLetterStory();
                    break;
            }
        }

        private void HogwartsSortingStory()
        {
            SendExplorationMission(new Text("Get to Hogwarts and get sorted!", ColorCode.YELLOW));
        }
        
        private void OllivandersMissionStory()
        {
            SendExplorationMission(new Text("Get to Olivanders' and buy your wand!", ColorCode.YELLOW));
        }

        private void HogwartsLetterStory()
        {
            Console.Clear();
            Program.Player.SeizeInput = true;

            var letter = new[]
            {
                new Text("Dear Mr. Potter,"),
                new Text("     We are pleased to inform you that"),
                new Text("you have been accepted at Hogwarts School of"),
                new Text("Witchcraft and Wizardry. Please find enclosed a list"),
                new Text("of all necessary books and equipment."),
                new Text(" "),
                new Text("Term begins on September 1st, We await your owl by"),
                new Text("no later than July 31st."),
                new Text(" "),
                new Text("Yours Sincerely,"),
                new Text("Madam McGonagall"),
                new Text("Minerva McGonagall"),
                new Text("Deputy Headmistress")
            };

            PreviousLetterMessage = letter;
            TextUtils.SendMessage(letter, TextType.LETTER_SLOW, true);
        }

        private void SendExplorationMission(Text message)
        {
            TextUtils.SendMessage(message, TextType.LETTER_SLOW);

            do
            {
                Thread.Sleep(500);
                if (TextUtils.IsWritingMessage()) continue;
                PreviousMissionMessage = message;
                Explore();
                TextUtils.SendMessage(message, TextType.MISSION);
            } while (TextUtils.IsWritingMessage());
        }
        
        public void Explore()
        {
            Console.Clear();
            var travelTime = Exploration.GetTravelTime();
            Travel(travelTime);
            Console.Clear();
            
            PreviousExplorationMessage = Exploration.GetExplorationMessage();
            PreviousExplanationMessage = Exploration.GetExplanationMessage();
            PreviousHeaderBarMessage = new Text(Exploration.GetClueMessage());

            TextUtils.SendMessage(PreviousHeaderBarMessage, TextType.HEADERBAR);
            TextUtils.SendMessage(PreviousExplorationMessage, TextType.EXPLORATION);
            TextUtils.SendMessage(PreviousExplanationMessage, TextType.EXPLANATION);
            TextUtils.SendMessage(PreviousMissionMessage, TextType.MISSION);

            Player.SendControls(Exploration.GetControlsMessage());
            Program.Player.SeizeInput = false;
        }

        private void Travel(int travelTime)
        {
            return; // Debugging
            var timeDone = DateTime.Now.AddSeconds(travelTime);
            var dots = 0;
            
            while (DateTime.Now.CompareTo(timeDone) < 0)
            {
                Console.Clear();
                dots = (dots + 1) % 4;
                var d = new StringBuilder();
                for (var i = 0; i < dots; i++)
                {
                    d.Append(".");
                }
                
                TextUtils.SendMessage(new Text($"Travelling{d}"), TextType.CENTERED);
                Task.Delay(500).Wait();
            }
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
