using System;
using System.Text;

namespace PotterGame.Utils
{
    // ColorCode string (30m/31m etc.) => Bytes => Integer 
    public enum ColorCode {
        BLACK = 1831875419,
        RED = 1831940955,
        GREEN = 1832006491,
        YELLOW = 1832072027,
        BLUE = 1832137563,
        MAGENTA = 1832203099,
        CYAN = 1832268635,
        WHITE = 1832334171,
        RESET = 7155803
    }

    public class Text
    {
        public string OriginalMessage { get; }
        public string Message { get; }

        public Text(string aMessage, ColorCode aColor, Boolean aBrightColor)
        {
            OriginalMessage = aMessage;
            Message = DeserializeColorCode(aColor, aBrightColor) + aMessage;
        }

        public Text(string aMessage)
        {
            OriginalMessage = aMessage;
            Message = aMessage;
        }

        
        // ColorCode int => Bytes => String
        private String DeserializeColorCode(ColorCode aCode, bool aBrightColor)
        {
            int code = (int)aCode;
            if (aBrightColor)
                return DeserializeColorCode(aCode, false) + ";1m";

            return "\u001b" + Encoding.ASCII.GetString(BitConverter.GetBytes(code)).Replace(" ", "0");
        }

    }
}
