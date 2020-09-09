using System.Collections.Generic;
using PotterGame.Inventories.Items;
using PotterGame.Inventories.Items.ShopItems;

namespace PotterGame.Inventories
{
    internal class ShopSelector : BaseInventory
    {
        public ShopSelector(string aName)
        {
            Name = aName;
            Player = Program.GetPlayer();
        }
        
        public override void OpenInventory()
        {
            if (Player == null)
                Player = Program.GetPlayer();
            Player.InventoryOpened(this);
            OpenInventory(0,0);
        }

        public override void RunInteractAction()
        {
            ((IShopItem) Selected).InteractEvent();
        }

    }
}
