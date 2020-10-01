using System;

namespace PotterGame.Inventories.Items.FoodItems
{
    internal class Tea : BaseItem
    {

        public Tea()
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
