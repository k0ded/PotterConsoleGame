using PotterGame.Inventories.Items.ShopItems;
using PotterGame.Utils;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public class ShopSelector : BaseInventory
    {
        private BaseShopItem openShop;
        private bool isOpen;
        
        public ShopSelector(string aName)
        {
            Name = aName;
            Header = new Text(Name.PadRight(45).PadLeft(0) + $"({Player.Player.Money})");
            HeaderFoot = new Text("     Item".PadRight(45) + "Price");
        }

        public override void RunInteractAction()
        {
            if (Selected is BaseShopItem item)
            {
                item.InteractEvent();
                openShop = item;
                isOpen = true;
            }
            base.RunInteractAction();
        }

        public override void RunBackspaceAction()
        {
            if (isOpen)
            {
                OpenInventory(true);
                isOpen = false;
                return;
            }
            base.RunBackspaceAction();
        }

        public override void RunWAction()
        {
            if (isOpen)
            {
                openShop.Shop.RunSAction();
                return;
            }
            base.RunWAction();
        }

        public override void RunSAction()
        {
            if (isOpen)
            {
                openShop.Shop.RunSAction();
                return;
            }
            base.RunSAction();
        }
    }
}
