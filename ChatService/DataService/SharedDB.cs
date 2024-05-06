
using System.Collections.Concurrent;
using ChatService.Models;

namespace ChatService.DataService
{
    public class SharedDB
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections;

        public SharedDB()
        {
            _connections = new ConcurrentDictionary<string, UserConnection>();
        }

        public ConcurrentDictionary<string, UserConnection> connections => _connections;
    }
}
