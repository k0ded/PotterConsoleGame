using System;
using System.Text;

namespace PotterGame.Utils.Text
{
    public class Text
    {
        public string OriginalMessage { get; }
        public string Message { get; }

        /// <summary>
        /// The simplest way to get a color on your text using <c>ColorCode</c>.
        /// </summary>
        /// <param name="aMessage">
        /// The message you'd like to display to the screen.
        /// </param>
        /// <param name="aColor">
        /// The <c>ColorCode</c> you'd like to use for the text.
        /// </param>
        public Text(string aMessage, ColorCode aColor)
        {
            OriginalMessage = aMessage;
            Message = DeserializeColorCode(aColor) + aMessage + DeserializeColorCode(ColorCode.RESET);
        }

        /// <summary>
        /// The simplest form of a <c>Text</c> message. Its plain white.
        /// </summary>
        /// <param name="aMessage">
        /// The message you'd like to display to the screen.
        /// </param>
        public Text(string aMessage)
        {
            OriginalMessage = aMessage;
            Message = DeserializeColorCode(ColorCode.RESET) + aMessage;
        }
        
        /// <summary>
        /// Sets the <c>Text</c> color to an rgb value and decides
        /// if it should <paramref name="reset"/> the color to white or not
        /// </summary>
        /// <param name="aMessage">
        /// The <c>string</c> message you want to send
        /// </param>
        /// <param name="r">Red in RGB values</param>
        /// <param name="g">Green in RGB values</param>
        /// <param name="b">Blue in RGB values</param>
        /// <param name="reset">If the message should return to white after sending</param>
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

        #region Operators

        public static Text operator +(Text t, ColorCode code)
        {
            return new Text(t.OriginalMessage, code);
        }
        
        public static Text operator +(Text t, Text t1)
        {
            return new Text(t.Message + t1.OriginalMessage);
        }
        
        public static Text operator +(Text t, string s)
        {
            return new Text(t.Message + s);
        }

        #endregion

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

        public Text Replace(string replace, string replacewith)
        {
            return new Text(Message.Replace(replace,replacewith));
        }
    }
}
