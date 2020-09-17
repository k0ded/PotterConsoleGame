using System;
using System.Linq;
using System.Text;
using PotterGame.Utils;

namespace PotterGame.Player.Battling
{
    public partial class Battle
    {
        //
        // (∩｀-´)⊃━ﾟ.*･｡ﾟ☆                                       ☆ﾟ｡･*.ﾟ━⊂(´-'∩)
        //⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺
            
        private Text[] rendered = new Text[3];

        const string player = "(∩｀-´)⊃━";
        const string enemy = "━⊂(´-'∩)";

        const string magicStupey = "ﾟ.*･｡ﾟ☆";
        const string magicPetrificus = "*--*--☆";
        
        /// <summary>
        /// Renders the information provided by <c>Battle</c> to the screen
        /// </summary>
        private void Render()
        {
            rendered[0] = new Text("");
            ProcessSpell();
            rendered[2] = new Text("⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺⎺");
            ProcessHealthbar();
            
            // Renders the processed information to the screen
            RenderControls();
            TextUtils.SendMessage(rendered, TextType.CENTERED);
        }

        /// <summary>
        /// Processes the spells to get it ready for rendering.
        /// </summary>
        private void ProcessSpell()
        {
            var sb = new StringBuilder("                                                     ");
            
            // Enemy spell rendering
            for (var i = 0; i < myEnemySpell.Length; i++)
            {
                sb[i] = ' ';
                switch (myEnemySpell[i].Type)
                {
                    case 0:
                        continue;
                    
                    // Stupefy (Attack)
                    case 1:
                    {
                        for (var j = i; j > Math.Max(i - 7, 0); j--)
                        {
                            sb[j] = magicStupey.ToCharArray()[j];
                        }

                        break;
                    }
                    
                    // Protego (Defend)
                    case 2:
                        sb[i] = '{';
                        break;
                    
                    // Petrificus (Stun)
                    case 3:
                    {
                        for (var j = i; j > Math.Max(i - 7, 0); j--)
                        {
                            sb[j] = magicPetrificus.ToCharArray()[j];
                        }

                        break;
                    }
                }
            }
            // The enemy spell is going the opposite direction and will therefor be flipped to match that
            var s = sb.ToString().Reverse();
            sb = new StringBuilder(s.ToString());
            
            // Make sure the spell from the Player gets sent last so its viewed above everything else.
            for (var i = 0; i < myPlayerSpell.Length; i++)
            {
                sb[i] = ' ';
                switch (myPlayerSpell[i].Type)
                {
                    case 0:
                        continue;
                    // Stupefy (Attack)
                    case 1:
                    {
                        for (var j = i; j > Math.Max(i - 7, 0); j--)
                        {
                            sb[j] = magicStupey.ToCharArray()[j];
                        }
                        break;
                    }
                    
                    // Protego (Defend)
                    case 2:
                        sb[i] = '}';
                        break;
                    
                    // Petrificus (Stun)
                    case 3:
                        for (var j = i; j > Math.Max(i - 7, 0); j--)
                        {
                            sb[j] = magicPetrificus.ToCharArray()[j];
                        }
                        break;
                }
            }
            
            rendered[1] = new Text(player + sb + enemy);
        }

        /// <summary>
        /// Processes the Healthbar to get it ready for rendering.
        /// </summary>
        private void ProcessHealthbar()
        {
            // Player Health bar
            var playerHealthAmount = myVirtualPlayerHealth / Program.Player.MaxHealth * 35;
            var playerHealthString = "";
            var playerNotHealthStr = "";
            
            // Adds the right amount of whitespaces to display the amount of health the enemy has.
            while (playerHealthString.Length < playerHealthAmount)
            {
                playerHealthString += " ";
            }
            
            // Adds the right amount of white spaces to make everything centered
            while (playerNotHealthStr.Length < 35 - playerHealthAmount)
            {
                playerNotHealthStr += " ";
            }
            
            // Player health bar is done
            var playerHealth = new Text(new Text(playerHealthString, ColorCode.B_RED).Message + new Text(playerNotHealthStr, ColorCode.RESET).Message);
            
            
            // Enemy Health bar
            var enemyHealthAmount = myVirtualEnemyHealth / Enemy.MaxHealth * 35;
            var enemyHealthString = "";
            var enemyNotHealthStr = "";
            
            // Adds the right amount of whitespaces to display the amount of health the enemy has
            while (enemyHealthString.Length < enemyHealthAmount)
            {
                enemyHealthString += " ";
            }
            
            // Adds the rest of the whitespaces to put the health bar in the right position
            while (enemyNotHealthStr.Length < 35 - enemyHealthAmount)
            {
                enemyNotHealthStr += " ";
            }
            var enemyHealth = new Text(new Text(enemyNotHealthStr, ColorCode.RESET).Message + new Text(enemyHealthString, ColorCode.B_RED).Message);
            
            // Adds the health bars to the rendered Text Array.
            rendered[3] = new Text($"{playerHealth.Message}|{enemyHealth.Message}");
        }

        /// <summary>
        /// Renders the processed controls to the screen
        /// </summary>
        private void RenderControls()
        {
            // Last thing that's needed is the controls bar that tells which spells are on cooldown and which arent
            var controls = "[Q] - Stupefy  [W] - Protego  [E] - Petrificus";
            if (DateTime.Now.Second < myStupefyCooldown)
            {
                controls = controls.Replace("Q", Convert.ToString(myStupefyCooldown - DateTime.Now.Second));
            }
            
            if (DateTime.Now.Second < myProtegoCooldown)
            {
                controls = controls.Replace("W", Convert.ToString(myProtegoCooldown - DateTime.Now.Second));
            }
            
            if (DateTime.Now.Second < myPetrificusCooldown)
            {
                controls = controls.Replace("E", Convert.ToString(myPetrificusCooldown - DateTime.Now.Second));
            }
            
            // Renders all of the information to the screen.
            TextUtils.SendMessage(new Text(controls), TextType.CONTROLS);
        }
    }
}