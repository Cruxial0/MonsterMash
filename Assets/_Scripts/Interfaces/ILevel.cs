using System.Collections.Generic;
using _Scripts.Handlers;

namespace _Scripts.Interfaces
{
    public interface ILevel
    {
        public int LevelID { get; }
        public Level Level { get; }
        public List<IEvent> Events { get; }
    }
}