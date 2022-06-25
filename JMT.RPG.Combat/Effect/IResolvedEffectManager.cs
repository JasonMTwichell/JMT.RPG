using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMT.RPG.Combat.Combatants;

namespace JMT.RPG.Combat.Effect
{
    public interface IResolvedEffectManager
    {
        CombatantBattleContext ApplyEffects(CombatantBattleContext ctx, IEnumerable<ResolvedEffect> resolvedEffects);
        CombatantBattleContext ResolveAppliedEffects(CombatantBattleContext ctx);
        CombatantBattleContext CarryForwardEffects(CombatantBattleContext ctx);
    }
}
