﻿using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat.Combatants
{
    public record CombatantState
    {
        public int TotalHealth { get; set; }
        public int RemainingHealth { get; set; }
        public int Intellect { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
    }
}