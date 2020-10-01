namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Butterbeer : BaseItem
    {

        public override void InteractEvent()
        {
                   
        }

        public override void ReturnEvent()
        {
            IsOpened = false;
        }
    }
}
