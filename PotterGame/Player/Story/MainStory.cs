using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Player.Story
{
    class MainStory : baseContext
    {
        int story = 0;

        public override void start()
        {
            runStory(story);
        }

        private void runStory(int i)
        {
            switch (i)
            {
                case 1:
                    runStoryTwo();
                    break;
                case 0:
                    runStoryOne();
                    break;
            }
        }

        private void runStoryTwo()
        {
            story = 1;

            // TODO: Make story two

            //runStoryThree();
        }

        private void runStoryOne()
        {
            story = 0;

            // TODO: Make story one

            runStoryTwo();
        }

        public override void tick()
        {
            
        }

    }
}
