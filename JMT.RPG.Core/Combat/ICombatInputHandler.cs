namespace JMT.RPG.Core.Combat
{
    public interface ICombatInputHandler
    {
        Task<CombatInputResult> GetInput(CombatInputContext ctx);
    }
}