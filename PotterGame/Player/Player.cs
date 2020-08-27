using ConsoleApp;
using PotterGame.Inventories;
using PotterGame.Player.Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Player
{
    class Player
    {
        baseInventory openInventory;
        baseInventory playerInventory;

        bool isInventoryOpen = false;
        baseContext context;
        
        int money;

        public Player()
        {
            playerInventory = new Inventory("Inventory", 20);
            context = new MainStory();
            context.start();
            new PlayerController();
        }

        internal void SendMenu(string[] inventory)
        {
            foreach(string s in inventory)
            {
                Console.WriteLine(s);
            }
        }

        public baseInventory GetOpenInventory()
        {
            if (isInventoryOpen)
                return openInventory;
            return null;
        }

        internal void OpenInventory(baseInventory inventory)
        {
            openInventory = inventory;
            isInventoryOpen = true;
        }

        internal void CloseInventory()
        {
            isInventoryOpen = false;
            SendMenu(context.getPreviousStory());
        }

        public int GetMoney()
        {
            return money;
        } 
        public baseContext GetContext()
        {
            return context;
        }

        internal void AddMoney(int money)
        {
            this.money += money;
        }

        internal void RemoveMoney(int money)
        {
            this.money -= money;
        }

        internal baseInventory GetPlayerInventory()
        {
            return playerInventory;
        }

        internal Boolean IsInventoryOpen()
        {
            return isInventoryOpen;
        }
    }
}
