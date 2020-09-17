
using System;
using System.Linq;
using System.Text;
using System.Threading;
using PotterGame.Player.Battling.Enemies;
using PotterGame.Player.Story;
using PotterGame.Utils;

namespace PotterGame.Player.Battling
{
    public partial class Battle : BaseContext
    {
        private ThreadStart myScheduledAction;
        private Spells mySpells;
        
        public BaseEnemy Enemy { get; private set; }
        public bool IsBattling { get; private set; }
        
        public override void Start(BaseEnemy aEnemy)
        {
            Enemy = aEnemy;
            Program.Player.SeizeInput = true;
            EnterStartAnimation();
            IsBattling = true;
            Program.Player.SeizeInput = false;
            mySpells = new Spells();
            Program.Instance.StartTicking();
        }


        private int myStupefyCooldown;
        private int myProtegoCooldown;
        private int myPetrificusCooldown;

        private int myVirtualPlayerHealth;
        private int myVirtualEnemyHealth;

        private Spell[] myPlayerSpell = new Spell[53];
        private Spell[] myEnemySpell = new Spell[53];
        
        // STUPEFY = 1, PROTEGO = 2, PETRIFICUS = 3.

        #region Input
        
        /// <summary>
        /// Uses the Stupefy spell (Type 1)
        /// </summary>
        public void UseStupefy()
        {
            if (myStupefyCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Stupefy;
            myStupefyCooldown = DateTime.Now.Second + 5;
            myPlayerSpell[0] = mySpells.GetAsSpell(6, 1, true);
        }

        /// <summary>
        /// Uses the Protego spell (Type 2)
        /// </summary>
        public void UseProtego()
        {
            if (myProtegoCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Protego;
            myProtegoCooldown = DateTime.Now.Second + 7;
            myPlayerSpell[1] = mySpells.GetAsSpell(0, 2, true);
        }

        /// <summary>
        /// Uses the Petrificus spell (Type 3)
        /// </summary>
        public void UsePetrificus()
        {
            if (myPetrificusCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Petrificus;
            myPetrificusCooldown = DateTime.Now.Second + 7;
            myPlayerSpell[0] = mySpells.GetAsSpell(8, 3, true);
        }
        #endregion

        public void OpenPlayerInventory()
        {
            IsBattling = false;
            Program.Player.PlayerInventory.OpenInventory(true);
        }

        private void EnterStartAnimation()
        {
            var startPlayerHealth = Program.Player.Health;
            var largestMaxHealth = Math.Max(Program.Player.MaxHealth, Enemy.MaxHealth);

            for (var i = 0; i < largestMaxHealth + 1; i++)
            {
                Render();
                myVirtualPlayerHealth = Math.Min(i / 35, Program.Player.Health / 35);
                myVirtualEnemyHealth = Math.Min(i / 35, Enemy.Health / 35);
                Thread.Sleep(500);
            }
        }


        #region Spells
        
        private static void Stupefy()
        {
            
        }

        private static void Protego()
        {
            
        }

        private static void Petrificus()
        {
            
        }
        
        #endregion
    }
}
