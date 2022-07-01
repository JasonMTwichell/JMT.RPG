namespace JMT.RPG.CharacterManagement
{
    public interface ICharacterLevelUpInputHandler
    {
        Task<CharacterLevelUpInputResult> GetLevelUpInput(CharacterLevelUpInputContext ctx); 
    }

    public record CharacterLevelUpInputResult
    {
        public bool IsLevellingUp { get; init; }
        public string StatisticToLevel { get; init; }        
    }

    public record CharacterLevelUpInputContext
    {
        public string CharacterID { get; init; }
        public int CostToLevel { get; init; }
        public int FromLevel { get; init; }
        public int ToLevel { get; init; }
    }
}