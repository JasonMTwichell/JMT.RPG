namespace JMT.RPG.Combat.Combatants
{
    public interface ICombatant
    {
        CombatantBattleContext GetCombatantBattleContext();
        void ApplyCombatantBattleContext(CombatantBattleContext combatantCtx);
    }
}
