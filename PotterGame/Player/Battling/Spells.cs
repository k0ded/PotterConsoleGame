using System;

namespace PotterGame.Player.Battling
{
    public struct Spell
    {
        public Spell(int aXSpeed, int aYSpeed, int aType, bool aFromPlayer)
        {
            xSpeed = aXSpeed;
            ySpeed = aYSpeed;
            if (aType > 3 || aType < 0)
                throw new ArgumentException("Cannot choose type " + aType);
            type = aType;
            fromPlayer = aFromPlayer;
            
        }
        int xSpeed { get; set; }
        int ySpeed { get; set; }
        private int type { get; set; }
        bool fromPlayer { get; set; }
    }

    public class Spells
    {
        public Spell GetAsSpell(int aXSpeed, int aYSpeed, int aType, bool aFromPlayer)
        {
            return new Spell();
        }
    }
}