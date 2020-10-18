namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Butterbeer : BaseItem
    {
        public Butterbeer() : base("Butterbeer", 10) {}
        
        public override void InteractEvent()
        {
                   
        }

        public override void ReturnEvent()
        {
            IsOpened = false;
        }
    }
}
