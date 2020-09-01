namespace PotterGame.Player.Battling.Enemies
{
    public interface IBaseEnemy
    {
        int Difficulty { get; set; }
        int Health { get; set; }
        int MaxHealth { get; }
        bool IsDead { get; set; }

        void GiveRewards();
        void Heal(int aAmount);
        void Damage(int aAmount);
        void Stun(int aSeconds);
    }
}