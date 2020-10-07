using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public class Shop : BaseInventory
    {

        public Shop(string aName) : base(
            aName,
            new Text(aName.PadRight(45).PadLeft(0) + $"({Player.Player.Money})"),
            new Text("     Item".PadRight(45) + "Price")) {}

        public override void RunInteractAction()
        {
            if (!Program.Player.RemoveMoney(Selected.Value)) return;
            
            InventoryManager.PlayerInventory.AddItem(Selected.Clone());
            TextUtils.SendMessage(new Text("+1 " + Selected.Name), TextType.DEBUG);
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
