namespace PotterGame.Utils
{
    // ANSI ColorCode string (30m/31m etc.) => Bytes => Integer 
    public enum ColorCode {
        // Text color
        BLACK = 1831875419,
        RED = 1831940955,
        GREEN = 1832006491,
        YELLOW = 1832072027,
        BLUE = 1832137563,
        MAGENTA = 1832203099,
        CYAN = 1832268635,
        WHITE = 1832334171,
        
        RESET = 7155803,
        
        // Background color
        B_BLACK = 1831875675,
        B_RED = 1831941211,
        B_GREEN = 1832006747,
        B_YELLOW = 1832072283,
        B_BLUE = 1832137819,
        B_MAGENTA = 1832203355,
        B_CYAN = 1832268891,
        B_WHITE = 1832334427
    }
}
