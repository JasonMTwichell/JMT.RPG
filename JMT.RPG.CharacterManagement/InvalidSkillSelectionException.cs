using System.Runtime.Serialization;

namespace JMT.RPG.CharacterManagement
{
    internal class InvalidSkillSelectionException : Exception
    {
        public InvalidSkillSelectionException(string? message) : base(message)
        {
        }
    }
}