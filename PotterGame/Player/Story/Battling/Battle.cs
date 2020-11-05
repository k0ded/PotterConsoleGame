using System;
using System.Text;
using System.Threading.Tasks;
using PotterGame.Inventories.Items.ShopItems.OlivandersItems.Wands;
using PotterGame.Player.Story.Battling.Enemies;
using PotterGame.Utils.Text;

namespace PotterGame.Player.Story.Battling
{
    public class Battle : BaseContext
    {

        public BaseEnemy Enemy { get; private set; }
        public bool IsBattling { get; private set; }
        public Text Controls { get; } = new Text("[Q] - Stupefy (Attack) [W] - Episkey (Heal)");
        public static int FightingConstant = 25; 
        
        private Random myRandom = new Random();

        public override void Start(BaseEnemy aEnemy)
        {
            Enemy = aEnemy;
            Program.Player.SeizeInput = true;
            IsBattling = true;
            Program.Player.SeizeInput = false;
        }
        
        
        //       -25
        //(∩｀-´)⊃━☆                    ☆━⊂(｀-´∩)
        
        //                                   -25
        //    (∩｀-´)⊃━☆                    ☆━⊂(｀-´∩)
        
        //
        //(∩｀-´)⊃━☆                        ☆━⊂(｀-´∩)
        //++++++++++                        ++++++++++
        private void RunSpell(bool aIsDamage)
        {
            Program.Player.SeizeInput = true;
            if (aIsDamage)
            {
                int damage = 0;
                if (Program.Player.PlayerWand != null)
                {
                    damage = (int) (myRandom.Next(5, FightingConstant) *
                                    FromCoreToDamageFactor(Program.Player.PlayerWand.Value.Core));
                }
                else if (!Program.Player.Damage(Program.Player.MaxHealth))
                    return;
                        
                Enemy.Damage(damage);
                RunAnimation(false, damage);
            }
            else
            {
                if (Program.Player.PlayerWand != null)
                {
                    var healAmount = (int) (myRandom.Next(5, FightingConstant) *
                                    FromCoreToDamageFactor(Program.Player.PlayerWand.Value.Core));
                    Program.Player.Heal(healAmount);
                }
                else if (!Program.Player.Damage(Program.Player.MaxHealth))
                    return;
            }

            Program.Player.SeizeInput = false;
        }

        /// <summary>
        /// This runs the damage animation of the player / enemy
        /// </summary>
        /// <param name="aIsEnemy">If the damager is enemy</param>
        /// <param name="damage">How much damage to be done</param>
        public void RunAnimation(bool aIsEnemy, int damage)
        {

            Text[] msg;
            if (aIsEnemy)
            {
                msg = new []
                {
                    new Text("      -" + damage),
                    new Text("(∩｀-´)⊃━☆                    ☆━⊂(｀-´∩)"),
                    GetHealthbar()
                };
            }
            else
            {
                msg = new []{
                    new Text("                                  -" + damage),
                    new Text("    (∩｀-´)⊃━☆                    ☆━⊂(｀-´∩)"),
                    GetHealthbar()
                };
            }
            
            TextUtils.SendMessage(msg, TextType.CENTERED);
            Task.Delay(500);
            TextUtils.SendMessage(GetBattleText(), TextType.CENTERED);
            if (!aIsEnemy)
            {
                Enemy.RunLogic();
                Task.Delay(500);
            }
            TextUtils.SendMessage(GetBattleText(), TextType.CENTERED);
            
        }
        
        /// <summary>
        /// Gets the default battle text (Nothing is happening)
        /// </summary>
        /// <returns>The default battle text</returns>
        public Text[] GetBattleText()
        {
            return new[]
            {
                new Text(" "),
                new Text("(∩｀-´)⊃━☆                        ☆━⊂(｀-´∩)"),
                GetHealthbar()
            };
        }

        /// <summary>
        /// Calculates and gets the healthbar of the player and the enemy.
        /// </summary>
        /// <returns></returns>
        private Text GetHealthbar()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Program.Player.Health / Program.Player.MaxHealth * 11; i++)
            {
                sb.Append("+");
            }
            
            var txt = new Text(sb.ToString(), ColorCode.B_RED);
            sb.Clear();
            for (var i = 0; i < 11 - Program.Player.Health / Program.Player.MaxHealth * 11; i++)
            {
                sb.Append("+");
            }
            var nText = new Text(sb.ToString());

            sb.Clear();
            for (var i = 0; i < Enemy.Health / Enemy.MaxHealth * 11; i++)
            {
                sb.Append("+");
            }
            
            var etxt = new Text(sb.ToString(), ColorCode.B_RED);
            sb.Clear();
            for (var i = 0; i < 11 - Enemy.Health / Enemy.MaxHealth * 11; i++)
            {
                sb.Append("+");
            }
            var enText = new Text(sb.ToString());
            return new Text(txt.Message + nText.Message + "                        " + enText.Message + etxt.Message);
        }

        /// <summary>
        /// Gets the damage output of a wand core
        /// </summary>
        /// <param name="core">The core of a wand</param>
        /// <returns>The damage factor</returns>
        private double FromCoreToDamageFactor(WandCores core)
        {
            switch (core)
            {
                case WandCores.THESTRAL_HAIR:
                    return 1;
                case WandCores.PHOENIX_FEATHER:
                    return 0.8;
                case WandCores.DRAGON_HEARTSTRING:
                    return 0.6;
                case WandCores.UNICORN_HAIR:
                    return 0.5;
                default:
                    return 0;
            }
        }
        
        /// <summary>
        /// Gets the healing output of a wand wood
        /// </summary>
        /// <param name="wood">The wood of the current wand</param>
        /// <returns>the healing factor</returns>
        private double FromWoodToHealFactor(WandWoods wood)
        {
            switch (wood)
            {
                case WandWoods.ELDER:
                    return 1;
                case WandWoods.ASH:
                    return 0.2;
                case WandWoods.FIR:
                    return 0.9;
                case WandWoods.YEW:
                    return 0.8;
                case WandWoods.VINE:
                    return 0.85;
                case WandWoods.HOLLY:
                    return 0.95;
                case WandWoods.CHERRY:
                    return 0.6;
                case WandWoods.WALNUT:
                    return 0.5;
                case WandWoods.WILLOW:
                    return 0.55;
                case WandWoods.HAWTHORN:
                    return 0.45;
                default:
                    return 0;
            }
        }

        public void UseEpiskey()
        {
            if (Program.Player.SeizeInput)
                return;
        }

        public void UseStupefy()
        {
            if (Program.Player.SeizeInput)
                return;
        }
        
    }
}
