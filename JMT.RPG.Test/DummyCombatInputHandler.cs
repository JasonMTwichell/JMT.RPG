using JMT.RPG.Core.Contracts.Combat;
using System.Threading.Tasks;

namespace JMT.RPG.Test
{
    internal class DummyCombatInputHandler : ICombatInputHandler
    {
        private string _defaultAbility;
        public DummyCombatInputHandler(string defaultAbility) 
        {
            _defaultAbility = defaultAbility;
        }

        public async Task<CombatInputResult> GetInput(CombatInputContext ctx)
        {
            return new CombatInputResult()
            {
                CombatantID = ctx.CombatantID,
                ChosenAbilityID = _defaultAbility,
                TargetID = ctx.CombatantID == "1" ? "2":"1",
            };
        }
    }
}
