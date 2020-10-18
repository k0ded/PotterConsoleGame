using System;

namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Tea : BaseItem
    {

        public Tea() : base("Tea", 5)
        {
            Controls = "    [ENTER] - Consume                       [BACKSPACE] - Back    ";
        }
            
        public override void InteractEvent()
        {
            
        }

        public override void ReturnEvent()
        {
            
        }
    }
}
