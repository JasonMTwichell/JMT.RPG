namespace JMT.RPG.Core.CharacterManagement
{
    public record CharacterLevelUpResult
    {
        public int ResultingLevel { get; init; }
        public int RemainingCurrency { get; init; }        
        public int Constitution { get; init; }
        public int Strength { get; init; }
        public int Intellect { get; init; }
        public int Speed { get; init; }
    }
}