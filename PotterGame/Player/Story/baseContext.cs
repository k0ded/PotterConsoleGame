using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Player.Story
{
    class baseContext
    {
        string[] previousStory;
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
