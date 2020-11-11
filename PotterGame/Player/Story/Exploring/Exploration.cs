using System;
using System.Collections.Generic;
using System.Text;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items.FoodItems;
using PotterGame.Inventories.Items.ShopItems;
using PotterGame.Utils.Dungeons;
using PotterGame.Utils.Text;

namespace PotterGame.Player.Story.Exploring
{

    internal class Exploration
    {
        private readonly Dictionary<ELocations, Locations> myLocations = new Dictionary<ELocations, Locations>();
        private Dictionary<ELocations, Shop> myShops = new Dictionary<ELocations, Shop>();
        private Dictionary<ELocations, ShopSelector> myShopSelectors = new Dictionary<ELocations, ShopSelector>();
        private ELocations myCurrentLocation = ELocations.PRIVET_DRIVE_HALL;

        private const string Controls = "[Q-D] Exploring    [I] - Inventory";

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
                ELocations.PRIVET_DRIVE_OUTSIDE,
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
                ELocations.NONE,
                ELocations.NONE,
                new []
                {
                    new Text("Apartments"),
                    new Text("You are standing in a narrow hallway with multiple apartments on either side. " +
                             "You hear multiple wizards and witches on either side shouting/talking."), 
                });
            
            var londonKingsCross = new Locations(
                "London - Kings Cross",
                ELocations.LONDON_KNIGHTBUS,
                ELocations.HOGWARTS_PLATFORM,
                ELocations.NONE,
                new []
                {
                    new Text("Kings Cross"),
                    new Text("You are standing in a large hall with trains coming from one side. " +
                             "The station is filled with muggles everywhere. " +
                             "There are also multiple restaurants and cafés around the place."), 
                });
            
            myLocations.Add(ELocations.LONDON_KNIGHTBUS, londonKnightBus);
            myLocations.Add(ELocations.LONDON_LEAKYCAULDRON, leakyCauldron);
            myLocations.Add(ELocations.LONDON_LEAKYCAULDRON_APPARTMENTS, leakyCauldronApartments);
            myLocations.Add(ELocations.LONDON_DIAGONALLEY, diagonAlley);
            myLocations.Add(ELocations.LONDON_KINGSCROSS, londonKingsCross);

            #endregion

            #region Hogwarts

            var hogwartsPlatform = new Locations(
                "Hogwarts - Platform 9 3/4",
                ELocations.LONDON_KINGSCROSS,
                ELocations.HOGWARTS_EXPRESS,
                ELocations.NONE,
                new []
                {
                    new Text("Platform 9 3/4"), 
                    new Text("You ran through a wall and ended up next to a red and black train. " +
                             "The hidden platform has a lot of witches and wizards entering almost every minute."), 
                });
            
            var hogwartsExpress = new Locations(
                "Hogwarts - Express",
                ELocations.HOGWARTS_PLATFORM,
                ELocations.HOGWARTS_STATION,
                ELocations.NONE,
                new []
                {
                    new Text("Hogwarts Express"), 
                    new Text("You've entered the train and you seat yourself in an empty compartment. " +
                             "Today the trolley lady doesn't show up, wonder what happened to her."), 
                });
            
            var hogwartsStation = new Locations(
                "Hogsmeade - Station",
                ELocations.HOGWARTS_EXPRESS,
                ELocations.HOGWARTS_GATES,
                ELocations.NONE,
                new []
                {
                    new Text("Hogsmeade Station"),
                    new Text("You're now standing in a tiny little village with a bunch of restaurants lying around. " +
                             "There are also multiple paths leaving the village but only one seems to be open!") 
                });
            
            var hogwartsGates =  new Locations(
                "Hogwarts - Gates",
                ELocations.HOGWARTS_STATION,
                ELocations.HOGWARTS_BOATHOUSE,
                ELocations.NONE,
                new []
                {
                    new Text("Gates"), 
                    new Text("You're standing in front of two large statues. " +
                             "You're right outside the Hogwarts grounds and ready to go in. ") 
                });
            
            var hogwartsBoatHouse = new Locations(
                "Hogwarts - Boat House",
                ELocations.HOGWARTS_GATES,
                ELocations.HOGWARTS_GREATHALL,
                ELocations.NONE,
                new []
                {
                    new Text("Boat house"), 
                    new Text("You are sitting in a boat with a bunch of first years. " +
                             "Hagrid is in the lead getting you to the great hall!")
                });
            
            var hogwartsGreatHall = new Locations(
                "Hogwarts - Great Hall",
                ELocations.HOGWARTS_BOATHOUSE,
                ELocations.HOGWARTS_QUIDDITCH,
                ELocations.HOGWARTS_HAGRIDSHUT,
                ELocations.HOGWARTS_HOSPITAL_WING,
                ELocations.HOGWARTS_FORBIDDEN_FOREST,
                ELocations.NONE,
                new []
                {
                    new Text("Great hall"),
                    new Text("You are standing in the great hall of Hogwarts. " +
                             "This is the hub to everyone and everything. " +
                             "This is also where sorting happens! [UNIMPLEMENTED]") 
                });
            
