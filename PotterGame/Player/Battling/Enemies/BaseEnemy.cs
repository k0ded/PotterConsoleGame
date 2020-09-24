using System;

namespace PotterGame.Player.Battling.Enemies
{
    public abstract class BaseEnemy
    {
        public static int Difficulty { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; } = 100 + 50 * Difficulty;
        public bool IsDead { get; set; }
        public int IsStunned { get; private set; }

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
        /// Stuns the Enemy for <paramref name="aSeconds"/> amount of time
        /// </summary>
        /// <param name="aSeconds">
        /// Amount of seconds to stun the enemy
        /// </param>
        public void Stun(int aSeconds)
        {
            IsStunned = DateTime.Now.Second + aSeconds;
        }
    }
}