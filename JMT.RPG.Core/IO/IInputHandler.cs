namespace JMT.Roguelike.Combat
{
    public interface IInputHandler
    {
        Task<InputResult> GetInput<T>(T context);
    }
}