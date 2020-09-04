using PotterGame.Inventories.Items.FoodItems;

namespace PotterGame.Player.Battling.Enemies
{
    public class Deatheater : BaseEnemy
    {

        public override void GiveRewards()
        {
            Program.GetPlayer().PlayerInventory.AddItem(new Tea());
        }
    }
}