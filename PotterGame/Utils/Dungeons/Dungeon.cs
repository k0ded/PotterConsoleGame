namespace PotterGame.Utils.Dungeons
{
    public class Dungeon : ISecret
    {
        public const int CluesNeeded = 7;
        public SecretPassword Password;
        public string Name { get; }
        private Clue[] clues;
        private int cluesFound;

        public Dungeon(string aName, Clue[] aClues, SecretPassword aPassword)
        {
            Name = aName;
            clues = aClues;
            Password = aPassword;
        }
        
        public bool TryPassword(string str)
        {
            // Lösenordet är redan encipherad så vi måste kolla om den decipherade verisionen är rätt
            return str.ToUpper() == DungeonManager.Cipher.Decipher(Password.Password);
        }
        
        /// <summary>
        /// Här kan du få clues från 
        /// </summary>
        /// <returns>a clue in the form of a string</returns>
        public string GetClue()
        {
            cluesFound++;
            if (cluesFound > clues.Length)
                return "";
            return clues[cluesFound-1].GetClue();
        }
    }
}