using PotterGame.Inventories.Items.ShopItems;
using PotterGame.Utils;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public class ShopSelector : BaseInventory
    {
        private BaseShopItem openShop;
        private bool isOpen;
        
        public ShopSelector(string aName) : base(
            aName,
            new Text(aName.PadRight(45).PadLeft(0) + "(%money%)"),
            new Text("     Item".PadRight(45) + "Price")) {}

        public override void RunReloadAction()
        {
            if (isOpen)
            {
                openShop.Shop.RunReloadAction();
                return;
            }
            base.RunReloadAction();
        }

        public override void RunInteractAction()
        {
            if (Selected is BaseShopItem item && !isOpen)
            {
                item.InteractEvent();
                openShop = item;
                isOpen = true;
            }
            else
                base.RunInteractAction();
        }

        public override void RunBackspaceAction()
        {
            if (isOpen)
            {
                isOpen = false;
                OpenInventory(true);
                return;
            }

            Selected.IsOpened = false;
            base.RunBackspaceAction();
        }

        public override void RunWAction()
        {
            if (isOpen)
            {
                openShop.Shop.RunWAction();
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
