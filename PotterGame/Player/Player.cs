using PotterGame.Inventories;
using PotterGame.Inventories.Items;
using PotterGame.Player.Story;
using PotterGame.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using PotterGame.Inventories.Items.FoodItems;

namespace PotterGame.Player
{
    public class Player
    {
        public BaseContext Context { get; }
        public bool IsInventoryOpen;
        
        public BaseInventory OpenInventory { get; set; }
        public BaseInventory PlayerInventory { get; }
        public int Money { get; private set; }

        public Player()
        {
            Context = new MainStory();
            PlayerInventory = new Inventory("Inventory");
        }

        public void Start()
        {
            //Context.Start();
            
            PlayerInventory.AddItem(new Butterbeer());
            PlayerInventory.AddItem(new Tea());
            PlayerInventory.OpenInventory(0, 0);
        }
       

        private static void SendContext(BaseContext aContext)
        {
            
        }

        /// <summary>
        /// Sends a paused message across the screen
        /// </summary>
        public static void SendPaused()
        {
            TextUtils.SendMessage(new Text[] { new Text("Paused", 255, 215, 0, true), new Text("Harry-Potter", 255, 197, 0, true), new Text("- Liam Sjöholm", ColorCode.GREEN) }, TextType.CENTERED);
        }

        /// <summary>
        /// Sends controls message in the default format. 
        /// </summary>
        /// <param name="s">The message to be sent</param>
        public static void SendControls(string s)
        {
            TextUtils.SendMessage(new Text(s, ColorCode.BLUE), TextType.CONTROLS);
        }

        /// <summary>
        /// Opens the <c>BaseInventory</c> -> <paramref name="inventory"/>
        /// </summary>
        /// <param name="inventory">Inventory to open</param>
        public void InventoryOpened(BaseInventory inventory)
        {
            OpenInventory = inventory;
            IsInventoryOpen = true;
        }

        /// <summary>
        /// Closes the current inventory
        /// </summary>
        internal void CloseInventory()
        {
            IsInventoryOpen = false;
            SendContext(Context);
        }

        /// <summary>
        /// Adds <paramref name="aMoney"/> amount of money to the players balance.
        /// </summary>
        ///
        /// <param name="aMoney">A non-negative number that is >0</param>
        /// <exception cref="ArgumentException">thrown when number is negative or 0.</exception>
        public void AddMoney(int aMoney)
        {
            if(aMoney < 1)
                throw new ArgumentException("Number must be non-negative and more than 0");
            Money += aMoney;
        }

        /// <summary>
        /// Removes <paramref name="aMoney"/> amount of money from the players balance.
        /// </summary>
        ///
        /// <param name="aMoney">A non-negative number that is >0</param>
        /// <exception cref="ArgumentException">thrown when number is negative or 0.</exception>
        /// <returns>Boolean, True if players balance > <paramref name="aMoney"/>.</returns>
        public bool RemoveMoney(int aMoney)
        {
            if(aMoney < 1)
                throw new ArgumentException("Number must be non-negative and more than 0");
            if (Money < aMoney)
                return false;
            Money -= aMoney;
            return true;
        }
    }
}
