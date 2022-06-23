using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMT.RPG.Combat;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public class CombatInputHandler : ICombatInputHandler
    {
        public Task<CombatInputResult> GetInput(CombatInputContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}
