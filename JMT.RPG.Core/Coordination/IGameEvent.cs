﻿namespace JMT.RPG.Core.Coordination
{
    public interface IGameEvent
    {
        public string EventID { get; }
        public DateTime EventTimestamp { get; }
    }
}