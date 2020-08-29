using PotterGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PotterGame.Player.Story
{
    class MainStory : BaseContext
    {

        private int myStory = 0;
        private bool myContinue = false;
        private Exploration myExploration = new Exploration();

        public string Controls { get; } = "[ENTER] - Continue";

        public override void Start()
        {
            runStory(myStory);
        }

        private void runStory(int i)
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

            Text[] mission = new Text[1];
            mission[0] = new Text("Get to Diagon Alley!");
            Program.TextUtils.SendLetterMessage(mission);
            Thread.Sleep(1000);
            Program.TextUtils.SendExplorationMessage(myExploration.GetMessage());
            Program.TextUtils.SendMissionMessage(mission[0]);

            //runStoryThree();
        }

        private void RunStoryOne()
        {
            myStory = 0;

            Text[] letter = new Text[13];
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
            Program.TextUtils.SendLetterMessage(letter);
            myContinue = true;
            Program.TextUtils.FadeInControls(Controls, 0, 0, 128, 10000);
        }

        public override void Tick()
        {
            
        }

        public override void RunInteractAction()
        {
            Program.TextUtils.StopFadeIn();
            if (myContinue)
            {
                runStory(myStory + 1);
                myContinue = false;
            }

        }

    }
}
