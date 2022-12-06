using System.Collections.Generic;

namespace _Scripts.Extensions.Enum
{
    public static class EnumIterationExtensions
    {
        /// <summary>
        /// Gets an enumerable from Enum flags
        /// </summary>
        /// <param name="input">Enum to extract flags from</param>
        /// <returns>Enumerable of Enum Flags</returns>
        public static IEnumerable<System.Enum> GetFlags(this System.Enum input)
        {
            foreach (System.Enum value in System.Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }
    }
}