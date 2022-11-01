using System.Collections.Generic;
using _Scripts.Handlers;

namespace _Scripts.Interfaces
{
    public interface ILevel
    {
        public int LevelID { get; } //Local ID of the level
        public Level Level { get; } //Level object
        public List<IEvent> Events { get; } //Events associated with level
        public StarLevels StarLevels { get; }
    }

    public class StarLevels
    {
        public double ThreeStarRequirement { get; set; }
        public double TwoStarRequirement { get; set; }
        public double OneStarRequirement { get; set; }
        public float NoiseThreshold { get; set; }
    }
}