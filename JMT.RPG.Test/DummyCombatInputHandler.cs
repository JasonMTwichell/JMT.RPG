using JMT.RPG.Core.Contracts.Combat;
using System.Threading.Tasks;

namespace JMT.RPG.Test
{
    internal class DummyCombatInputHandler : ICombatInputHandler
    {
        private string _defaultAbility;
        private string _defaultTarget;
        public DummyCombatInputHandler(string defaultAbility, string defaultTarget) 
        {
            _defaultAbility = defaultAbility;
            _defaultTarget = defaultTarget;
        }

        public async Task<CombatInputResult> GetInput(CombatContext context)
        {
            return new CombatInputResult()
            {
                ChosenAbilityId = _defaultAbility,
                TargetId = _defaultTarget,
            };
        }
    }
}
