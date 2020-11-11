using System;
using System.Text;

namespace PotterGame.Utils.Dungeons
{
    public class SecretPassword : ISecret
    {
        public string Password { get; }
        
        public SecretPassword(int cluesNeeded, Random r)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < cluesNeeded * 2; i++)
            {
                var c = Convert.ToChar(r.Next(1, 26) + 65);
                builder.Append(c);
            }

            var p = builder.ToString();
            Password = DungeonManager.Cipher.Encipher(p.ToUpper()).ToUpper();
        }

        public string GetClue()
        {
            return "";
        }
    }
}