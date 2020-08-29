using System;
using System.Text;

namespace PotterGame.Utils
{
    public class Text
    {
        public string OriginalMessage { get; }
        public string Message { get; }

        public Text(string aMessage, ColorCode aColor)
        {
            OriginalMessage = aMessage;
            Message = DeserializeColorCode(aColor) + aMessage + DeserializeColorCode(ColorCode.RESET);
        }
        public Text(string aMessage)
        {
            OriginalMessage = aMessage;
        }
        public Text(string aMessage, int r, int g, int b, bool reset)
        {
            OriginalMessage = aMessage;
            if (!reset)
            {
                Message = DeserializeColor(r, g, b) + aMessage;
                return;
            }
            Message = DeserializeColor(r, g, b) + aMessage + DeserializeColorCode(ColorCode.RESET);
        }
        
        /// <summary>
        /// Translates the ColorCodes constant integer into bytes then into the correct string
        /// for the color. Empty space is replaced with 0 as 0 doesnt have a value in the encoded
        /// Integer.
        /// </summary>
        /// <param name="aCode">ColorCode const int => Bytes => String</param>
        /// <returns><c>string</c> that changes the color of the chars coming after</returns>
        private static string DeserializeColorCode(ColorCode aCode)
        {
            var code = (int)aCode;
            return "\u001b" + Encoding.ASCII.GetString(BitConverter.GetBytes(code)).Replace(" ", "0");
        }

        /// <summary>
        /// Deserializes the color from R,G,B values.
        /// </summary>
        /// <param name="aRed">Red in RGB values.</param>
        /// <param name="aGreen">Green in RGB values.</param>
        /// <param name="aBlue">Blue in RGB values</param>
        /// <returns><c>string</c> that changes the color of the chars coming after</returns>
        private static string DeserializeColor(int aRed, int aGreen, int aBlue)
        {
            return $"\u001b[38;2;{aRed};{aGreen};{aBlue}m";
        }

    }
}
