
using System;
using System.Threading;
using PotterGame.Enemies;

namespace PotterGame.Player.Battling
{
    public class Battle
    {
        private ThreadStart myScheduledAction;
        
        public IBaseEnemy Enemy { get; set; }
        public bool IsBattling { get; set; }
        
        public static Battle CreateInstance()
        {
            return new Battle();
        }
        private void Start(IBaseEnemy aEnemy)
        {
            Enemy = aEnemy;
            Program.GetPlayer().SeizeInput = true;
            EnterStartAnimation();
            IsBattling = true;
            Program.GetPlayer().SeizeInput = false;
            Program.Instance.StartTicking();
        }


        private int myStupefyCooldown = 0;
        private int myProtegoCooldown = 0;
        private int myPetrificusCooldown = 0;
        public void UseStupefy()
        {
            if (myStupefyCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Stupefy;
            myStupefyCooldown = DateTime.Now.Second + 3;
        }

        public void UseProtego()
        {
            if (myProtegoCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Protego;
            myProtegoCooldown = DateTime.Now.Second + 3;
        }

        public void UsePetrificus()
        {
            if (myPetrificusCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Petrificus;
            myPetrificusCooldown = DateTime.Now.Second + 3;
        }

        public void OpenPlayerInventory()
        {
            IsBattling = false;
            Program.GetPlayer().PlayerInventory.OpenInventory();
        }

        private static void EnterStartAnimation()
        {
            
            
            Thread.Sleep(500);
        }

        private void Render()
        {
            
        }

        /// <summary>
        /// This runs async so the battle is realtime,
        /// Otherwise it would stop at <c>Console.ReadKey();</c>
        /// </summary>
        public void Tick()
        {
            // Enemy logic
            // TODO: Implement Enemy actions
            if (Enemy.IsDead)
            {
                Enemy.GiveRewards();
                Enemy = null;
                IsBattling = false;
                Program.Instance.StopTicking();
                return;
            }
            
            
            // Execute scheduled Player Actions
            myScheduledAction?.Invoke();
            myScheduledAction = null;

            // Last thing to be done should be Rendering when everything is done.
            Render();
        }
        
        
        
        //SPELLS FUNCTIONALITY
        private static void Stupefy()
        {
            
        }

        private static void Protego()
        {
            
        }

        private static void Petrificus()
        {
            
        }

    }
}
