using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Player.Story
{
    abstract class BaseContext
    {
        string[] previousStory;

        public abstract void tick();
        public abstract void start();

        public string[] getPreviousStory()
        {
            return previousStory;
        }

        protected void SendMenu(string[] message)
        {
            previousStory = message;
            Program.getPlayer().SendMenu(message);
        }
    }
}
