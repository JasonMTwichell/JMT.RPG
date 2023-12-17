namespace JMT.RPG.Core.Campaign
{
    public record ItemEffect
    {
        public string EffectedAttribute { get; init; }
        public string EffectType { get; init; }
        public int Magnitude { get; init; }
        public int MagnitudeFactor { get; init; } //  all calcs are += so -1 to do damage for example
        public ItemEffect ForwardEffect { get; init; }
    }
}