using JMT.RPG.Combat;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public interface ICombatAbilityManager
    {
        IEnumerable<CombatAbility> GetCombatAbilities();
        IEnumerable<ResolvedEffect> ResolveCombatAbility(CombatAbilityResolutionContext resCtx);
        void ApplyCooldownAllAbilities(int coolDown); //gets added so use negative to reduce
        void ApplyCooldown(string combatAbilityID, int coolDown); //gets added so use negative to reduce
    }
}
