using PotterGame.Inventories.Items.FoodItems;

namespace PotterGame.Player.Battling.Enemies
{
    public class Deatheater : BaseEnemy
    {

        public override void GiveRewards()
        {
            Program.Player.PlayerInventory.AddItem(new Tea());
        }
    }
}