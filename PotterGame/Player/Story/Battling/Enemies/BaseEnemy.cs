using System;

namespace PotterGame.Player.Battling.Enemies
{
    public abstract class BaseEnemy
    {
        public static int Difficulty { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; } = 100 + 50 * Difficulty;
        public bool IsDead { get; set; }
        public double HealDamageAmount { get; set; }
        Random r = new Random();

        /// <summary>
        /// Gives the player their reward
        /// </summary>
        public abstract void GiveRewards();

        /// <summary>
        /// Heals the Enemy <paramref name="aAmount"/> of health
        /// </summary>
        /// <param name="aAmount">
        /// The amount of health to heal the Enemy
        /// </param>
        public void Heal(int aAmount)
        {
            Health = Math.Min(Health + aAmount, MaxHealth);
        }

        /// <summary>
        /// Damages the Enemy <paramref name="aAmount"/>
        /// </summary>
        /// <param name="aAmount">
        /// The amount to damage the enemy
        /// </param>
        public void Damage(int aAmount)
        {
            if (Health - aAmount <= 0)
            {
                IsDead = true;
            }

            Health -= aAmount;
        }

        /// <summary>
        /// Takes care of the enemies action during their turn.
        /// </summary>
        public void RunLogic()
        {
            if (r.NextDouble() > HealDamageAmount)
            {
                //Damage
                var dmg = (int) (r.Next(5, Battle.FightingConstant) * HealDamageAmount);
                Program.Player.Damage(dmg);
                Program.Player.CurrentBattle.RunAnimation(true, dmg);

            }else if (r.NextDouble() < HealDamageAmount)
            {
                //Heal
                var heal = (int) (r.Next(5, Battle.FightingConstant) * HealDamageAmount);
                Heal(heal);
            }
            else
            {
                //Reroll if healdamageamount == r.nextdouble.
                RunLogic();
            }
        }
    }
}