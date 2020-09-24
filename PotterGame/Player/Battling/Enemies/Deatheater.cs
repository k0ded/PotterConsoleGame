using PotterGame.Inventories;
using PotterGame.Inventories.Items.FoodItems;

namespace PotterGame.Player.Battling.Enemies
{
    public class Deatheater : BaseEnemy
    {

        public override void GiveRewards()
        {
            InventoryManager.PlayerInventory.AddItem(new Tea());
        }
    }
}