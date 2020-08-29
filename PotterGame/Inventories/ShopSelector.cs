namespace PotterGame.Inventories
{
    internal class ShopSelector : BaseInventory
    {

        public ShopSelector(string aName)
        {
            Name = aName;
            Player = Program.GetPlayer();
        }

        public override void RunInteractAction()
        {
            Selected.InteractEvent();
        }

    }
}
