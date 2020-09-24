using PotterGame.Utils;
using System;
using System.Collections.Generic;
using PotterGame.Inventories;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items.FoodItems;
using PotterGame.Inventories.Items.ShopItems;

namespace PotterGame.Player.Story
{
    public readonly struct Locations
    {
        public Locations(string aName, 
            ELocations aQLoc, 
            ELocations aWLoc, 
            ELocations aELoc, 
            ELocations aALoc,
            ELocations aSLoc,
            ELocations aDLoc, 
            Text[] aExplanation)
        {
            Name = aName;
            QLoc = aQLoc;
            WLoc = aWLoc;
            ELoc = aELoc;
            ALoc = aALoc;
            SLoc = aSLoc;
            DLoc = aDLoc;
            Explanation = aExplanation;
        }
        public Locations(string aName, ELocations aQLoc, ELocations aWLoc, ELocations aELoc, Text[] aExplanation)
        {
            Name = aName;
            QLoc = aQLoc;
            WLoc = aWLoc;
            ELoc = aELoc;
            ALoc = ELocations.NONE;
            SLoc = ELocations.NONE;
            DLoc = ELocations.NONE;
            Explanation = aExplanation;
        }
        public string Name { get; }
        public ELocations QLoc { get; }
        public ELocations WLoc { get; }
        public ELocations ELoc { get; }
        public ELocations ALoc { get; }
        public ELocations SLoc { get; }
        public ELocations DLoc { get; }
        public Text[] Explanation { get; }
    }

    public enum ELocations
    {
        #region Privet Drive
        
        PRIVET_DRIVE_HALL,
        PRIVET_DRIVE_OUTSIDE,
        PRIVET_DRIVE_SECOND_FLOOR,
        PRIVET_DRIVE_KITCHEN,
        PRIVET_DRIVE_DUDLEYS_ROOM,
        PRIVET_DRIVE_HARRYS_ROOM,
        
        #endregion

        #region London
        
        LONDON_KNIGHTBUS,
        LONDON_LEAKYCAULDRON,
        LONDON_LEAKYCAULDRON_APPARTMENTS,
        LONDON_DIAGONALLEY,
        LONDON_KINGSCROSS,
        
        DIAGON_ALLEY_SHOP,
        LEAKY_CAULDRON_SHOP,
        LEAKY_CAULDRON_APARTMENT_SHOP,

        #endregion
        
        NONE
    }

    internal class Exploration
    {
        private readonly Dictionary<ELocations, Locations> myLocations = new Dictionary<ELocations, Locations>();
        private Dictionary<ELocations, Shop> myShops = new Dictionary<ELocations, Shop>();
        private Dictionary<ELocations, ShopSelector> myShopSelectors = new Dictionary<ELocations, ShopSelector>();
        private ELocations myCurrentLocation = ELocations.PRIVET_DRIVE_HALL;

        private const string Controls = "[Q-D] Exploring";

        public void Load()
        {
            LoadLocations();
            LoadShops();
        }

