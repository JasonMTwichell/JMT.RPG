﻿namespace JMT.RPG.Core.Combat
{
    public record CombatInputContext
    {
        public string CombatantID { get; init; }
        public int TurnNumber { get; init; }
        public CombatantContext[] CombatantContexts { get; init; }
        public CombatItem[] AvailableItems { get; set; }
    }
}