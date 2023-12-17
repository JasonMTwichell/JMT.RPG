using JMT.RPG.Core.CharacterManagement;

namespace JMT.RPG.CharacterManagement
{
    public class CharacterLevelUpManager : ICharacterLevelManager
    {
        private ICharacterLevelUpInputHandler _inputHandler;
        public CharacterLevelUpManager(ICharacterLevelUpInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public async Task<CharacterLevelUpResult> PerformLevelUp(CharacterLevelUpContext ctx)
        {
            int currencyToLevel = GetCurrencyToNextLevel(ctx.CurrentLevel);
            CharacterLevelUpInputContext inputCtx = new CharacterLevelUpInputContext()
            {
                CharacterID = ctx.CharacterID,
                CostToLevel = GetCurrencyToNextLevel(ctx.CurrentLevel),
                FromLevel = ctx.CurrentLevel,
                ToLevel = ctx.CurrentLevel + 1,
            };

            CharacterLevelUpInputResult inputResult = await _inputHandler.GetLevelUpInput(inputCtx);
            if(inputResult.IsLevellingUp)
            {
                return new CharacterLevelUpResult()
                {
                    RemainingCurrency = ctx.Currency - currencyToLevel,
                    ResultingLevel = ctx.CurrentLevel + 1,
                    Constitution = inputResult.StatisticToLevel == "CONSTITUTION" ? (ctx.Constitution + 1) : ctx.Constitution,
                    Strength = inputResult.StatisticToLevel == "STRENGTH" ? (ctx.Strength + 1) : ctx.Strength,
                    Intellect = inputResult.StatisticToLevel == "INTELLECT" ? (ctx.Intellect + 1) : ctx.Intellect,
                    Speed = inputResult.StatisticToLevel == "SPEED" ? (ctx.Speed + 1) : ctx.Speed,
                };
            }
            else
            {
                return new CharacterLevelUpResult()
                {
                    RemainingCurrency = ctx.Currency,
                    ResultingLevel = ctx.CurrentLevel,
                    Constitution = ctx.Constitution,
                    Strength = ctx.Strength,
                    Intellect = ctx.Intellect,
                    Speed = ctx.Speed,
                };
            }
        }

        private int GetCurrencyToNextLevel(int currentLevel)
        {
            return (int) (((currentLevel+1) * 100) * 1.2);
        }
    }
}