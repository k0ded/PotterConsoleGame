using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Player.Story
{
    class MainStory : BaseContext
    {
        int story = 0;

        public override void Start()
        {
            runStory(story);
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
            }
        }

        private void RunStoryTwo()
        {
            story = 1;

            // TODO: Make story two

            //runStoryThree();
        }

        private void RunStoryOne()
        {
            story = 0;

            // TODO: Make story one

            RunStoryTwo();
        }

        public override void Tick()
        {
            
        }

    }
}
