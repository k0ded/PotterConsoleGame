using System;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items.BankItems;

namespace PotterGame.Inventories.Items.ShopItems
{
    public class GringottsItem : BaseShopItem
    {
        public GringottsItem() : base("Gringotts Bank")
        {
            Shop = new Shop("Gringotts Bank");
            Shop.AddItem(new WithdrawItem());
            Shop.AddItem(new DepositItem());
        }
    }
}