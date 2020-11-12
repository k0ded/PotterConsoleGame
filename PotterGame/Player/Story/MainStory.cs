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
        private Random myRandom = new Random();
        public Exploration Exploration;

        public MainStory()
        {
            Exploration = new Exploration();
            Exploration.Load();
        }

        /// <summary>
        /// Startar berättelsen.
        /// </summary>
        public override void Start()
        {
            RunStory(0);
        }

        /// <summary>
        /// Kör en specifik berättelse.
        /// </summary>
        /// <param name="i"></param>
        public void RunStory(int i)
        {
            myStory = i;
            ResetPrevious();
            switch (i)
            {
                case 2:
                    GringottsStory();
                    break;
                case 1:
                    OllivandersMissionStory();
                    break;
                case 0:
                    HogwartsLetterStory();
                    break;
            }
        }

        #region Stories

        private void GringottsStory()
        {
            // Den här skickar en text i mitten av skärmen där den sakta skriver ut allting,
            // efter en liten stund när den är färdig går den tillbaka till Explore();
            SendExplorationMission(new Text("You now have your wand! Find the clues necessary to open the dungeons at gringotts!", ColorCode.YELLOW));
        }
        
        private void OllivandersMissionStory()
        {
            // Den här skickar en text i mitten av skärmen där den sakta skriver ut allting,
            // efter en liten stund när den är färdig går den tillbaka till Explore();
            SendExplorationMission(new Text("Get to Olivanders' and buy your wand!", ColorCode.YELLOW));
        }

        private void HogwartsLetterStory()
        {
            Console.Clear();
            Program.Player.SeizeInput = true; // Ser till så att input stoppas

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

        #endregion

        /// <summary>
        /// Av någon anledning så vägrade LETTER_SLOW fungera på rätt sätt så jag valde att göra
        /// Denna metoden istället för att fixa problemet då det var lite lättare.
        /// </summary>
        private void SendExplorationMission(Text message)
        {
            TextUtils.SendMessage(message, TextType.LETTER_SLOW);

            // Hade problem med att en letter valde att köras två gånger så för att fixa detta gjorde jag en do-while
            // Vilket kör först sen kollar conditionen vilket betyder att jag bara kommer få loopen att köra en gång
            // så länge conditionen är false
            do
            {
                Thread.Sleep(500);
                if (TextUtils.IsWritingMessage()) continue;
                PreviousMissionMessage = message;
                Explore();
                TextUtils.SendMessage(message, TextType.MISSION);
            } while (TextUtils.IsWritingMessage());
        }
        
        /// <summary>
        /// Skickar informationen till skärmen när man utforskar.
        /// </summary>
        public void Explore()
        {
            Program.Player.ChangeStamina(-5);
            Console.Clear();
            var travelTime = Exploration.GetTravelTime();
            
            // Den här tar "travelTime" sekunder på sig att bli färdig och skriver Travelling...
            // med olika mängd punkter tills den är färdig
            TravelDelay(travelTime);
            Console.Clear();

            int moneyFound = myRandom.Next(1, 10);
            bool shouldGiveMoney = myRandom.Next(100) > 80;
            
            // Här ser jag till så att ifall man går in i Inventoryt så kan man ta sig tillbaka ut.
            PreviousExplorationMessage = Exploration.GetExplorationMessage();
            PreviousExplanationMessage = Exploration.GetExplanationMessage();
            PreviousHeaderBarMessage = new Text(Exploration.GetClueMessage());

            TextUtils.SendMessage(PreviousHeaderBarMessage, TextType.HEADERBAR);
            TextUtils.SendMessage(PreviousExplorationMessage, TextType.EXPLORATION);
            TextUtils.SendMessage(PreviousExplanationMessage, TextType.EXPLANATION);
            TextUtils.SendMessage(PreviousMissionMessage, TextType.MISSION);
            
            // Den här skickar rätt text ifall man hittat guld eller ej och hur mycket guld man i så fall hittat.
            Text hud = shouldGiveMoney
                ? new Text("You found " + moneyFound + " gold!    " + Program.Player.Stamina + "/" +
                           Program.Player.MaxStamina)
                : new Text(Program.Player.Stamina + "/" + Program.Player.MaxStamina);
            
            TextUtils.SendMessage(hud, TextType.HUD);

            if(shouldGiveMoney)
                Program.Player.AddMoney(moneyFound);
            Player.SendControls(Exploration.GetControlsMessage());
            Program.Player.SeizeInput = false;
        }

        /// <summary>
        /// Används som ett sätt att vänta lite innan man får fortsätta utforska.
        /// Detta gjordes för att man inte ska kunna spamma olika knappar för att komma fram.
        /// </summary>
        private void TravelDelay(int travelTime)
        {
            var timeDone = DateTime.Now.AddSeconds(travelTime);
            var dots = 0;
            
            // CompareTo ger:
            // <0 om DateTime.Now är tidigare än timeDone
            // 0 om DateTime.Now är exakt samma som timeDone
            // >0 om DateTime.Now är mer än timeDone
            while (DateTime.Now.CompareTo(timeDone) < 0)
            {
                Console.Clear();
                //Den här ser till så att det går från en punkt till två till tre till noll.
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

        /// <summary>
        /// Ser till så att man inte får med onödig information från en annan plats.
        /// </summary>
        private void ResetPrevious()
        {
            PreviousCenteredMessage = null;
            PreviousControlsMessage = null;
            PreviousExplorationMessage = null;
            PreviousLetterMessage = null;
            PreviousMissionMessage = null;
        }

        #region Controls

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

        #endregion
    }
}
