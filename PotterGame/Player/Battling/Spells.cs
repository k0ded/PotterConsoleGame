using System;

namespace PotterGame.Player.Battling
{
    public struct Spell
    {
        public Spell(int aXSpeed, int aType, bool aFromPlayer)
        {
            XSpeed = aXSpeed;
            if (aType > 3 || aType < 0)
                throw new ArgumentException("Cannot choose type " + aType);
            Type = aType;
            FromPlayer = aFromPlayer;
            
        }
        public int XSpeed { get; set; }
        public int Type { get; set; }
        public bool FromPlayer { get; set; }
    }

    public class Spells
    {
        public Spell GetAsSpell(int aXSpeed, int aType, bool aFromPlayer)
        {
            return new Spell(aXSpeed, aType, aFromPlayer);
        }
    }
}