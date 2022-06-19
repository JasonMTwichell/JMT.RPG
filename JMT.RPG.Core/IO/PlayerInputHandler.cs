using JMT.RPG.Core.Coordination;
using System.Reactive.Linq;

namespace JMT.RPG.Core.IO
{
    internal class PlayerInputHandler
    {
        private IEventBus _eventBus;
        private string _playerID;
        public PlayerInputHandler(IEventBus eventBus, string playerID)
        {
            _eventBus = eventBus;
            _playerID = playerID;

            _eventBus.GetEventStream<InputRequestEvent>().Where(t => t.PlayerID == _playerID).Subscribe(ir => RequestInput(ir));
        }

        private Task RequestInput(InputRequestEvent irEvent)
        {
            Console.WriteLine($"Requesting input from player {irEvent.PlayerID}");
            string playerInput = Console.ReadLine();

            _eventBus.Publish(new InputResponseEvent(_playerID, playerInput));
            return Task.CompletedTask;
        }
    }
}
