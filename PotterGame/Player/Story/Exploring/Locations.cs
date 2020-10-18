using PotterGame.Utils.Text;

namespace PotterGame.Player.Story.Exploring
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
            Text[] aExplanation,
            int aDanger = 1,
            bool aFakeDanger = false)
        {
            Name = aName;
            QLoc = aQLoc;
            WLoc = aWLoc;
            ELoc = aELoc;
            ALoc = aALoc;
            SLoc = aSLoc;
            DLoc = aDLoc;
            Explanation = aExplanation;
            Danger = aDanger;
            IsFakeDanger = aFakeDanger;
        }
        public Locations(string aName,
            ELocations aQLoc,
            ELocations aWLoc,
            ELocations aELoc,
            Text[] aExplanation,
            int aDanger = 1,
            bool aFakeDanger = false)
        {
            Name = aName;
            QLoc = aQLoc;
            WLoc = aWLoc;
            ELoc = aELoc;
            ALoc = ELocations.NONE;
            SLoc = ELocations.NONE;
            DLoc = ELocations.NONE;
            Explanation = aExplanation;
            Danger = aDanger;
            IsFakeDanger = aFakeDanger;
        }
        public string Name { get; }
        public ELocations QLoc { get; }
        public ELocations WLoc { get; }
        public ELocations ELoc { get; }
        public ELocations ALoc { get; }
        public ELocations SLoc { get; }
        public ELocations DLoc { get; }
        public Text[] Explanation { get; }
        public int Danger { get; }
        public bool IsFakeDanger { get; }
    }
}