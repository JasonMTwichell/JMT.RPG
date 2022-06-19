namespace JMT.RPG.Combat
{
    public interface IInputHandler
    {
        Task<InputResult> GetInput<T>(T context);
    }
}