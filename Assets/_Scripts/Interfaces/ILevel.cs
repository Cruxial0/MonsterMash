using System.Collections.Generic;
using _Scripts.Handlers;

namespace _Scripts.Interfaces
{
    public interface ILevel
    {
        public int LevelID { get; } //Local ID of the level
        public Level Level { get; } //Level object
        public List<IEvent> Events { get; } //Events associated with level
    }
}