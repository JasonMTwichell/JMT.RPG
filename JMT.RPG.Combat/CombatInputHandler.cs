using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.Roguelike.Combat
{
    public class CombatInputHandler : IInputHandler
    {
        public async Task<InputResult> GetInput<T>(T context)
        {
            Console.WriteLine("Choose Action...");
            return new InputResult()
            {
                ChosenAbilityId = "1",
                TargetId = "1",
            };
        }
    }
}
