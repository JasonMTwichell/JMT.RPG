using System.Runtime.Serialization;

namespace JMT.RPG.Core.CharacterManagement
{
    [Serializable]
    internal class IllegalPurchaseException : Exception
    {
        public IllegalPurchaseException()
        {
        }

        public IllegalPurchaseException(string? message) : base(message)
        {
        }

        public IllegalPurchaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IllegalPurchaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}