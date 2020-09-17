namespace PotterGame.Player.Battling
{
    public partial class Battle
    {
        
        /// <summary>
        /// This runs async so the battle is realtime,
        /// Otherwise it would stop at <c>Console.ReadKey();</c>
        /// </summary>
        public void Tick()
        {
            // Processes the moving of magic.
            MoveMagic();
            
            // If enemy is dead dont continue processing the tick
            if (EnemyLogic())
                return;

            // Execute scheduled Player Actions
            myScheduledAction?.Invoke();
            myScheduledAction = null;

            myVirtualPlayerHealth = Program.Player.Health;
            myVirtualEnemyHealth = Enemy.Health;
            // Last thing to be done should be Rendering when everything is done.
            Render();
        }

        
        /// <summary>
        /// Runs the EnemyLogic such as dying casting spells etc. If the enemy has died that tick
        /// or it doesnt exist the <c>bool</c> will return true, otherwise it stays false.
        /// </summary>
        private bool EnemyLogic()
        {
            if(Enemy == null)
                return true;
            // Enemy logic
            // TODO: Implement Enemy actions
            if (!Enemy.IsDead) return false;
            Enemy.GiveRewards();
            Enemy = null;
            IsBattling = false;
            Program.Instance.StopTicking();
            return true;

        }

        private void MoveMagic()
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
                    // NOTE: PROTEGO HAS AN XSPEED OF 0 AND THEREFOR WONT RUN INTO THE ENEMY PLAYER
                    // THIS IS WHY WE DONT NEED TO HANDLE PROTEGO RUNNING INTO THE ENEMY.
                    // STUPEFY
                    if (s.Type == 1)
                    {
                        Enemy.Damage(Program.Player.DamageAmount);
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
                        Program.Player.Damage(10);
                    }
                    
                    // PETRIFICUS
                    if (s.Type == 3)
                    {
                        Program.Player.Stun(2);
                    }
                }
                
                // DIDNT RUN INTO ANYTHING
                myEnemySpell[i + s.XSpeed / 20] = s;
                myEnemySpell[i] = new Spell(0,0,s.FromPlayer);
            }
        }
        
    }
}