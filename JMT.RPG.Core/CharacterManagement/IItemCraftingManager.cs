using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.CharacterManagement
{
    public class ItemCraftingManager : IItemCraftingManager
    {
        private IItemCraftingInputHandler _inputHandler;
        public ItemCraftingManager(IItemCraftingInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public async Task<ItemCraftingResult> CraftItem(ItemCraftingContext ctx)
        {
            // mark all recipes craftable or not to inform input
            ItemCraftingRecipe[] markedRecipes = ctx.ItemCraftingRecipes.Select(icr => MarkCraftable(icr, ctx.CraftingItems)).ToArray();

            // ask the user what they want to craft
            ItemCraftingInputContext inputCtx = new ItemCraftingInputContext()
            {
                CraftingItems = ctx.CraftingItems,
                ItemCraftingRecipes = markedRecipes,
            };

            ItemCraftingInputResult result = await _inputHandler.GetItemToCraft(inputCtx);
            if(result.SelectedRecipe == null)
            {
                return new ItemCraftingResult()
                {
                    RemainingIngredients = ctx.CraftingItems,
                };
            }

            if (!(result.SelectedRecipe.CanCraft ?? false)) throw new NotCraftableException("Cannot craft the selected recipe, you do not possess the required ingredients.");

            CraftingItem[] remainingCraftingItems = RemoveRecipeIngredients(result.SelectedRecipe, ctx.CraftingItems).ToArray();

            return new ItemCraftingResult()
            {
                CraftedItems = BuildCraftingResult(result.SelectedRecipe).ToArray(),
                RemainingIngredients = remainingCraftingItems,
            };
        }

        private ItemCraftingRecipe MarkCraftable(ItemCraftingRecipe itemCraftingRecipe, IEnumerable<CraftingItem> craftingItems)
        {
            bool canCraft = itemCraftingRecipe.Ingredients.All(ci => craftingItems.Count(i => i.CraftingItemID == ci.CraftingIngredientID) > ci.NumRequired);
            return itemCraftingRecipe with { CanCraft = canCraft };
        }

        private IEnumerable<CraftingItem> RemoveRecipeIngredients(ItemCraftingRecipe craftedRecipe, IEnumerable<CraftingItem> craftingItems)
        {
            List<CraftingItem> materializedCraftingItems = new List<CraftingItem>(craftingItems.ToArray());
            foreach(ItemCraftingIngredient ingredient in craftedRecipe.Ingredients)
            {
                CraftingItem[] removedItems = materializedCraftingItems.Where(ci => ci.CraftingItemID == ingredient.CraftingIngredientID).Take(ingredient.NumRequired).ToArray();
                materializedCraftingItems = materializedCraftingItems.Except(removedItems).ToList();
            }

            return materializedCraftingItems;
        }

        private IEnumerable<CraftingItem> BuildCraftingResult(ItemCraftingRecipe selectedRecipe)
        {
            for(int i = 0; i < selectedRecipe.NumCrafted; i++)
            {
                yield return selectedRecipe.CraftedItem with { };
            }
        }
    }
    
    public class NotCraftableException : Exception
    {
        public NotCraftableException(string? message) : base(message)
        {
        }
    }

    public interface IItemCraftingInputHandler
    {
        Task<ItemCraftingInputResult> GetItemToCraft(ItemCraftingInputContext ctx);
    }

    public record ItemCraftingInputResult
    {
        public ItemCraftingRecipe SelectedRecipe { get; init; }
    }

    public record ItemCraftingInputContext
    {
        public IEnumerable<CraftingItem> CraftingItems { get; init; }
        public IEnumerable<ItemCraftingRecipe> ItemCraftingRecipes { get; init; }
    }

    public interface IItemCraftingManager
    {
        Task<ItemCraftingResult> CraftItem(ItemCraftingContext ctx);
    }

    public record ItemCraftingResult
    {
        public IEnumerable<CraftingItem> CraftedItems { get; init; }
        public IEnumerable<CraftingItem> RemainingIngredients { get; init; }
    }

    public record ItemCraftingContext
    {
        public IEnumerable<CraftingItem> CraftingItems { get; init; } 
        public IEnumerable<ItemCraftingRecipe> ItemCraftingRecipes { get; init; } // all available recipes
    }

    public record CraftingItem
    {
        public string CraftingItemID { get; init; }
        public string CraftingItemName { get; init; }
        public string CraftingItemDescription { get; init; }
    }

    public record ItemCraftingRecipe
    {
        public bool? CanCraft { get; init; }
        public IEnumerable<ItemCraftingIngredient> Ingredients { get; init; }
        public CraftingItem CraftedItem { get; init; }
        public int NumCrafted { get; init; }
    }

    public record ItemCraftingIngredient
    {
        public string CraftingIngredientID { get; init; }
        public string CraftingIngredientName { get; init; }
        public string CraftingIngredientDescription { get; init; }
        public int NumRequired { get; init; }
    }
}
