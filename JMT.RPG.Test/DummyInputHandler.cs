using JMT.Roguelike.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.Roguelike.Test
{
    internal class DummyInputHandler : IInputHandler
    {
        private string _defaultAbility;
        private string _defaultTarget;
        public DummyInputHandler(string defaultAbility, string defaultTarget) 
        {
            _defaultAbility = defaultAbility;
            _defaultTarget = defaultTarget;
        }

        public async Task<InputResult> GetInput<T>(T context)
        {
            return new InputResult()
            {
                ChosenAbilityId = _defaultAbility,
                TargetId = _defaultTarget,
            };
        }
    }
}
