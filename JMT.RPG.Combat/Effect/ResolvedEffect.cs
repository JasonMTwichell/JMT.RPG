namespace JMT.RPG.Combat.Effect
{
    public record ResolvedEffect
    {
        public string TargetID { get; init; }
        public string EffectedAttribute { get; init; }
        public int ResolvedMagnitude { get; init; }
        public ResolvedEffect? ForwardEffect { get; init; }
    }
}
