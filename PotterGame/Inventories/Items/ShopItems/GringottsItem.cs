using System;
using PotterGame.Inventories.InventoryTypes;
using PotterGame.Inventories.Items.BankItems;

namespace PotterGame.Inventories.Items.ShopItems
{
    public class GringottsItem : BaseShopItem
    {
        public GringottsItem()
        {
            myShop = new Shop("Gringotts Bank");
            myShop.AddItem(new WithdrawItem());
            myShop.AddItem(new DepositItem());
        }
    }
}