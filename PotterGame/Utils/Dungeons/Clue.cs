namespace PotterGame.Utils.Dungeons
{
    public class Clue : ISecret
    {
        private string myClue;
           
        public Clue(char prefix, SecretPassword aPassword, int aClueNumber)
        {
            var start = aClueNumber * 2;
            var str = $"{aClueNumber}{prefix}. {aPassword.Password[start]}{aPassword.Password[start + 1]}";
            myClue = str;
        }
        
        /// <summary>
        /// Ger tillbaka en sträng som är en ledtråd till vad lösenordet kommer bli
        /// Strängen är formaterad såhär (Position)(Dungeon prefix). XX
        /// </summary>
        public string GetClue()
        {
            return myClue;
        }
    }
}