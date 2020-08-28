using ConsoleApp;
using PotterGame.Inventories;
using PotterGame.Inventories.Items;
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
        
        private PlayerController myController;
        private BaseContext myContext;

        public bool IsInventoryOpen = false;
        public BaseInventory OpenInventory { get; set; }
        public BaseInventory PlayerInventory { get; set; }
        public int Money { get; set; }

        public void Start()
        {
            PlayerInventory = new Inventory("Inventory");
            PlayerInventory.AddItem(new Butterbeer());
            PlayerInventory.AddItem(new Tea());
            PlayerInventory.OpenInventory(0, 0);
            myController = new PlayerController();
        }

        internal void SendMessage(Text[] aMessage)
        {
            Program.TextUtils.SendCenteredMessage(aMessage);
        }

        internal void SendInventory(Text[] aInventory)
        {
            Program.TextUtils.SendInventoryMessage(aInventory);
        }

        public void SendPaused()
        {
            Program.TextUtils.SendCenteredMessage(new Text[] { new Text("Paused", 255, 215, 0, true), new Text("Harry-Potter", 255, 197, 0, true), new Text("- Liam Sjöholm", ColorCode.GREEN) });
        }

        public void SendControls(string s)
        {
            Program.TextUtils.SendControlsMessage(new Text(s, ColorCode.BLUE));
        }

        public BaseInventory GetOpenInventory()
        {
            if (IsInventoryOpen)
                return OpenInventory;
            return null;
        }

        public void InventoryOpened(BaseInventory inventory)
        {
            OpenInventory = inventory;
            IsInventoryOpen = true;
        }

        internal void CloseInventory()
        {
            IsInventoryOpen = false;
            SendMessage(myContext.getPreviousStory());
        }

        public BaseContext GetContext()
        {
            return myContext;
        }

        internal void AddMoney(int money)
        {
            Money += money;
        }

        internal void RemoveMoney(int money)
        {
            Money -= money;
        }
    }
}
