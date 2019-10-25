using System;
using System.Collections.Generic;

namespace LINQ_Sample
{
    public class Racer : IComparable<Racer>, IFormattable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Wins { get; set; }
        public string Country { get; set; }
        public int Starts { get; set; }
        public IEnumerable<string> Cars { get; }
        public IEnumerable<int> Years { get; }

        public Racer(string firstName, string lastName, string country,
            int starts, int wins, IEnumerable<int> years, IEnumerable<string> cars)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.Starts = starts;
            this.Wins = wins;
            this.Years = years != null ? new List<int>(years) : new List<int>();
            this.Cars = cars != null ? new List<string>(cars) : new List<string>();
        }

        public Racer(string firstName, string lastName, string country, int starts, int wins)
            : this(firstName, lastName, country, starts, wins, null, null)
        {
        }

        public override string ToString() => FirstName + " " + LastName;

        public int CompareTo(Racer other) => LastName.CompareTo(other?.LastName);

        public string Tostring(string format) => ToString(format, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "N":
                    return "None";

                case "F":
                    return FirstName;

                case "L":
                    return LastName;

                case "C":
                    return Country;

                case "S":
                    return Starts.ToString();

                case "W":
                    return Wins.ToString();

                case "A":
                    return $"{FirstName} {LastName},{Country}; start:{Starts}, wins:{Wins}";
                default:
                    throw new FormatException($"Format {format} not supproted");
            }
        }
    }
}