        /// <summary>
        /// This loads the locations into the <c>myLocations</c> <c>Dictionary</c>.
        /// </summary>
        private void LoadLocations()
        {
            
            #region Privet Drive
            
            var privetDriveHall = new Locations(
                "4. Privet Drive - Hall",
                ELocations.PRIVET_DRIVE_OUTSIDE,
                ELocations.PRIVET_DRIVE_SECOND_FLOOR,
                ELocations.PRIVET_DRIVE_KITCHEN,
                new []
                {
                    new Text("Privet Drive"), 
                    new Text("You are standing in the hallway. " +
                             "Next to you there's a staircase with a small cupboard underneath it. " +
                             "At the end of the hallway theres a small see-through door leading into the kitchen. "),
                });

            var privetDriveOutside = new Locations(
                "4. Privet Drive - Outside",
                ELocations.PRIVET_DRIVE_HALL,
                ELocations.LONDON_KNIGHTBUS,
                ELocations.NONE,
                new []
                {
                    new Text("Privet Drive"), 
                    new Text("You are standing in the front yard. " +
                             "The house is made of bricks and it has a small garage on the right hand side. " +
                             "The grass in front of the house is tidy and emerald green. " +
                             "Down the street you see a large purple bus with a sign on the front that says: \"Knight Bus\". "),
                });

            var privetDriveSecondFloor = new Locations(
                "4. Privet Drive - Second Floor",
                ELocations.PRIVET_DRIVE_HALL,
                ELocations.PRIVET_DRIVE_DUDLEYS_ROOM,
                ELocations.PRIVET_DRIVE_HARRYS_ROOM,
                new []
                {
                    new Text("Privet Drive"), 
                    new Text("You are standing on the second floor of 4. Privet Drive. " +
                             "In front of you there's a hallway with multiple rooms along it. " +
                             "One of the rooms belong to Harry, one to Dudley and one to Vernon and Petunia"), 
                });

            var privetDriveHarrysRoom = new Locations(
                "4. Privet Drive - Harrys Room",
                ELocations.PRIVET_DRIVE_SECOND_FLOOR,
                ELocations.NONE,
                ELocations.NONE,
                new []
                {
                    new Text("Privet Drive"),
                    new Text("You are standing in a small room with a small bed, a desk and a small cupboard. " +
                             "This room belongs to the boy who lived, Harry Potter. " +
                             "It used to belong to Dudley way back in the day.")
                });
            
            var privetDriveDudleysRoom = new Locations(
                "4. Privet Drive - Dudleys Room",
                ELocations.PRIVET_DRIVE_SECOND_FLOOR,
                ELocations.NONE,
                ELocations.NONE,
                new []
                {
                    new Text("Privet Drive"), 
                    new Text("You are standing in a rather large room with a huge bed, a desk and a big closet. " +
                             "There are beanbags in the room pointed towards the TV as well as a mini fridge. " +
                             "This is the biggest room of the house, even bigger than Vernon and Petunias!") 
                });
            
            var privetDriveKitchen = new Locations(
                "4. Privet Drive - Kitchen",
                ELocations.PRIVET_DRIVE_HALL,
                ELocations.NONE,
                ELocations.NONE,
                new []
                {
                    new Text("Privet Drive"), 
                    new Text("You are standing in a rather small kitchen." +
                             " The kitchen has a small fridge, alot of cupboards and utensils. ")
                });
            
            myLocations.Add(ELocations.PRIVET_DRIVE_HALL, privetDriveHall);
            myLocations.Add(ELocations.PRIVET_DRIVE_OUTSIDE, privetDriveOutside);
            myLocations.Add(ELocations.PRIVET_DRIVE_SECOND_FLOOR, privetDriveSecondFloor);
            myLocations.Add(ELocations.PRIVET_DRIVE_HARRYS_ROOM, privetDriveHarrysRoom);
            myLocations.Add(ELocations.PRIVET_DRIVE_DUDLEYS_ROOM, privetDriveDudleysRoom);
            myLocations.Add(ELocations.PRIVET_DRIVE_KITCHEN, privetDriveKitchen);
            
            #endregion

            #region London

            var londonKnightBus = new Locations(
                "London - Knight Bus",
                ELocations.LONDON_LEAKYCAULDRON,
                ELocations.LONDON_KINGSCROSS,
                ELocations.NONE,
                new []
                {
                    new Text("Knight Bus"),
                    new Text("You travel in a purple bus, it shifts shape and slows time to get " +
                             "through the busy London traffic. " +
                             "There are multiple beds on the bus and a couple wizards and witches sleeping."), 
                });
            
            var leakyCauldron = new Locations(
                "London - Leaky Cauldron",
                ELocations.LONDON_KNIGHTBUS,
                ELocations.LONDON_DIAGONALLEY,
                ELocations.LONDON_LEAKYCAULDRON_APPARTMENTS,
                ELocations.LEAKY_CAULDRON_SHOP,
                ELocations.NONE,
                ELocations.NONE,
                new []
                {
                    new Text("Leaky Cauldron"),
                    new Text("You are standing in the entrance of the Leaky Cauldron. " +
                             "In the back there's a brick wall that leads to Diagon Alley. " +
                             "Upstairs there are apartments of different sizes. " +
                             "There's also a bar that sells primarily Butterbeer."), 
                });

            var diagonAlley = new Locations(
                "London - Diagon Alley",
                ELocations.LONDON_LEAKYCAULDRON,
                ELocations.DIAGON_ALLEY_SHOP,
                ELocations.NONE,
                new []
                {
                    new Text("Diagon Alley"),
                    new Text("You are standing in front of a brick wall. " +
                             "It slowly and magically opens to reveal a rather small market. " +
                             "On the end of the road this market lies on theres a rather large bank " +
                             "called Gringotts Bank"), 
                });
            
            var leakyCauldronApartments = new Locations(
                "London - Leaky Cauldron Apartments",
                ELocations.LONDON_LEAKYCAULDRON,
                ELocations.LEAKY_CAULDRON_APARTMENT_SHOP,
                ELocations.NONE,
                new []
                {
                    new Text(""),
                    new Text(""), 
                });
            
            // TODO: ADD PLATFORM 9 3/4
            var londonKingsCross = new Locations(
                "London - Kings Cross",
                ELocations.LONDON_KNIGHTBUS,
                ELocations.NONE,
                ELocations.NONE,
                new []
                {
                    new Text(""),
                    new Text(""), 
                });
            
            myLocations.Add(ELocations.LONDON_KNIGHTBUS, londonKnightBus);
            myLocations.Add(ELocations.LONDON_LEAKYCAULDRON, leakyCauldron);
            myLocations.Add(ELocations.LONDON_LEAKYCAULDRON_APPARTMENTS, leakyCauldronApartments);
            myLocations.Add(ELocations.LONDON_DIAGONALLEY, diagonAlley);
            myLocations.Add(ELocations.LONDON_KINGSCROSS, londonKingsCross);

            #endregion

            #region Generic

            var none = new Locations(
                " - ",
                ELocations.NONE,
                ELocations.NONE,
                ELocations.NONE,
                new []
                {
                    new Text("")
                });
            
            myLocations.Add(ELocations.NONE, none);

            #endregion
            
        }

