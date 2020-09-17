using PotterGame.Utils;
using System;
using System.Collections.Generic;

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
        private ELocations myCurrentLocation = ELocations.PRIVET_DRIVE_HALL;

        public Exploration()
        {
            LoadLocations();
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
                ELocations.PRIVET_DRIVE_SECOND_FLOOR,
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
                ELocations.LONDON_DIAGONALLEY,
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
            
            #endregion
            
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
        }

        public Text[] GetExplorationMessage()
        {
            var messages = new Text[4];
            var loc = myLocations[myCurrentLocation];
            var qloc = myLocations[loc.QLoc];

            messages[0] = new Text(loc.Name);
            messages[1] = GetLocationMessage("[Q] - Go -> ", loc.QLoc);
            if (loc.WLoc == ELocations.NONE)
                return messages;
            messages[2] = GetLocationMessage("[W] - Go -> ", loc.WLoc);
            if (loc.ELoc == ELocations.NONE)
                return messages;
            messages[3] = GetLocationMessage("[E] - Go -> ", loc.ELoc);
            return messages;
        }

        private Text GetLocationMessage(string prefix, ELocations aLocation)
        {
            switch (aLocation)
            {
                case(ELocations.DIAGON_ALLEY_SHOP):
                    return new Text(prefix + "Shop Selector");
                
                case(ELocations.LEAKY_CAULDRON_SHOP):
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
    
        public bool RunQAction()
        { ;
            var location = myLocations[myCurrentLocation];
            if (location.QLoc == ELocations.NONE)
                return false;
            myCurrentLocation = location.QLoc;
            return true;
        }

        public bool RunWAction()
        {
            var location = myLocations[myCurrentLocation];
            if (location.WLoc == ELocations.NONE)
                return false;
            myCurrentLocation = location.WLoc;
            return true;
        }

        public bool RunEAction()
        {
            var location = myLocations[myCurrentLocation];
            if (location.ELoc == ELocations.NONE)
                return false;
            myCurrentLocation = location.ELoc;
            return true;
        }
    }
}
