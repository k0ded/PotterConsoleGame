using PotterGame.Inventories.Items;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.InventoryTypes
{
    public class Shop : BaseInventory
    {
        private bool myIgnoreBuy;
        private bool myRealShop;
        
        /// <summary>
        /// Konstruktorn är viktig för den bestämmer själva inventory delarna av affären.
        /// Vissa affärer kanske inte ska sälja saker men istället ge dig en sak eller
        /// kanske leda dig vidare till ett nytt ställe.
        /// </summary>
        /// <param name="aName">Namnet på affären</param>
        /// <param name="aIgnoreBuy">Om den ska ignorera att ge dig ett item</param>
        /// <param name="aRealShop">Om affären ska användas som affär eller inte</param>
        public Shop(string aName, bool aIgnoreBuy = false, bool aRealShop = true) : base(
            aName,
            new Text(aName.PadRight(45).PadLeft(0) + "(%money%)"),
            aRealShop ? 
                new Text("     Item".PadRight(45) + "Price") : new Text("     Item".PadRight(45)))
        {
            myIgnoreBuy = aIgnoreBuy;
            myRealShop = aRealShop;
        }

        /// <summary>
        /// Den här ser till så att vi får rätt text för shoppen. Den använder det vi
        /// gav i konstruktorn och lägger på det här.
        /// </summary>
        /// <param name="aItem">Det item du vill ha ShopTexten till</param>
        /// <param name="aSelected">Om itemet är selectad eller inte</param>
        /// <returns>En text som är anpassad för det item man gav</returns>
        protected override Text GetItemName(BaseItem aItem, bool aSelected)
        {
            var itemName = $"[{aItem.Name}]";
            var value = myRealShop ? $"({aItem.Value})" : "";
            
            if (!aSelected)
                return new Text(("   " + itemName + "   ").PadRight(45) + value, 128, 128, 128, true);
            const string prefix = ">> ";
            const string suffix = " <<";
            Selected = aItem;
            return new Text((prefix + itemName + suffix).PadRight(45) + value, ColorCode.WHITE);
        }

        /// <summary>
        /// Denna overriden ser till så att allt sker som det ska. Är shoppen flaggad till att
        /// ignorera köp så kör den selectade itemets interactevent så vi kan lägga på funktionallitet.
        /// </summary>
        public override void RunInteractAction()
        {
            if (myIgnoreBuy)
            {
                Selected.InteractEvent();
                return;
            }
            if (!Program.Player.RemoveMoney(Selected.Value))
            {
                TextUtils.SendMessage(new Text("Not enough money!"), TextType.ACTION);
                return;
            }
            
            InventoryManager.PlayerInventory.AddItem(Selected.Clone());
            TextUtils.SendMessage(new Text("+1 " + Selected.Name), TextType.ACTION);
        }

        /// <summary>
        /// Overriden är viktig för att vi ska kunna lägga Shop objekt i en ShopSelector.
        /// Denna gör så att man kan gå fram och tillbaka i en ShopSelector om man är
        /// i en ShopSelector.
        /// </summary>
        public override void RunBackspaceAction()
        {
            if (InventoryManager.OpenInventory == this)
            {
                base.RunBackspaceAction();
                return;
            }
            InventoryManager.OpenInventory.OpenInventory(true);
        }
    }
}
