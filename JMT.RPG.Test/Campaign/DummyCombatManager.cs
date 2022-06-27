using JMT.RPG.Core.Contracts.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Test.Campaign
{
    internal class DummyCombatManager : ICombatManager
    {
        private CombatResult _result;
        public DummyCombatManager(CombatResult result)
        {
            _result = result;
        }

        public async Task<CombatResult> PerformCombat(CombatEncounterContext combatEncounterCtx)
        {
            return _result;
        }
    }
}