            var hogwartsQuidditch = new Locations(
                "Hogwarts - Quidditch Arena",
                ELocations.HOGWARTS_GREATHALL,
                ELocations.HOGWARTS_HOSPITAL_WING,
                ELocations.NONE,
                new []
                {
                    new Text("Quidditch Arena"),
                    new Text("You are standing in the middle of an empty quidditch arena. " +
                             "The grass is very tidy for a place people would fall out of the sky. ") 
                });
            
            var hogwartsHagridsHut = new Locations(
                "Hogwarts - Hagrids Hut",
                ELocations.HOGWARTS_GREATHALL,
                ELocations.HOGWARTS_FORBIDDEN_FOREST,
                ELocations.HOGWARTS_HOSPITAL_WING,
                new []
                {
                    new Text("Hagrid's hut"),
                    new Text("You are standing outside a simple and small hut that belongs to the gamekeeper. " +
                             "Hagrid is the current gamekeeper and he helps take care of the different animals you may come across.") 
                });
            
            var hogwartsHospitalWing = new Locations(
                "Hogwarts - Hospital Wing",
                ELocations.HOGWARTS_GREATHALL,
                ELocations.HOGWARTS_QUIDDITCH,
                ELocations.NONE,
                new []
                {
                    new Text("Hospital Wing"),
                    new Text("If you're ever injured this is the place to go. " +
                             "With magical cures and great doctors to help you they're here for you.")
                });
            
            var hogwartsForbiddenForest = new Locations(
                "Hogwarts - Forbidden Forest",
                ELocations.HOGWARTS_GREATHALL,
                ELocations.HOGWARTS_HAGRIDSHUT,
                ELocations.NONE,
                new []
                {
                    new Text("Forbidden Forest"),
                    new Text("ITS DANGEROUS TRAVELLING HERE! " +
                             "GET OUT NOW IF YOU WANT YOUR LIFE SPARED!")
                });
            
            myLocations.Add(ELocations.HOGWARTS_PLATFORM, hogwartsPlatform);
            myLocations.Add(ELocations.HOGWARTS_EXPRESS, hogwartsExpress);
            myLocations.Add(ELocations.HOGWARTS_STATION, hogwartsStation);
            myLocations.Add(ELocations.HOGWARTS_GATES, hogwartsGates);
            myLocations.Add(ELocations.HOGWARTS_BOATHOUSE, hogwartsBoatHouse);
            
            myLocations.Add(ELocations.HOGWARTS_GREATHALL, hogwartsGreatHall);
            myLocations.Add(ELocations.HOGWARTS_QUIDDITCH, hogwartsQuidditch);
            myLocations.Add(ELocations.HOGWARTS_HAGRIDSHUT, hogwartsHagridsHut);
            myLocations.Add(ELocations.HOGWARTS_HOSPITAL_WING, hogwartsHospitalWing);
            myLocations.Add(ELocations.HOGWARTS_FORBIDDEN_FOREST, hogwartsForbiddenForest);
            
            
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
            
            
            
            var diagonAlley = new ShopSelector("Diagon Alley");
            diagonAlley.AddItem(new OlivandersItem());
            diagonAlley.AddItem(new GringottsItem());
            myShopSelectors.Add(ELocations.DIAGON_ALLEY_SHOP, diagonAlley);
            
            #endregion
        } 

        public Text[] GetExplorationMessage()
        {
            var messages = new Text[7];
            var loc = myLocations[myCurrentLocation];

            messages[0] = new Text(loc.Name, ColorCode.CYAN);
            messages[1] = loc.QLoc != ELocations.NONE ? GetLocationMessage('Q', loc.QLoc) : null;
            messages[2] = loc.WLoc != ELocations.NONE ? GetLocationMessage('W', loc.WLoc) : null;
            messages[3] = loc.ELoc != ELocations.NONE ? GetLocationMessage('E', loc.ELoc) : null;
            messages[4] = loc.ALoc != ELocations.NONE ? GetLocationMessage('A', loc.ALoc) : null;
            messages[5] = loc.SLoc != ELocations.NONE ? GetLocationMessage('S', loc.SLoc) : null;
            messages[6] = loc.DLoc != ELocations.NONE ? GetLocationMessage('D', loc.DLoc) : null;
            return messages;
        }

        public string GetClueMessage()
        {
            var sb = new StringBuilder("");
            if (DungeonManager.Instance.Secrets.ContainsKey(myCurrentLocation))
            {
                foreach (var str in DungeonManager.Instance.Secrets[myCurrentLocation])
                {
                    sb.Append(str.GetClue() + ", ");
                }
            }

            return sb.ToString();
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

        public void SetLocation(ELocations loc)
        {
            RunAction(loc);
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
                    myShopSelectors[ELocations.DIAGON_ALLEY_SHOP].OpenInventory(true);
                    return false;
                case ELocations.LEAKY_CAULDRON_SHOP:
                    myShops[ELocations.LEAKY_CAULDRON_SHOP].OpenInventory(true);
                    return false;
                case ELocations.LEAKY_CAULDRON_APARTMENT_SHOP:
                    myShops[ELocations.LONDON_LEAKYCAULDRON_APPARTMENTS].OpenInventory(true);
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

        public int GetTravelTime()
        {
            return myLocations[myCurrentLocation].TravelTime;
        }
    }
}
