using PotterGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Player.Story
{
    abstract class BaseContext
    {
        private Text[] myPreviousStory;

        public abstract void Tick();
        public abstract void Start();

        public Text[] getPreviousStory()
        {
            return myPreviousStory;
        }
    }
}
