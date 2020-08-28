using PotterGame.Inventories.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Inventories
{
    class BaseInventory
    {
        public  List<IBaseItem> content;

        public int Selection = 0;
        public int Offset = 0;

        public IBaseItem Selected;
        protected String myName;

        protected void SendMenu(string[] message)
        {
            Program.getPlayer().SendMenu(message);
        }

        public void OpenInventory(int selection, int offset)
        {
            Player.Player p = Program.getPlayer();
            p.OpenInventory(this);
            Selection = selection;
            Offset = offset;
            String[] inventory = new String[Math.Min(2 + content.Count + 2, 11)];

            // Den här visar om jag ska visa en pil ner eller inte (Det visas om det finns fler items längre upp)
            bool canScrollDown = (content.Count - offset) - 6 > 0;

            // Den här visar om jag ska visa en pil up eller inte (Det visas om det finns fler items längre upp)
            bool canScrollUp = offset > 0;

            // Har vi färre än 6 eller färre items så kommer inventoryt visas på exakt samma sätt.
            if (content.Count < 6)
            {
                //Ser till så att man kan ha olika instanser av inventoryt till t.ex Affärer. Så att det inte bara
                //Säger inventory men kan också bli t.ex Shop utan att förstöra formatet.
                inventory[0] = myName + "                                 💰 ".Substring(0, myName.Length) + "(" + p.GetMoney() + ")";
                inventory[1] = "     Item                           Value  Count";
                for (int i = 0; i < content.Count; i++)
                {
                    inventory[i + 2] = getItemName(content.ElementAt(i), selection == i);
                    inventory[i + 3] = " [W/S] To scroll up and down in the inventory";
                }
                p.SendMenu(inventory);
                return;
            }

            // Det är inte jättemånga items som ska vara i listan så jag valde att lägga i dom i dom rätta platserna direkt.
            inventory[0] = myName + ("                                💰 ".Substring(0, myName.Length)) + "(" + p.GetMoney() + ")";
            inventory[1] = "     Item                           Price";
            inventory[2] = canScrollUp ? "           ↑" : "            ";

            for (var i = 0; i < Math.Min(inventory.Length - offset, 6); i++)
            {
                IBaseItem item = content.ElementAt(i);
                inventory[i + 3] = getItemName(item, selection == i);
            }

            inventory[9] = canScrollDown ? "           ↓" : "            ";
            inventory[10] = " [W/S] To scroll up and down in the inventory";

            p.SendMenu(inventory);

        }

        // Funktionen sätter ihop de olika sakerna på ett sätt som gör att de ligger på samma rad oberoende
        // på hur lång texten innan är. (Så länge det är inom mängden blanksteg det finns)
        private String getItemName(IBaseItem item, bool selected)
        {
            String prefix = "     ";
            String maxSuffix = "                              ";
            String itemName = "[" + item.Name + "]";
            if (selected)
            {
                prefix = "  >> ";
                maxSuffix = " <<                           ";
                Selected = item;
            }

            String s = "     ";
            s = s.Substring(0, Math.Max(s.Length - String.Concat(item.Value).Length, 0));

            return prefix + itemName + maxSuffix.Substring(0, maxSuffix.Length - item.Name.Length) + "(" + item.Value + ")";
        }

        public void AddItem(IBaseItem i)
        {
            // Iterates through the content list to see if the item already exists
            foreach (IBaseItem item in content)
            {
                // The list doesnt contain items and goes over to adding one
                if (item == null)
                    break;
                // The list contains the item and adds one to the count of the item
                if (item.Name == i.Name)
                {
                    item.Count++;
                    return;
                }
            }
            content.Add(i);
        }

        public virtual void RunInteractAction()
        {
            Selected.InteractEvent();
        }

        public virtual void RunBackspaceAction()
        {
            Program.getPlayer().CloseInventory();
        }

        public virtual void RunWAction()
        {

        }

        public virtual void RunSAction()
        {

        }
    }
}
