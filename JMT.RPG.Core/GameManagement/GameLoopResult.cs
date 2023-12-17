namespace JMT.RPG.Core.GameManagement
{
    public record GameLoopResult
    {
        public SavedGameState SaveGameState { get; init; }
    }
}
