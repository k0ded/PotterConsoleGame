using System;
using System.Collections.Generic;
using System.Linq;
using PotterGame.Inventories.Items.ShopItems.GringottsItems;
using PotterGame.Player.Story.Exploring;

namespace PotterGame.Utils.Dungeons
{
    public class DungeonManager
    {
        private static List<string> dungeonNames = new List<string>{
            Constants.tDungeon,
            Constants.gDungeon
        };
        private Random myRandom = new Random();
        
        public static int OpenedDungeons { get; set; }
        public static DungeonManager Instance;
        public Dungeon[] Dungeons = new Dungeon[dungeonNames.Count];

        public Dictionary<ELocations, List<ISecret>> Secrets = new Dictionary<ELocations, List<ISecret>>();
        public static SecretCipher Cipher;

        
        public DungeonManager()
        {
            Cipher = new SecretCipher(myRandom);
            RollDungeons();
        }

        public void RollDungeons()
        {
            Instance = this;
            var minIndex = (int) ELocations.LONDON_KNIGHTBUS;
            var maxIndex = (int) ELocations.NONE;

            for (var i = 0; i < dungeonNames.Count; i++)
            {
                var aName = dungeonNames[i];
                var clues = new Clue[Dungeon.CluesNeeded];
                var password = new SecretPassword(Dungeon.CluesNeeded, myRandom);

                for (var c = 0; c < Dungeon.CluesNeeded; c++)
                {
                    var location = (ELocations) myRandom.Next(minIndex, maxIndex);
                    clues[c] = new Clue(aName[0], password, c);
                    if (!Secrets.ContainsKey(location))
                    {
                        Secrets.Add(location, new List<ISecret> {clues[c]});
                    }
                    else
                    {
                        Secrets[location].Add(clues[c]);
                    }
                }

                Dungeons[i] = new Dungeon(aName, clues, password);
            }

            var l = (ELocations) myRandom.Next(minIndex, maxIndex);
            if (!Secrets.ContainsKey(l))
            {
                Secrets.Add(l, new List<ISecret> {Cipher});
            }
            else
            {
                Secrets[l].Add(Cipher);
            }
        }

        public static DungeonItem GetDungeonItem(string constant)
        {
            var dungeon = Instance.Dungeons[dungeonNames.IndexOf(constant)];
            var ditem = new DungeonItem(dungeon);
            return ditem;
        }
    }
}