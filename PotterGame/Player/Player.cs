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
        BaseInventory openInventory;
        BaseInventory playerInventory;

        bool isInventoryOpen = false;
        BaseContext context;
        
        int money;

        public Player()
        {
            playerInventory = new Inventory("Inventory");
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

        public BaseInventory GetOpenInventory()
        {
            if (isInventoryOpen)
                return openInventory;
            return null;
        }

        internal void OpenInventory(BaseInventory inventory)
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
        public BaseContext GetContext()
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

        internal BaseInventory GetPlayerInventory()
        {
            return playerInventory;
        }

        internal Boolean IsInventoryOpen()
        {
            return isInventoryOpen;
        }
    }
}
