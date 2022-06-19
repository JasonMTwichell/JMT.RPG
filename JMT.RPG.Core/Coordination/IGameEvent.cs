namespace JMT.Roguelike.Core.Coordination
{
    public interface IGameEvent
    {
        public string EventID { get; }
        public DateTime EventTimestamp { get; }
    }
}