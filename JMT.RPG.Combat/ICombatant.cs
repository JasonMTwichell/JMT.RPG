using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Combat
{
    public interface ICombatant
    {
        string Id { get; init; }        
        int Speed { get; }
        int RemainingHealth { get; }
        Task<IEnumerable<ResolvedEffect>> ChooseCombatAbility(CombatContext combatContext);
        void StartOfTurnPhase();
        void ApplyEffects(ResolvedEffect[] targetingEffects);
        void ActionResolutionPhase();
        void EndOfTurnPhase();
        CombatantContext GetCombatantContext();
    }
}
