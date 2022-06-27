using JMT.RPG.Core.Contracts.Combat;
using System.Threading.Tasks;

namespace JMT.RPG.Test.Combat
{
    internal class DummyCombatItemInputHandler : ICombatInputHandler
    {
        private string _defaultPlayerItemID;
        private string _defaultEnemyAbilityID;
        public DummyCombatItemInputHandler(string defaultPlayerItemID, string defaultEnemyAbilityID)
        {
            _defaultPlayerItemID = defaultPlayerItemID;
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
                    ChoseItemID = _defaultPlayerItemID,
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
