namespace JMT.RPG.Core.GameManagement
{
    public record GameLoopContext
    {
        public SavedGameState SavedGameState { get; init; }
        public IEnumerable<Campaign> Campaigns { get; init; }
        public IEnumerable<ItemShop> ItemShops { get; init; }
        public IEnumerable<CraftingRecipe> CraftingRecipes { get; init; }
        public IEnumerable<SkillCopse> SkillCopses { get; init; } 
    }
}
