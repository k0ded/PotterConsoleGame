using System.Collections.Generic;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items;
using PotterGame.Player.Story;

namespace PotterGame.Inventories
{
    public static class InventoryManager
    {
        
        public static bool IsInventoryOpen { get; set; }
        public static BaseInventory OpenInventory { get; set; }
        public static BaseInventory PlayerInventory { get; set; }

    }
}