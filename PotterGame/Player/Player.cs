using System;
using PotterGame.Inventories;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items.ShopItems.OlivandersItems.Wands;
using PotterGame.Player.Story;
using PotterGame.Player.Story.Exploring;
using PotterGame.Utils.Text;

namespace PotterGame.Player
{
    public class Player
    {
        public BaseContext Context { get; }
        public bool SeizeInput { get; set; }
        public int Money { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; }= 100;
        public Wand? PlayerWand { get; set; }

        private Menu menu;
        
        public Player()
        {
            Context = new MainStory();
            Program.Player = this;
            InventoryManager.PlayerInventory = new Inventory("Inventory");
            InventoryManager.OpenInventory = InventoryManager.PlayerInventory;
        }

        public void Start()
        {
            InventoryManager.OpenInventory = InventoryManager.PlayerInventory;
            InventoryManager.IsInventoryOpen = false;
            Console.Clear();
            Console.CursorVisible = false;
            Context.Start();
        }

        public void StartMenu()
        {
            menu = new Menu();
            StartMenuReload();
        }
        
        public void StartMenuReload()
        {
            menu.OpenInventory(true);
            PlayerController.MakeSelection();
        }

        /// <summary>
        /// Sends the previous state of the context to the screen.
        /// </summary>
        /// <param name="aContext"></param>
        private static void SendContext(BaseContext aContext)
        {
            Console.Clear();
            Text[] previousCenteredMessage;
            previousCenteredMessage = aContext.PreviousCenteredMessage;
            var previousControls = aContext.PreviousControlsMessage;
            var previousExplorationMessage = aContext.PreviousExplorationMessage;
            var previousExplanationMessage = aContext.PreviousExplanationMessage;
            var previousLetterMessage = aContext.PreviousLetterMessage;
            var previousMissionMessage = aContext.PreviousMissionMessage;
            var previousDangerScaleMessage = aContext.PreviousHeaderBarMessage;

            if (previousCenteredMessage != null) 
                TextUtils.SendMessage(previousCenteredMessage, TextType.CENTERED);

            if (previousControls != null) 
                TextUtils.SendMessage(previousControls, TextType.CONTROLS);

            if (previousExplorationMessage != null) 
                TextUtils.SendMessage(previousExplorationMessage, TextType.EXPLORATION);
            
            if(previousExplanationMessage != null)
                TextUtils.SendMessage(previousExplanationMessage, TextType.EXPLANATION);

            if (previousLetterMessage != null) 
                TextUtils.SendMessage(previousLetterMessage, TextType.LETTER_INSTANT);

            if (previousMissionMessage != null) 
                TextUtils.SendMessage(previousMissionMessage, TextType.MISSION);
            
            if(previousDangerScaleMessage != null)
                TextUtils.SendMessage(previousDangerScaleMessage, TextType.HEADERBAR);
        }

        /// <summary>
        /// Sends a paused message across the screen
        /// </summary>
        public static void SendPaused()
        {
            TextUtils.SendMessage(new[]
            {
                new Text("Paused", 255, 215, 0, true),
                new Text("Harry-Potter", 255, 197, 0, true),
                new Text("- Liam Sjöholm", ColorCode.GREEN)
            }, TextType.CENTERED);
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
            InventoryManager.OpenInventory = inventory;
            InventoryManager.IsInventoryOpen = true;
        }

        /// <summary>
        /// Closes the current inventory
        /// </summary>
        internal void CloseInventory()
        {
            InventoryManager.IsInventoryOpen = false;
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
            if (Money < aMoney)
                return false;
            Money -= aMoney;
            return true;
        }

        /// <summary>
        /// Adds <paramref name="aAmount"/> amount of money to the players balance.
        /// </summary>
        ///
        /// <param name="aAmount">A non-negative number that is >0</param>
        /// <exception cref="ArgumentException">thrown when number is negative or 0.</exception>
        public void Heal(int aAmount)
        {
            if (aAmount < 1)
                throw new ArgumentException("Number must be non-negative and more than 0");
            if (Health + aAmount > MaxHealth)
            {
                Health = MaxHealth;
                return;
            }

            Health += aAmount;
        }

        /// <summary>
        /// Removes <paramref name="aAmount"/> amount of money from the players balance.
        /// </summary>
        ///
        /// <param name="aAmount">A non-negative number that is >0</param>
        /// <exception cref="ArgumentException">thrown when number is negative or 0.</exception>
        /// <returns>Boolean, True if players balance > <paramref name="aAmount"/>.</returns>
        public bool Damage(int aAmount)
        {
            if (aAmount < 1)
                throw new ArgumentException("Number must be non-negative and more than 0");
            if (Health < aAmount)
            {
                ((MainStory)Context).Exploration.SetLocation(ELocations.HOGWARTS_HOSPITAL_WING);
                ((MainStory)Context).Explore();
                Health = MaxHealth;
                return false;
            }

            Health -= aAmount;
            return true;
        }
    }
}