using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public class Shop : BaseInventory
    {
        private bool myIgnoreBuy;
        public Shop(string aName, bool aIgnoreBuy = false) : base(
            aName,
            new Text(aName.PadRight(45).PadLeft(0) + $"({Player.Player.Money})"),
            new Text("     Item".PadRight(45) + "Price"))
        {
            myIgnoreBuy = aIgnoreBuy;
        }

        public override void RunInteractAction()
        {
            if (myIgnoreBuy)
                return;
            if (!Program.Player.RemoveMoney(Selected.Value))
            {
                TextUtils.SendMessage(new Text("Not enough money!"), TextType.ACTION);
                return;
            }
            
            InventoryManager.PlayerInventory.AddItem(Selected.Clone());
            TextUtils.SendMessage(new Text("+1 " + Selected.Name), TextType.ACTION);
        }

        public override void RunBackspaceAction()
        {
            if (InventoryManager.OpenInventory == this)
            {
                base.RunBackspaceAction();
                return;
            }
            InventoryManager.OpenInventory.OpenInventory(true);
        }
    }
}
