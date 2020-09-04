using System;

namespace PotterGame.Player.Battling.Enemies
{
    public abstract class BaseEnemy
    {
        public static int Difficulty { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; } = 100 + 50 * Difficulty;
        public bool IsDead { get; set; }
        public bool IsStunned { get; private set; }

        public abstract void GiveRewards();

        public void Heal(int aAmount)
        {
            Health = Math.Min(Health + aAmount, MaxHealth);
        }

        public void Damage(int aAmount)
        {
            if (Health - aAmount <= 0)
            {
                IsDead = true;
            }

            Health -= aAmount;
        }

        public void Stun(int aSeconds)
        {
            IsStunned = true;
        }
    }
}