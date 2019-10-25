using System.Collections.Generic;

namespace LINQ_Sample
{
    public static class Formulal
    {
        private static List<Racer> _racers;

        public static IList<Racer> GetChampions()
        {
            if (_racers == null)
            {
                _racers = new List<Racer>(40);
                _racers.Add(new Racer("Nino", "Farina", "Italy", 33, 5, new int[] { 1950 }
                , new string[] { "Alfa Rmomeo" }));
                _racers.Add(new Racer("Alberto", "Ascari", "Italy", 32, 10, new int[] { 1952, 1953 }
                , new string[] { "Ferrari" }));
                _racers.Add(new Racer("Juan Manuel", "fangio", "Argentina", 51, 24, new int[] { 1951, 1954, 1955, 1956, 1957 }
                , new string[] { "Alfa Rmomeo", "Maserati", "Mercedes", "Ferrari" }));
                _racers.Add(new Racer("MIke", "Hawthorn", "UK", 45, 3, new int[] { 1958 }, new string[] { "Ferrari" }));
                _racers.Add(new Racer("Phil", "Hill", "USA", 48, 3, new int[] { 1961 }, new string[] { "Ferrari" }));
                _racers.Add(new Racer("John", "Surtees", "UK", 111, 6, new int[] { 1964 }, new string[] { "Ferrari" }));
                _racers.Add(new Racer("Jim", "Clark", "UK", 72, 25, new int[] { 1963, 1965 }, new string[] { "Lotus" }));
                _racers.Add(new Racer("Jack", "Brabham", "Australia", 125, 14, new int[] { 1959, 1960, 1966 }
                , new string[] { "Cooper", "Brabham" }));
                _racers.Add(new Racer("Denny", "Hulme", "New Zealand", 112, 8, new int[] { 1967 }, new string[] { "Brabham" }));
                _racers.Add(new Racer("Graham", "Hill", "UK", 176, 14, new int[] { 1962, 1968 }, new string[] { "BRM", "Lotus" }));
                _racers.Add(new Racer("Jochen", "Rindt", "Austria", 60, 6, new int[] { 1970 }, new string[] { "Lotus" }));
                _racers.Add(new Racer("Jackie", "Stewart", "UK", 99, 27, new int[] { 1969, 1971, 1973 }
                , new string[] { "Matra", "Tyrrell" }));
                _racers.Add(new Racer("张", "小新", "China", 86, 6, new int[] { 1974 }, new string[] { "Brabham" }));
                _racers.Add(new Racer("刘", "备", "China", 98, 15, new int[] { 1976, 1977 }, new string[] { "Brabham", "Lotus" }));
                _racers.Add(new Racer("关", "羽", "China", 130, 14, new int[] { 1975, 1979, 1981 }, new string[] { "Tyrrell" }));
                _racers.Add(new Racer("曹", "操", "China", 89, 18, new int[] { 1978 }, new string[] { "Cooper", "BRM" }));
                _racers.Add(new Racer("赵", "云", "China", 83, 11, new int[] { 1980, 1983 }, new string[] { "BRM", "Lotus" }));
                _racers.Add(new Racer("刘", "邦", "China", 108, 16, new int[] { 1982 }, new string[] { "Brabham", "Matra", "Lotus" }));
                _racers.Add(new Racer("项", "羽", "China", 100, 26, new int[] { 1984, 1985, 1986 }, new string[] { "Ferrari" }));
            }

            return _racers;
        }

        private static List<Team> _teams;

        public static IList<Team> GetContructorChampions()
        {
            if (_teams == null)
            {
                _teams = new List<Team>() {
                    new Team("Vanwall",1958),
                    new Team("Cooper",1959,1960),
                    new Team("Ferrari",1961,1964,1975,1976,1977,1979,1982,1983,1999,2000,2001,2002,2003,2004,2007,2008),
                    new Team("BRM",1962),
                    new Team("Lotus",1963,1965,1968,1970,1972,1973,1978),
                    new Team("Brabham",1966,1967),
                    new Team("Matra",1969),
                    new Team("Tyrrell",1971),
                    new Team("McLaren",1974,1984,1985,1988,1989,1990,1991,1998),
                    new Team("Williams",1980,1981,1992,1993,1994,1996,1997),
                    new Team("Benetton",1995),
                    new Team("Renault",2005,2006),
                    new Team("Brawn GP",2009),
                    new Team("三国",1974,1975,1976,1977,1978,1979,1980,1981,1982,1983),
                    new Team("汉强",1984,1985,1986,1987)
                };
            }
            return _teams;
        }

        private static List<Championship> championships;

        public static IEnumerable<Championship> GetChampionships()
        {
            if (championships == null)
            {
                championships = new List<Championship>();
                championships.Add(new Championship
                {
                    Year = 1950,
                    First = "Nino Farina",
                    Second = "Juan Manuel Fangio",
                    Third = "Luigi Fagioli"
                });
                championships.Add(new Championship
                {
                    Year = 1951,
                    First = "Juan Manuel Fangio",
                    Second = "Alberto Ascari",
                    Third = "Froilan Gonzalez"
                });
            }
            return championships;
        }
    }
}