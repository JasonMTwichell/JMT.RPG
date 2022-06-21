using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public class CombatInputHandler : ICombatInputHandler
    {
        public async Task<CombatInputResult> GetInput(CombatContext ctx)
        {
            Console.WriteLine("Choose Action...");
            return new CombatInputResult()
            {
                ChosenAbilityId = "1",
                TargetId = "1",
            };
        }
    }
}
