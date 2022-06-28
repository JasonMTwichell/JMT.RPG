namespace JMT.RPG.Core.CharacterManagement
{
    public record CharacterLevelUpContext
    {
        public int Currency { get; init; }
        public int CurrentLevel { get; init; }
        public int Constitution { get; init; }
        public int Strength { get; init; }
        public int Intellect { get;init; }
        public int Speed { get; init; }
    }
}