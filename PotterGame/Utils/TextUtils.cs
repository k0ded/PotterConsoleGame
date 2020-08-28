using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotterGame.Utils
{
    public class TextUtils
    {

        public void SendCenteredMessage(Text[] aMessage)
        {
            for (var i = 0;i < aMessage.Length; i++)
            {
                int x = Console.WindowWidth / 2 - aMessage[i].OriginalMessage.Length / 2;
                int y = Console.WindowHeight / 2 - aMessage.Length / 2 + i;
                Console.SetCursorPosition(x, y);
                Console.WriteLine(aMessage[i].Message);
            }

        }

        public void SendInventoryMessage(Text[] aMessage)
        {
            for (var i = 0; i < aMessage.Length; i++)
            {
                int x = 2;
                int y = Math.Min(2 + i, Console.WindowHeight - 2);
                Console.SetCursorPosition(x, y);
                Console.WriteLine(aMessage[i].Message);
            }

        }

        public void SendControlsMessage(Text aControls)
        {
            int x = Console.WindowWidth / 2 - aControls.OriginalMessage.Length / 2;
            int y = Console.WindowHeight;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(aControls.Message);
        }

    }
}
