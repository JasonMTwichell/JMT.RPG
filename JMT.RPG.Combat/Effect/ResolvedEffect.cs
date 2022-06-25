﻿namespace JMT.RPG.Combat.Effect
{
    public record ResolvedEffect
    {
        public string TargetID { get; set; }
        public string EffectedAttribute { get; set; }
        public int ResolvedMagnitude { get; set; }
        public ResolvedEffect? ForwardEffect { get; set; }
    }
}