using ConsoleApp;
using PotterGame.Inventories;
using PotterGame.Player.Story;
using PotterGame.Utils;
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
            context.Start();
            new PlayerController();
        }

        internal void SendMessage(Text[] aMessage)
        {
            Program.TextUtils.SendCenteredMessage(aMessage);
        }

        internal void SendInventory(Text[] aInventory)
        {
            Program.TextUtils.SendInventoryMessage(aInventory);
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
            SendMessage(context.getPreviousStory());
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
