
using System;
using System.Threading;
using PotterGame.Enemies;

namespace PotterGame.Player.Battling
{
    public class Battle
    {
        private ThreadStart myScheduledAction;
        private Spells mySpells;

        public IBaseEnemy Enemy { get; set; }
        public bool IsBattling { get; set; }
        
        private void Start(IBaseEnemy aEnemy)
        {
            Enemy = aEnemy;
            Program.GetPlayer().SeizeInput = true;
            EnterStartAnimation();
            IsBattling = true;
            Program.GetPlayer().SeizeInput = false;
            mySpells = new Spells();
            Program.Instance.StartTicking();
        }


        private int myStupefyCooldown;
        private int myProtegoCooldown;
        private int myPetrificusCooldown;

        private Spell[,] myPlayerSpell;
        private Spell[,] myEnemySpell;
        public void UseStupefy()
        {
            if (myStupefyCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Stupefy;
            myStupefyCooldown = DateTime.Now.Second + 3;
            myPlayerSpell[0,0] = mySpells.GetAsSpell(6, 0, 1, true);
        }

        public void UseProtego()
        {
            if (myProtegoCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Protego;
            myProtegoCooldown = DateTime.Now.Second + 3;
            myPlayerSpell[1,3] = mySpells.GetAsSpell(0, 12, 2, true);
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
            if(Enemy == null)
                return;
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
