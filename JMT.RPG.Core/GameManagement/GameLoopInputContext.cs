namespace JMT.RPG.Core.GameManagement
{
    public record GameLoopInputContext
    {
        public IEnumerable<UserActionOption> UserActionOptions { get; init; }
    }
}
