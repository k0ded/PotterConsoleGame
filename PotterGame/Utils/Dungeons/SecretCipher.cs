using System;
using System.Linq;
using PotterGame.Player.Story.Exploring;

namespace PotterGame.Utils.Dungeons
{
    public class SecretCipher : ISecret
    {
        private readonly Random r;
        private readonly int key;

        public ELocations SecretLocation;
        
        public SecretCipher(Random aRandom)
        {
            r = aRandom;
            key = r.Next(1, 20);
        }
        
        /// <summary>
        /// Den här metoden flyttar karaktären ch mellan a-z fram k steg. Går den över Z så går den tillbaka till A.
        /// </summary>
        private char Cipher(char ch, int k) {  
            if (!char.IsLetter(ch)) {
                return ch;  
            }  
  
            var offset = char.IsUpper(ch) ? 'A' : 'a';  
            return (char)((ch + k - offset) % 26 + offset);
        }

        public string Encipher(string input)
        {
            // Denna hämtar alla karaktärer från input strängen och kör dom igenom cipher metoden och
            // sedan lägger på dom på en tom sträng en efter en.
            return input.Aggregate(string.Empty, (current, ch) => current + Cipher(ch, key));
        }  
  
        public string Decipher(string input) {
            // Denna hämtar alla karaktärer från input strängen och kör dom igenom cipher metoden och
            // sedan lägger på dom på en tom sträng en efter en.
            return input.Aggregate(string.Empty, (current, ch) => current + Cipher(ch, 26 - key));
        }  
        
        /// <summary>
        /// Den här metoden är en metod från ISecret som är till för att man ska kunna få ledtrådar till vad de olika sakerna betyder.
        /// </summary>
        /// <returns>Ledtråden</returns>
        public string GetClue()
        {
            return "C_Cipher. -" + key;
        }
    }
}