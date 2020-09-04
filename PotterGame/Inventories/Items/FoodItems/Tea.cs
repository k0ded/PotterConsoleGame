using System;

namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Tea : BaseItem
    {
        public bool IsOpened { get; set; }

        public int Value { get; } = 5;

        public int Count { get; set; } = 1;

        public string Name { get; } = "Tea";

        public string Controls { get; } = "    [ENTER] - Consume                       [BACKSPACE] - Back    ";

        public override void InteractEvent()
        {
            throw new NotImplementedException();
        }

        public override void ReturnEvent()
        {
            throw new NotImplementedException();
        }
    }
}
