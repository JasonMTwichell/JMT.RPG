using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Combat
{
    public interface ICombatManager
    {
        Task<CombatResult> PerformCombat();
    }
}
