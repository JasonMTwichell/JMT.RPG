using System.Runtime.Serialization;

namespace JMT.RPG.Core.GameManagement
{
    public class UnsupportedGameLoopActionException : Exception
    {
        private const string ErrorMessage = "The selected action is not supported";
        public UnsupportedGameLoopActionException() : base(ErrorMessage)
        {
        }
    }
}