using JMT.RPG.Core.Interfaces;
using System.Threading.Tasks;

namespace JMT.RPG.Test
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
