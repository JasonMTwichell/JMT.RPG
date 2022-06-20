namespace JMT.RPG.Core.Interfaces
{
    public interface IInputHandler
    {
        Task<InputResult> GetInput<T>(T context);
    }
}