        private void LoadShops()
        {
            #region Shop Locations

            Shop leakyCauldronBar = new Shop("Leaky Cauldron Bar");
            leakyCauldronBar.AddItem(new Butterbeer());
            leakyCauldronBar.AddItem(new Tea());
            myShops.Add(ELocations.LEAKY_CAULDRON_SHOP, leakyCauldronBar);

            #endregion
            
            
            #region Shop Selector
            
            
            
            ShopSelector DiagonAlley = new ShopSelector("Diagon Alley");
            DiagonAlley.AddItem(new OlivandersItem());
            DiagonAlley.AddItem(new GringottsItem());
            myShopSelectors.Add(ELocations.DIAGON_ALLEY_SHOP, DiagonAlley);
            
            #endregion
        } 

        public Text[] GetExplorationMessage()
        {
            var messages = new Text[7];
            var loc = myLocations[myCurrentLocation];

            messages[0] = new Text(loc.Name);
            messages[1] = loc.QLoc != ELocations.NONE ? GetLocationMessage('Q', loc.QLoc) : null;
            messages[2] = loc.WLoc != ELocations.NONE ? GetLocationMessage('W', loc.WLoc) : null;
            messages[3] = loc.ELoc != ELocations.NONE ? GetLocationMessage('E', loc.ELoc) : null;
            messages[4] = loc.ALoc != ELocations.NONE ? GetLocationMessage('A', loc.ALoc) : null;
            messages[5] = loc.SLoc != ELocations.NONE ? GetLocationMessage('S', loc.SLoc) : null;
            messages[6] = loc.DLoc != ELocations.NONE ? GetLocationMessage('D', loc.DLoc) : null;
            return messages;
        }

        public string GetControlsMessage()
        {
            return Controls;
        }

        private Text GetLocationMessage(char selectionLetter, ELocations aLocation)
        {
            string prefix = $"[{selectionLetter}] - Go -> ";
            
            switch (aLocation)
            {
                case ELocations.DIAGON_ALLEY_SHOP:
                    return new Text( prefix + "Diagon Alley Shop Selector");
                
                case ELocations.LEAKY_CAULDRON_SHOP:
                    return new Text(prefix + "Leaky Cauldron Bar");
                
                //MORE SHOPS...
                default:
                    var loc = myLocations[aLocation];
                    return new Text(prefix + loc.Name.Replace(
                        loc.Name.Substring(0, loc.Name.IndexOf("-", StringComparison.OrdinalIgnoreCase) + 2),
                        ""));
            }
        }

        public Text[] GetExplanationMessage()
        {
            return myLocations[myCurrentLocation].Explanation;
        }

        /// <summary>
        /// Gets the correct action for the location, such as setting the <c>myCurrentLocation</c> to
        /// something and executing the explore code by returning true
        /// </summary>
        /// <param name="aRunLocation">
        /// The location you want to get the action of (Like exploring to).
        /// </param>
        /// <returns> if it should explore or not (It shouldnt explore if it isnt a location but a shop instead) </returns>
        private bool RunAction(ELocations aRunLocation)
        {
            switch (aRunLocation)
            {
                case ELocations.DIAGON_ALLEY_SHOP:
                    myShopSelectors[aRunLocation].OpenInventory(true);
                    return false;
                case ELocations.LEAKY_CAULDRON_SHOP:
                    myShops[aRunLocation].OpenInventory(true);
                    return false;
                case ELocations.LEAKY_CAULDRON_APARTMENT_SHOP:
                    
                    return false;
                case ELocations.NONE:
                    return false;
                default:
                    myCurrentLocation = aRunLocation;
                    return true;
            }
        }

        public bool RunQAction()
        {
            var location = myLocations[myCurrentLocation];
            return RunAction(location.QLoc);
        }

        public bool RunWAction()
        {
            var location = myLocations[myCurrentLocation];
            return RunAction(location.WLoc);
        }

        public bool RunEAction()
        {
            var location = myLocations[myCurrentLocation];
            return RunAction(location.ELoc);
        }
        
        public bool RunAAction()
        {
            var location = myLocations[myCurrentLocation];
            return RunAction(location.ALoc);
        }

        public bool RunSAction()
        {
            var location = myLocations[myCurrentLocation];
            return RunAction(location.SLoc);
        }

        public bool RunDAction()
        {
            var location = myLocations[myCurrentLocation];
            return RunAction(location.DLoc);
        }
    }
}
