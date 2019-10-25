using System.Collections.Generic;

namespace LINQ_Sample
{
    public class Team
    {
        public string Name { get; }
        public IEnumerable<int> Years { get; }

        public Team(string name, params int[] years)
        {
            this.Name = name;
            this.Years = years != null ? new List<int>(years) : new List<int>();
        }
    }
}