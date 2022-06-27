using JMT.RPG.Core.Contracts.Combat;
using System.Threading.Tasks;

namespace JMT.RPG.Test.Combat
{
    internal class DummyCombatInputHandler : ICombatInputHandler
    {
        private string _defaultPlayerAbilityID;
        private string _defaultEnemyAbilityID;
        public DummyCombatInputHandler(string defaultPlayerAbilityID, string defaultEnemyAbilityID)
        {
            _defaultPlayerAbilityID = defaultPlayerAbilityID;
            _defaultEnemyAbilityID = defaultEnemyAbilityID;
        }

        public async Task<CombatInputResult> GetInput(CombatInputContext ctx)
        {
            if (ctx.CombatantID == "PLAYER")
            {
                return new CombatInputResult()
                {
                    CombatantID = "PLAYER",
                    TargetID = "ENEMY",
                    ChosenAbilityID = _defaultPlayerAbilityID,
                };
            }
            else
            {
                return new CombatInputResult()
                {
                    CombatantID = "ENEMY",
                    TargetID = "PLAYER",
                    ChosenAbilityID = _defaultEnemyAbilityID,
                };
            }
        }
    }
}
