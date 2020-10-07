namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Butterbeer : BaseItem
    {
        public Butterbeer() : base("Butterbeer") {}
        
        public override void InteractEvent()
        {
                   
        }

        public override void ReturnEvent()
        {
            IsOpened = false;
        }
    }
}
