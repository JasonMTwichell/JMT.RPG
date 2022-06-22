using JMT.RPG.Core.Contracts.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Combat
{
    public interface ICombatantStateManager
    {
        CombatantState GetCombatantState();
        void ApplyEffects(IEnumerable<ResolvedEffect> resolvedEffects, ICombatant combatant);
        CombatantState ResolveAppliedEffects();
        void CarryForwardEffects();
    }
}
