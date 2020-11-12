using System;
using System.Diagnostics;
using PotterGame.Inventories;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items.ShopItems.OlivandersItems.Wands;
using PotterGame.Player.Story;
using PotterGame.Utils.Text;

namespace PotterGame.Player
{
    public class Player
    {
        public BaseContext Context { get; }
        public bool SeizeInput { get; set; }
        public int Money { get; private set; } = 1000;
        public int Stamina { get; private set; } = 105;
        public int MaxStamina { get; }= 100;
        public Wand? PlayerWand { get; set; }

        private Menu myMenu;
        
        public Player()
        {
            Context = new MainStory();
        }

        public void Start()
        {
            Program.Player = this;
            InventoryManager.PlayerInventory = new Inventory("Inventory");
            InventoryManager.OpenInventory = InventoryManager.PlayerInventory;
            InventoryManager.IsInventoryOpen = false;
            Console.Clear();
            Console.CursorVisible = false;
            Context.Start();
        }

        public void StartMenu()
        {
            myMenu = new Menu();
            StartMenuReload();
        }
        
        public void StartMenuReload()
        {
            myMenu.OpenInventory(true);
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
            TextUtils.SendMessage(new Text(Program.Player.Stamina + "/" + Program.Player.MaxStamina), TextType.HUD);
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
        /// Ändrar staminan med <paramref name="aAmount"/>
        /// </summary>
        public void ChangeStamina(int aAmount)
        {
            if (Stamina + aAmount > MaxStamina)
            {
                Stamina = MaxStamina;
            }else if (Stamina + aAmount < 0)
            {
                //DIE
                Program.SendLoseMessage();
            }else
                Stamina += aAmount;
        }
    }
}