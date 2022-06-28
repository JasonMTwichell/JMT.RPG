using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Combat
{
    public interface ICombatManager
    {
        Task<CombatResult> PerformCombat(CombatEncounterContext combatEncounterCtx);
    }
}
