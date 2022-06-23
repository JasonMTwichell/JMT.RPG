using JMT.RPG.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Combat
{
    public interface IResolvedEffectManager
    {
        CombatantBattleContext ApplyEffects(CombatantBattleContext ctx, IEnumerable<ResolvedEffect> resolvedEffects);
        CombatantBattleContext ResolveAppliedEffects(CombatantBattleContext ctx);
        CombatantBattleContext CarryForwardEffects(CombatantBattleContext ctx);
    }
}
