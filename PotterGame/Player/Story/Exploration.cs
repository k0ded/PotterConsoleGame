using PotterGame.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Player.Story
{
    public struct Locations
    {
        public Locations(string aName, ELocations aQLoc, ELocations aWLoc, ELocations aELoc)
        {
            Name = aName;
            QLoc = aQLoc;
            WLoc = aWLoc;
            ELoc = aELoc;
        }
        public string Name { get; }
        public ELocations QLoc { get; }
        public ELocations WLoc { get; }
        public ELocations ELoc { get; }
    }

    public enum ELocations
    {
        //Privet drive
        privetDriveHall, privetDriveOutside, privetDriveSecondFloor, privetDriveKitchen, privetDriveDudleysRoom, privetDriveHarrysRoom,

        SHOP,
        NONE
    }

    class Exploration
    {
        Dictionary<ELocations, Locations> locations = new Dictionary<ELocations, Locations>();
        ELocations currentLocation = ELocations.privetDriveHall;

        public Exploration()
        {
            LoadLocations();
        }

        private void LoadLocations()
        {
            Locations privetDriveHall = new Locations(
                "4. Privet Drive - Hall",
                ELocations.privetDriveOutside,
                ELocations.privetDriveSecondFloor,
                ELocations.privetDriveKitchen);

            Locations privetDriveOutside = new Locations(
                "4. Privet Drive - Outside",
                ELocations.privetDriveHall,
                ELocations.privetDriveSecondFloor,
                ELocations.NONE);

            Locations privetDriveSecondFloor = new Locations(
                "4. Privet Drive - Second Floor",
                ELocations.privetDriveHall,
                ELocations.privetDriveDudleysRoom,
                ELocations.privetDriveHarrysRoom);

            Locations privetDriveKitchen = new Locations(
                "4. Privet Drive - Kitchen",
                ELocations.privetDriveHall,
                ELocations.SHOP,
                ELocations.NONE);

            Locations none = new Locations(" - ", ELocations.NONE, ELocations.NONE, ELocations.NONE);
            
            locations.Add(ELocations.NONE, none);
            locations.Add(ELocations.privetDriveHall, privetDriveHall);
            locations.Add(ELocations.privetDriveOutside, privetDriveOutside);
            locations.Add(ELocations.privetDriveSecondFloor, privetDriveSecondFloor);
            locations.Add(ELocations.privetDriveKitchen, privetDriveKitchen);
        }

        public Text[] GetMessage()
        {
            Text[] Messages = new Text[4];
            Locations loc = locations[currentLocation];
            Locations Qloc = locations[loc.QLoc];
            Locations Wloc = locations[loc.WLoc];
            Locations Eloc = locations[loc.ELoc];

            Messages[0] = new Text(loc.Name);
            Messages[1] = new Text("[Q] - Go -> " + Qloc.Name.Replace(Qloc.Name.Substring(0, Qloc.Name.IndexOf("-") + 2), ""));
            if (loc.WLoc == ELocations.NONE)
                return Messages;
            Messages[2] = new Text("[W] - Go -> " + Wloc.Name.Replace(Wloc.Name.Substring(0, Wloc.Name.IndexOf("-") + 2), ""));
            if (loc.ELoc == ELocations.NONE)
                return Messages;
            Messages[3] = new Text("[E] - Go -> " + Eloc.Name.Replace(Eloc.Name.Substring(0, Eloc.Name.IndexOf("-") + 2), ""));
            return Messages;
        }
    
        public Boolean RunQAction()
        {
            Locations location = locations[currentLocation];
            if (location.QLoc == ELocations.NONE)
                return false;
            currentLocation = location.QLoc;
            return true;
        }

        public Boolean RunWAction()
        {
            Locations location = locations[currentLocation];
            if (location.WLoc == ELocations.NONE)
                return false;
            currentLocation = location.WLoc;
            return true;
        }

        public Boolean RunEAction()
        {
            Locations location = locations[currentLocation];
            if (location.ELoc == ELocations.NONE)
                return false;
            currentLocation = location.ELoc;
            return true;
        }
    }
}
