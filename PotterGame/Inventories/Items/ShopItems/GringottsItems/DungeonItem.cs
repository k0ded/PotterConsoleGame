using System;
using System.Threading;
using System.Threading.Tasks;
using PotterGame.Player.Story.Exploring;
using PotterGame.Utils.Dungeons;
using PotterGame.Utils.Text;

namespace PotterGame.Inventories.Items.ShopItems.GringottsItems
{
    public class DungeonItem : BaseItem
    {
        private Dungeon myDungeon;
        private bool alreadyOpened;
        public DungeonItem(Dungeon aDungeon) : base(aDungeon.Name, 0)
        {
            myDungeon = aDungeon;
        }

        /// <summary>
        /// När man 
        /// </summary>
        public override void InteractEvent()
        {
            Text[] query = {
                new Text($"You are in {myDungeon.Name}"),
                new Text("=================================="),
                new Text("Enter password:"),
            };
            
            while (true)
            {
                Console.Clear();
                var result = SendQuery(query);
                if (myDungeon.TryPassword(result))
                {
                    // Lösenordet var rätt
                    if (!alreadyOpened)
                        DungeonManager.OpenedDungeons++;
                    else
                        break;
                    var length = DungeonManager.Instance.Dungeons.Length;
                    if (DungeonManager.OpenedDungeons >= length)
                    {
                        Program.SendWinMessage();
                    }
                    else
                    {
                        Program.Player.AddMoney(100);
                        for (var i = 0; i < 8; i++)
                        {
                            var o = i % 2;

                            if (o == 0)
                            {
                                Text[] colored = {
                                    new Text($"You are in {myDungeon.Name}", ColorCode.CYAN),
                                    new Text("==================================", ColorCode.CYAN),
                                    new Text("Enter password:", ColorCode.CYAN),
                                };
                                TextUtils.SendMessage(colored, TextType.CENTERED);
                            }
                            else
                            {
                                TextUtils.SendMessage(query, TextType.CENTERED);
                            }
                            Task.Delay(100).Wait();
                        }
                    }

                    alreadyOpened = true;
                    break;
                }

                if (result.ToLower() == "back")
                {
                    // Backa!
                    break;
                }

                // Lösenordet var fel
                TextUtils.SendMessage(new Text("Wrong password!"), TextType.ACTION);
            }
            
            InventoryManager.OpenInventory.RunReloadAction();
        }

        private string SendQuery(Text[] query)
        {
            TextUtils.SendMessage(new []
            {
                new Text("Try to figure out the password with the clues lying around!"),
                new Text("Write \"back\" to go back to the inventory")
            }, TextType.HEADERBAR);
            TextUtils.SendMessage(query, TextType.CENTERED);
            return Console.ReadLine();
        }

        public override void ReturnEvent() {}
    }
}