using JMT.RPG.Combat;
using JMT.RPG.Combat;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public interface ICombatAbilityManager
    {
        IEnumerable<ResolvedEffect> ResolveCombatAbility(CombatAbilityResolutionContext resCtx);
        void ApplyCooldown(CombatAbility combatAbility, int coolDown); //gets added so use negative to reduce
    }
}
