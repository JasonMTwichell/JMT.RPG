﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMT.RPG.Combat.Effect;
using JMT.RPG.Core.Combat;

namespace JMT.RPG.Combat.Combatants
{
    public record CombatantBattleContext
    {
        public string CombatantID { get; init; }
        public string Name { get; init; }
        public int Level { get; init; }
        public bool IsEnemyCombatant { get; init; }
        public CombatantState CombatantState { get; init; }
        public IEnumerable<CombatAbility> CombatAbilities { get; init; }
        public IEnumerable<ResolvedEffect> AppliedEffects { get; init; }
        public IEnumerable<ResolvedEffect> CarryForwardEffects { get; init; }
    }
}
