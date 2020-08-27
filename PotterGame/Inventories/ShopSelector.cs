using ConsoleApp;
using PotterGame;
using PotterGame.Inventories;
using PotterGame.Inventories.Items;
using PotterGame.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class ShopSelector : baseInventory
    {
        String name;

        Shop selected;

        public ShopSelector(String name)
        {
            this.name = name;
        }

        //Används när inventoryt ska öppnas. Den andra används för att man ska kunna scrolla.
        public void OpenInventory()
        {
            OpenInventory(0, 0);
        }

        public int selection = 0;
        public int offset = 0;
        // Används om man ska flytta upp och ner.
        public void OpenInventory(int selection, int offset)
        {
            Player p = Program.getPlayer();
            p.OpenInventory(this);
            this.selection = selection;
            this.offset = offset;
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
                inventory[0] = name + "                                 💰 ".Substring(0, name.Length) + "(" + p.GetMoney() + ")";
                inventory[1] = "     Item                           ";
                for (int i = 0; i < content.Count; i++)
                {
                    inventory[i + 2] = getItemName(content.ElementAt(i), 0, 1, selection == i);
                    inventory[i + 3] = " [W/S] To scroll up and down in the inventory";
                }
                p.SendMenu(inventory);
                return;
            }

            // Det är inte jättemånga items som ska vara i listan så jag valde att lägga i dom i dom rätta platserna direkt.
            // Annars hade man kunnat använda en for loop för att göra det.
            inventory[0] = name + ("                                💰 ".Substring(0, name.Length)) + "(" + p.GetMoney() + ")";
            inventory[1] = "     Item                           ";
            inventory[2] = canScrollUp ? "           ↑" : "            ";
            baseItem i0 = content.ElementAt(0 + offset);
            baseItem i1 = content.ElementAt(1 + offset);
            baseItem i2 = content.ElementAt(2 + offset);
            baseItem i3 = content.ElementAt(3 + offset);
            baseItem i4 = content.ElementAt(4 + offset);
            baseItem i5 = content.ElementAt(5 + offset);

            inventory[3] = getItemName(i0, 0, 1, selection == 0);
            inventory[4] = getItemName(i1, 0, 1, selection == 1);
            inventory[5] = getItemName(i2, 0, 1, selection == 2);
            inventory[6] = getItemName(i3, 0, 1, selection == 3);
            inventory[7] = getItemName(i4, 0, 1, selection == 4);
            inventory[8] = getItemName(i5, 0, 1, selection == 5);
            inventory[9] = canScrollDown ? "           ↓" : "            ";
            inventory[10] = " [W/S] To scroll up and down in the inventory";

            p.SendMenu(inventory);

        }

        // Funktionen sätter ihop de olika sakerna på ett sätt som gör att de ligger på samma rad oberoende
        // på hur lång texten innan är. (Så länge det är inom mängden blanksteg det finns)
        private String getItemName(baseItem item, int value, int count, bool selected)
        {
            String prefix = "     ";
            String maxSuffix = " ";
            String itemName = "[" + item.GetName() + "]";
            if (selected)
            {
                prefix = "  >> ";
                maxSuffix = " <<";
                this.selected = Program.getShop(item);
            }

            return prefix + itemName + maxSuffix;
        }

        public void addItem(baseItem i)
        {
            content.Add(i);
        }

        public override void RunWAction()
        {
            bool canScrollUp = offset > 0;
            if (canScrollUp && selection == 1)
            {
                OpenInventory(selection, offset - 1);
                return;
            }
            if (selection == 0)
            {
                OpenInventory(0, offset);
            }
            OpenInventory(selection - 1, offset);

        }

        public override void RunSAction()
        {
            bool canScrollDown = (content.Count - offset) - 6 > 0;
            if (canScrollDown && selection == 4)
            {
                OpenInventory(selection, offset + 1);
                return;
            }
            if (selection == 5 || selection == content.Count - 1)
            {
                OpenInventory(selection, offset);
                return;
            }
            OpenInventory(selection + 1, offset);

        }

        public override void RunInteractAction()
        {
            selected.openInventory();
        }

    }
}
