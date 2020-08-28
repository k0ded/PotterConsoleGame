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
            Message = aMessage;
        }

        public Text(string aMessage, int r, int b, int g, Boolean reset)
        {
            OriginalMessage = aMessage;
            if (!reset)
            {
                Message = DeserializeColor(r, b, g) + aMessage;
                return;
            }
            Message = DeserializeColor(r, b, g) + aMessage + DeserializeColorCode(ColorCode.RESET);
        }


        // ColorCode int => Bytes => String
        private String DeserializeColorCode(ColorCode aCode)
        {
            int code = (int)aCode;
            return "\u001b" + Encoding.ASCII.GetString(BitConverter.GetBytes(code)).Replace(" ", "0");
        }

        private String DeserializeColor(int r, int b, int g)
        {
            return $"\u001b[38;2;{r};{b};{g}m";
        }

    }
}
