namespace PotterGame.Enemies
{
    public interface IBaseEnemy
    {
        int Difficulty { get; set; }
        bool IsDead { get; set; }

        void GiveRewards();
    }
}