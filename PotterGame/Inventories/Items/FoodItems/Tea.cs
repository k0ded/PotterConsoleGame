using System;

namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Tea : IBaseItem
    {
        public bool IsOpened { get; set; }

        public int Value { get; } = 5;

        public int Count { get; set; } = 1;

        public string Name { get; } = "Tea";

        public string Controls { get; } = "    [ENTER] - Consume                       [BACKSPACE] - Back    ";

        public void InteractEvent()
        {
            throw new NotImplementedException();
        }

        public void ReturnEvent()
        {
            throw new NotImplementedException();
        }
    }
}
