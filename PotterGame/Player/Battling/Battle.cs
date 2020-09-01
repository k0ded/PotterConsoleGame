
using System;
using System.Linq;
using System.Text;
using System.Threading;
using PotterGame.Player.Battling.Enemies;
using PotterGame.Utils;

namespace PotterGame.Player.Battling
{
    public class Battle
    {
        private ThreadStart myScheduledAction;
        private Spells mySpells;
        
        public IBaseEnemy Enemy { get; private set; }
        public bool IsBattling { get; private set; }
        
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

        private Spell[] myPlayerSpell = new Spell[53];
        private Spell[] myEnemySpell = new Spell[53];
        
        // STUPEFY = 1, PROTEGO = 2, PETRIFICUS = 3.
        public void UseStupefy()
        {
            if (myStupefyCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Stupefy;
            myStupefyCooldown = DateTime.Now.Second + 5;
            myPlayerSpell[0] = mySpells.GetAsSpell(6, 1, true);
        }

        public void UseProtego()
        {
            if (myProtegoCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Protego;
            myProtegoCooldown = DateTime.Now.Second + 7;
            myPlayerSpell[1] = mySpells.GetAsSpell(0, 2, true);
        }

        public void UsePetrificus()
        {
            if (myPetrificusCooldown > DateTime.Now.Second)
                return;
            myScheduledAction = Petrificus;
            myPetrificusCooldown = DateTime.Now.Second + 7;
            myPlayerSpell[0] = mySpells.GetAsSpell(8, 3, true);
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
            //
            // (∩｀-´)⊃━ﾟ.*･｡ﾟ☆                                       ☆ﾟ｡･*.ﾟ━⊂(´-'∩)
            //⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺
            
            var rendered = new Text[3];
            rendered[0] = new Text(" ");
            
            const string player = "(∩｀-´)⊃━";
            const string enemy = "━⊂(´-'∩)";

            const string magicStupey = "ﾟ.*･｡ﾟ☆";
            const string magicPetrificus = "*--*--☆";
            
            var sb = new StringBuilder("                                                     ");
            
            // Enemy spell rendering
            for (var i = 0; i < myEnemySpell.Length; i++)
            {
                sb[i] = ' ';
                if (myEnemySpell[i].Type == 0)
                    continue;

                // Stupefy (Attack)
                if (myEnemySpell[i].Type == 1)
                {
                    for (var j = i; j > Math.Max(i - 7, 0); j--)
                    {
                        sb[j] = magicStupey.ToCharArray()[j];
                    }
                }
                
                // Protego (Defend)
                if (myEnemySpell[i].Type == 2)
                {
                    sb[i] = '{';
                }
                
                // Petrificus (Stun)
                if (myEnemySpell[i].Type == 3)
                {
                    for (var j = i; j > Math.Max(i - 7, 0); j--)
                    {
                        sb[j] = magicPetrificus.ToCharArray()[j];
                    }
                }
            }
            
            // The enemy spell is going the opposite direction and will therefor be flipped to match that
            var s = sb.ToString().Reverse();
            sb = new StringBuilder(s.ToString());
            
            // Make sure the spell from the Player gets sent last so its viewed above everything else.
            for (var i = 0; i < myPlayerSpell.Length; i++)
            {
                sb[i] = ' ';
                if (myPlayerSpell[i].Type == 0)
                    continue;

                // Stupefy (Attack)
                if (myPlayerSpell[i].Type == 1)
                {
                    for (var j = i; j > Math.Max(i - 7, 0); j--)
                    {
                        sb[j] = magicStupey.ToCharArray()[j];
                    }
                }
                
                // Protego (Defend)
                if (myPlayerSpell[i].Type == 2)
                {
                    sb[i] = '}';
                }
                
                // Petrificus (Stun)
                if (myPlayerSpell[i].Type == 3)
                {
                    for (var j = i; j > Math.Max(i - 7, 0); j--)
                    {
                        sb[j] = magicPetrificus.ToCharArray()[j];
                    }
                }
            }
            
            rendered[1] = new Text(player + sb + enemy);
            rendered[2] = new Text("⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺");

            // Player Health bar
            var playerHealthAmount = Program.GetPlayer().Health / Program.GetPlayer().MaxHealth * 35;
            var playerHealthString = "";
            var playerNotHealthStr = "";
            
            // Adds the right amount of whitespaces to display the amount of health the enemy has.
            while (playerHealthString.Length < playerHealthAmount)
            {
                playerHealthString += " ";
            }
            
            // Adds the right amount of white spaces to make everything centered
            while (playerNotHealthStr.Length < 35 - playerHealthAmount)
            {
                playerNotHealthStr += " ";
            }
            
            // Player health bar is done
            var playerHealth = new Text(new Text(playerHealthString, ColorCode.B_RED).Message + new Text(playerNotHealthStr, ColorCode.RESET).Message);
            
            
            // Enemy Health bar
            var enemyHealthAmount = Enemy.Health / Enemy.MaxHealth * 35;
            var enemyHealthString = "";
            var enemyNotHealthStr = "";
            
            // Adds the right amount of whitespaces to display the amount of health the enemy has
            while (enemyHealthString.Length < enemyHealthAmount)
            {
                enemyHealthString += " ";
            }
            
            // Adds the rest of the whitespaces to put the health bar in the right position
            while (enemyNotHealthStr.Length < 35 - enemyHealthAmount)
            {
                enemyNotHealthStr += " ";
            }
            var enemyHealth = new Text(new Text(enemyNotHealthStr, ColorCode.RESET).Message + new Text(enemyHealthString, ColorCode.B_RED).Message);
            
            // Adds the health bars to the rendered Text Array.
            rendered[3] = new Text($"{playerHealth.Message}|{enemyHealth.Message}");
            
            // Last thing that's needed is the controls bar that tells which spells are on cooldown and which arent
            var Controls = "[Q] - Stupefy  [W] - Protego  [E] - Petrificus";
            if (DateTime.Now.Second < myStupefyCooldown)
            {
                Controls = Controls.Replace("Q", Convert.ToString(myStupefyCooldown - DateTime.Now.Second));
            }
            
            if (DateTime.Now.Second < myProtegoCooldown)
            {
                Controls = Controls.Replace("W", Convert.ToString(myProtegoCooldown - DateTime.Now.Second));
            }
            
            if (DateTime.Now.Second < myPetrificusCooldown)
            {
                Controls = Controls.Replace("E", Convert.ToString(myPetrificusCooldown - DateTime.Now.Second));
            }
            
            // Renders all of the information to the screen.
            TextUtils.SendMessage(new Text(Controls), TextType.CONTROLS);
            TextUtils.SendMessage(rendered, TextType.CENTERED);
        }

        /// <summary>
        /// This runs async so the battle is realtime,
        /// Otherwise it would stop at <c>Console.ReadKey();</c>
        /// </summary>
        public void Tick()
        {
            // MOVE MAGIC
            for (var i = 0; i < myPlayerSpell.Length; i++)
            {
                var s = myPlayerSpell[i];

                // RAN INTO PROTEGO AND DISAPPEARED
                if (myEnemySpell[1].Type == 2 && i + s.XSpeed / 20 == 1)
                {
                    myPlayerSpell[i] = new Spell(0,0,s.FromPlayer);
                    break;
                }
                
                // RAN INTO THE ENEMY PLAYER
                if (i + s.XSpeed / 20 >= myPlayerSpell.Length)
                {
                    // NOTE: PROTEGO HAS AN XSPEED OF 0
                    // STUPEFY
                    if (s.Type == 1)
                    {
                        Enemy.Damage(10);
                    }
                    
                    // PETRIFICUS
                    if (s.Type == 3)
                    {
                        Enemy.Stun(2);
                    }
                }
                
                // DIDNT RUN INTO ANYTHING
                myPlayerSpell[i + s.XSpeed / 20] = s;
                myPlayerSpell[i] = new Spell(0,0,s.FromPlayer);
            }
            
            for (var i = 0; i < myEnemySpell.Length; i++)
            {
                var s = myEnemySpell[i];
                
                // RAN INTO PROTEGO AND DISAPPEARED
                if (myPlayerSpell[1].Type == 2 && i + s.XSpeed / 20 == 1)
                {
                    myEnemySpell[i] = new Spell(0,0,s.FromPlayer);
                    break;
                }
                // RAN INTO THE PLAYER
                if (i + s.XSpeed / 20 >= myPlayerSpell.Length)
                {
                    // NOTE: PROTEGO HAS AN XSPEED OF 0
                    // STUPEFY
                    if (s.Type == 1)
                    {
                        Program.GetPlayer().Damage(10);
                    }
                    
                    // PETRIFICUS
                    if (s.Type == 3)
                    {
                        Program.GetPlayer().Stun(2);
                    }
                }
                
                // DIDNT RUN INTO ANYTHING
                myEnemySpell[i + s.XSpeed / 20] = s;
                myEnemySpell[i] = new Spell(0,0,s.FromPlayer);
            }

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
