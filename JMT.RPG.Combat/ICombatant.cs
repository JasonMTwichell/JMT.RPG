namespace JMT.RPG.Combat
{
    public interface ICombatant
    {
        CombatantBattleContext GetCombatantBattleContext();
        void ApplyCombatantBattleContext(CombatantBattleContext combatantCtx);
    }
}
