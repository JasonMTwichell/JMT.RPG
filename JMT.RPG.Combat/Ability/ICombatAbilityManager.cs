using JMT.RPG.Combat.Effect;
using JMT.RPG.Core.Combat;

namespace JMT.RPG.Combat.Ability
{
    public interface ICombatAbilityManager
    {
        IEnumerable<ResolvedEffect> ResolveCombatAbility(CombatAbilityResolutionContext resCtx);
        CombatAbility ApplyCooldown(CombatAbility combatAbility, int coolDown); //gets added so use negative to reduce
    }
}
