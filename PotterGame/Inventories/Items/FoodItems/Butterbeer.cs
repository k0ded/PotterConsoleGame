namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Butterbeer : BaseItem
    {
        public bool IsOpened { get; set; } = false;

        public int Value { get; } = 7;
        public int Count { get; set; } = 1;
        public string Name { get; } = "Butterbeer";
        public string Controls { get; } =   "    [ENTER] - Consume                       [BACKSPACE] - Back    ";

        public override void InteractEvent()
        {
                   
        }

        public override void ReturnEvent()
        {
            IsOpened = false;
        }
    }
}
