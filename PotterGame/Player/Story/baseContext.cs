﻿using PotterGame.Inventories;
using PotterGame.Utils.Text;

namespace PotterGame.Player.Story
{
    public abstract class BaseContext
    {
        public Text[] PreviousCenteredMessage { get; protected set; }
        public Text[] PreviousLetterMessage { get; protected set; }
        public Text[] PreviousExplorationMessage { get; protected set; }
        public Text[] PreviousExplanationMessage { get; protected set; }
        public Text PreviousHeaderBarMessage { get; protected set; }
        public Text PreviousMissionMessage { get; protected set; }
        public Text PreviousControlsMessage { get; protected set; }
        public bool Continue { get; set; }
        
        public virtual void Start() {}
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
        public virtual void RunAAction() {}
        public virtual void RunWAction()
        {

        }
        public virtual void RunSAction()
        {

        }
        public virtual void RunDAction() {}
        public virtual void RunInventoryAction()
        {
            InventoryManager.PlayerInventory.OpenInventory(true);
        }

       
    }
}
