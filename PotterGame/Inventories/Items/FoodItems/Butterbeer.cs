namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Butterbeer : IBaseItem
    {
        public bool IsOpened { get; set; } = false;

        public int Value { get; } = 7;
        public int Count { get; set; } = 1;
        public string Name { get; } = "Butterbeer";
        public string Controls { get; } =   "    [ENTER] - Consume                       [BACKSPACE] - Back    ";

        public void InteractEvent()
        {
                   
        }

        public void ReturnEvent()
        {
            IsOpened = false;
        }
    }
}
