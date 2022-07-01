namespace JMT.RPG.CharacterManagement
{
    public interface ICharacterSkillInputManager
    {
        Task<CharacterSkillInputResult> GetInput(CharacterSkillInputContext req);
    }
}
