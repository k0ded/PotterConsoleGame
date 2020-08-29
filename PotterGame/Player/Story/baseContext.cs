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
        public virtual void RunInteractAction()
        {

        }
        public virtual void RunBackspaceAction()
        {

        }
        public virtual void RunWAction()
        {

        }
        public virtual void RunSAction()
        {

        }

        public Text[] getPreviousStory()
        {
            return myPreviousStory;
        }

        public virtual void RunQAction()
        {
            
        }
    }
}
