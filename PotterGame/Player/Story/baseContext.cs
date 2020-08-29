using PotterGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Player.Story
{
    public abstract class BaseContext
    {
        public Text[] PreviousCenteredMessage { get; set; }
        public Text[] PreviousLetterMessage { get; set; }
        public Text[] PreviousExplorationMessage { get; set; }
        public Text PreviousMissionMessage { get; set; }
        public Text PreviousControlsMessage { get; set; }
        public Boolean Continue { get; set; } = false;

        public abstract void Tick();
        public abstract void Start();
        public virtual void RunInteractAction()
        {

        }
        public virtual void RunBackspaceAction()
        {

        }
        public virtual void RunQAction()
        {

        }
        public virtual void RunEAction()
        {

        }
        public virtual void RunWAction()
        {

        }
        public virtual void RunSAction()
        {

        }
        public virtual void RunInventoryAction()
        {
            Program.Instance.StopTicking();
            Program.GetPlayer().PlayerInventory.OpenInventory(0, 0);
        }

       
    }
}
