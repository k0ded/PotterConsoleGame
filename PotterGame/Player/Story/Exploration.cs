using PotterGame.Utils;
using System;
using System.Collections.Generic;

namespace PotterGame.Player.Story
{
    public readonly struct Locations
    {
        public Locations(string aName, ELocations aQLoc, ELocations aWLoc, ELocations aELoc, Text[] aExplanation)
        {
            Name = aName;
            QLoc = aQLoc;
            WLoc = aWLoc;
            ELoc = aELoc;
            Explanation = aExplanation;
        }
        public string Name { get; }
        public ELocations QLoc { get; }
        public ELocations WLoc { get; }
        public ELocations ELoc { get; }
        public Text[] Explanation { get; }
    }

    public enum ELocations
    {
        //Privet drive
        PRIVET_DRIVE_HALL, PRIVET_DRIVE_OUTSIDE, PRIVET_DRIVE_SECOND_FLOOR, PRIVET_DRIVE_KITCHEN, PRIVET_DRIVE_DUDLEYS_ROOM, PRIVET_DRIVE_HARRYS_ROOM,

        SHOP,
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
            var privetDriveHall = new Locations(
                "4. Privet Drive - Hall",
                ELocations.PRIVET_DRIVE_OUTSIDE,
                ELocations.PRIVET_DRIVE_SECOND_FLOOR,
                ELocations.PRIVET_DRIVE_KITCHEN,
                new []
                {
                    new Text("Privet Drive"), 
                    new Text("You are standing in the hallway."),
                    new Text("Next to you there's a staircase with a small cupboard underneath it."), 
                    new Text("At the end of the hallway theres a small see-through door"),
                    new Text("leading into the kitchen") 
                });

            var privetDriveOutside = new Locations(
                "4. Privet Drive - Outside",
                ELocations.PRIVET_DRIVE_HALL,
                ELocations.PRIVET_DRIVE_SECOND_FLOOR,
                ELocations.NONE,
                new []
                {
                    new Text("Privet Drive"), 
                    new Text("You are standing in the front yard."),
                    new Text("The house is made of bricks and it has a small garage on the right"),
                    new Text("hand side. The grass in front of the house is tidy and emerald green."),
                    new Text(" "),
                    new Text("Down the street you see a large purple bus with a sign on the front"),
                    new Text("that says: \"Knight Bus\"."), 
                });

            var privetDriveSecondFloor = new Locations(
                "4. Privet Drive - Second Floor",
                ELocations.PRIVET_DRIVE_HALL,
                ELocations.PRIVET_DRIVE_DUDLEYS_ROOM,
                ELocations.PRIVET_DRIVE_HARRYS_ROOM,
                new []
                {
                    new Text(""), 
                    new Text(""), 
                });

            var privetDriveKitchen = new Locations(
                "4. Privet Drive - Kitchen",
                ELocations.PRIVET_DRIVE_HALL,
                ELocations.SHOP,
                ELocations.NONE,
                new []
                {
                    new Text(""), 
                    new Text(""), 
                });

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
            myLocations.Add(ELocations.PRIVET_DRIVE_HALL, privetDriveHall);
            myLocations.Add(ELocations.PRIVET_DRIVE_OUTSIDE, privetDriveOutside);
            myLocations.Add(ELocations.PRIVET_DRIVE_SECOND_FLOOR, privetDriveSecondFloor);
            myLocations.Add(ELocations.PRIVET_DRIVE_KITCHEN, privetDriveKitchen);
        }

        public Text[] GetExplorationMessage()
        {
            var messages = new Text[4];
            var loc = myLocations[myCurrentLocation];
            var qloc = myLocations[loc.QLoc];

            messages[0] = new Text(loc.Name);
            messages[1] = new Text(
                "[Q] - Go -> " + qloc.Name.Replace(qloc.Name.Substring(0, qloc.Name.IndexOf("-", StringComparison.OrdinalIgnoreCase) + 2), ""));
            if (loc.WLoc == ELocations.NONE)
                return messages;
            var wloc = myLocations[loc.WLoc];
            messages[2] = new Text("[W] - Go -> " + wloc.Name.Replace(wloc.Name.Substring(0, wloc.Name.IndexOf("-", StringComparison.OrdinalIgnoreCase) + 2), ""));
            if (loc.ELoc == ELocations.NONE)
                return messages;
            var eloc = myLocations[loc.ELoc];
            messages[3] = new Text("[E] - Go -> " + eloc.Name.Replace(eloc.Name.Substring(0, eloc.Name.IndexOf("-", StringComparison.OrdinalIgnoreCase) + 2), ""));
            return messages;
        }

        public Text[] GetExplanationMessage()
        {
            return myLocations[myCurrentLocation].Explanation;
        }
    
        public bool RunQAction()
        {
            if (Program.GetPlayer().SeizeInput) return false;
            var location = myLocations[myCurrentLocation];
            if (location.QLoc == ELocations.NONE)
                return false;
            myCurrentLocation = location.QLoc;
            return true;
        }

        public bool RunWAction()
        {
            if (Program.GetPlayer().SeizeInput) return false;
            var location = myLocations[myCurrentLocation];
            if (location.WLoc == ELocations.NONE)
                return false;
            myCurrentLocation = location.WLoc;
            return true;
        }

        public bool RunEAction()
        {
            if (Program.GetPlayer().SeizeInput) return false;
            var location = myLocations[myCurrentLocation];
            if (location.ELoc == ELocations.NONE)
                return false;
            myCurrentLocation = location.ELoc;
            return true;
        }
    }
}
