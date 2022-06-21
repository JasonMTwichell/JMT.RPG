namespace JMT.RPG.Core.Contracts.Combat
{
    public interface ICombatInputHandler
    {
        Task<CombatInputResult> GetInput(CombatContext ctx);
    }
}