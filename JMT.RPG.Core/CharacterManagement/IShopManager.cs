using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.CharacterManagement
{
    public interface IShopManager
    {
        Task<ShopTransactionResult> PerformShopTransaction(ShopTransactionContext ctx);
    }

    public class ShopManager : IShopManager
    {
        private IShopInputHandler _inputHandler;
        public ShopManager(IShopInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public async Task<ShopTransactionResult> PerformShopTransaction(ShopTransactionContext ctx)
        {
            ShopTransactionInputContext inputCtx = new ShopTransactionInputContext()
            {
                AvailableCurrency = ctx.AvailableCurrency,
                ShopItems = ctx.ShopItems,
            };

            ShopTransactionInputResult inputResult = await _inputHandler.GetShopTransactionInput(inputCtx);
            if(inputResult.PurchasedShopItem == null)
            {
                return new ShopTransactionResult()
                {
                    RemainingCurrency = ctx.AvailableCurrency
                };
            }

            if (inputResult.PurchasedShopItem.ItemPrice > ctx.AvailableCurrency) throw new IllegalPurchaseException("You do not have enough money to purchase that item.");

            return new ShopTransactionResult()
            {
                PurchasedItem = inputResult.PurchasedShopItem,
                RemainingCurrency = (ctx.AvailableCurrency - inputResult.PurchasedShopItem.ItemPrice),
            };
        }
    }

    public interface IShopInputHandler
    {
        Task<ShopTransactionInputResult> GetShopTransactionInput(ShopTransactionInputContext ctx);
    }

    public record ShopTransactionInputResult
    {
        public ShopItem? PurchasedShopItem { get; init; }
    }

    public record ShopTransactionInputContext
    {
        public int AvailableCurrency { get; init; }
        public IEnumerable<ShopItem> ShopItems { get; init; }
    }

    public record ShopTransactionResult
    {
        public int RemainingCurrency { get; init; }
        public ShopItem PurchasedItem { get; init; }
    }

    public record ShopTransactionContext
    {
        public int AvailableCurrency { get; init; }
        public IEnumerable<ShopItem> ShopItems { get; init; }
    }

    public record ShopItem
    {
        public string ItemID { get; init; }
        public string ItemName { get; init; }
        public string ItemDescription { get; init; }
        public int ItemPrice { get; init; }
    }
}
