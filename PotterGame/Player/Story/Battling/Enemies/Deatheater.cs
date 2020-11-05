using PotterGame.Inventories;
using PotterGame.Inventories.Items.FoodItems;

namespace PotterGame.Player.Story.Battling.Enemies
{
    public class Deatheater : BaseEnemy
    {

        public Deatheater()
        {
            HealDamageAmount = 0.6;
        }

        public override void GiveRewards()
        {
            InventoryManager.PlayerInventory.AddItem(new Tea());
            Program.Player.AddMoney(50);
        }
    }
}