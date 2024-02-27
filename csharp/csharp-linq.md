# C# LINQ

LINQ（Language Integrated Query，语言集成查询）。在C# 语言中集成了查询语法，可以用相同的语法访问不同的数据源。

命名空间`System.Linq`下的类`Enumerate`中定义了许多LINQ扩展方法，用于可以在实现了`IEnumerable<T>`接口的任意集合上使用LINQ查询。

## 扩展方法

C#扩展方法在静态类中声明，定义为一个静态方法，其中第一个参数定义了它扩展的类型，扩展方法必须对第一个参数使用`this`关键字。

```c#
public static class StringExtension
{
    public static void WriteLine(this string str)
    {
        Console.WriteLine(str);
    }
}
```

调用方式有两种：

```c#
//方式一
"测试".WriteLine();
//方式二
StringExtension.WriteLine("测试二");
```

采用方式一的方式调用，需要导入该扩展方法所在类的命名空间即可。在使用LINQ时，需要导入`System.Linq`命名空间。



## 示例实体定义

为了更好的说明LINQ的使用， 我们将使用具体的示例进行说明，在该示例中，分别定义如下几个实体：

Racer.cs：该类用来显示赛车手信息

```c#
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
```

Team.cs：该类包含获得冠军车队称号的车队名称和年份

```c#
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
```

Formulal.cs：返回获的冠军的赛车手信息集合、冠军车队列表。

```c#
public static class Formulal
{
    private static List<Racer> _racers;
    public static IList<Racer> GetChampions()
    {
        if (_racers == null)
        {
            _racers = new List<Racer>(40);
            _racers.Add(new Racer("Nino", "Farina", "Italy", 33, 5
           , new int[] { 1950 }, new string[] { "Alfa Rmomeo" }));
            _racers.Add(new Racer("Alberto", "Ascari", "Italy", 32, 10
            , new int[] { 1952, 1953 }  , new string[] { "Ferrari" }));
            _racers.Add(new Racer("Juan Manuel", "fangio", "Argentina", 51, 24
            , new int[] { 1951, 1954, 1955, 1956, 1957 }
            , new string[] { "Alfa Rmomeo", "Maserati", "Mercedes", "Ferrari" }));
            _racers.Add(new Racer("MIke", "Hawthorn", "UK", 45, 3
            , new int[] { 1958 }, new string[] { "Ferrari" }));
            _racers.Add(new Racer("Phil", "Hill", "USA", 48, 3
            , new int[] { 1961 }, new string[] { "Ferrari" }));
            _racers.Add(new Racer("John", "Surtees", "UK", 111, 6
            , new int[] { 1964 }, new string[] { "Ferrari" }));
            _racers.Add(new Racer("Jim", "Clark", "UK", 72, 25
            , new int[] { 1963, 1965 }, new string[] { "Lotus" }));
            _racers.Add(new Racer("Jack", "Brabham", "Australia", 125, 14
            , new int[] { 1959, 1960, 1966 }
            , new string[] { "Cooper", "Brabham" }));
            _racers.Add(new Racer("Denny", "Hulme", "New Zealand", 112, 8
            , new int[] { 1967 }, new string[] { "Brabham" }));
            _racers.Add(new Racer("Graham", "Hill", "UK", 176, 14
            , new int[] { 1962, 1968 }, new string[] { "BRM", "Lotus" }));
            _racers.Add(new Racer("Jochen", "Rindt", "Austria", 60, 6
            , new int[] { 1970 }, new string[] { "Lotus" }));
            _racers.Add(new Racer("Jackie", "Stewart", "UK", 99, 27
            , new int[] { 1969, 1971, 1973 }, new string[] { "Matra", "Tyrrell" }));
            _racers.Add(new Racer("张", "小新", "China", 86, 6
            , new int[] { 1974 }, new string[] { "Brabham" }));
            _racers.Add(new Racer("刘", "备", "China", 98, 15
            , new int[] { 1976, 1977 }, new string[] { "Brabham", "Lotus" }));
            _racers.Add(new Racer("关", "羽", "China", 130, 14
            , new int[] { 1975, 1979, 1981 }, new string[] { "Tyrrell" }));
            _racers.Add(new Racer("曹", "操", "China", 89, 18
            , new int[] { 1978 }, new string[] { "Cooper", "BRM" }));
            _racers.Add(new Racer("赵", "云", "China", 83, 11
            , new int[] { 1980, 1983 }, new string[] { "BRM", "Lotus" }));
            _racers.Add(new Racer("刘", "邦", "China", 108, 16
            , new int[] { 1982 }, new string[] { "Brabham", "Matra", "Lotus" }));
            _racers.Add(new Racer("项", "羽", "China", 100, 26
            , new int[] { 1984, 1985, 1986 }, new string[] { "Ferrari" }));
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
                new Team("Ferrari",1961,1964,1975,1976,1977,1979,1982
                         ,1983,1999,2000,2001,2002,2003,2004,2007,2008),
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
```

Championship.cs 和RacerInfo.cs：附加类

```c#
public class Championship
{
    public int Year { get; set; }
    public string First { get; set; }
    public string Second { get; set; }
    public string Third { get; set; }
}
public class RacerInfo
{
    public int Year { get; set; }
    public int Positon { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```



## LINQ查询

例如，查询来自China的世界冠军，并按照夺冠次数排序：

```c#
var query = from r in Formulal.GetChampions()
            where r.Country == "China"
            orderby r.Wins descending
            select r;

foreach (Racer r in query)
{
    Console.WriteLine($"{r:A}");
}
```

上述中的query之后的语句就是一个**LINQ查询表达式**。查询表达式必须以`from`子句开头，以`select`或`group`子句结束。在这两个子句之间，可以使用`where`、`orderby`、`join`、`let`和其他`from`子句。

> 在编译时，查询表达式根据 C# 规范规则转换成标准查询运算符方法调用。 可使用查询语法表示的任何查询都可以使用方法语法进行表示。 不过，在大多数情况下，查询语法的可读性更高，也更为简洁。  

注意：上述示例中的变量query只指定了LINQ查询，该查询不是通过这个赋值语句执行的，而是在使用`foreach`语句对其进行循环访问时，该查询才会执行，关于这一点，请查看下述中”推迟查询的执行“的相关说明。

将上述的查询表达式使用LINQ扩展方法进行实现：

```c#
var champions = new List<Racer>(Formulal.GetChampions());
IEnumerable<Racer> brazilChampions = champions.Where(r => r.Country == "China")
     .OrderByDescending(r => r.Wins)
.Select(r => r);

foreach (Racer r in brazilChampions)
{
    Console.WriteLine($"{r:A}");
}
```

#### 推迟查询的执行

在运行期间定义查询表达式时，查询并不会运行，而是在迭代数据项是运行。例如：

```c#
var names = new List<string> { "Nino", "Alberto", "Juan", "Mike", "Phil" };
//在定义的时候并不会马上执行
var namesWithJ = from n in names
                 where n.StartsWith("J")
                 orderby n
                 select n;
Console.WriteLine("以J开头的有：");
//在遍历的时候才出发表达式的执行
foreach (string name in namesWithJ)
{
    Console.WriteLine(name);
}

names.Add("John");
names.Add("Jim");
names.Add("Jack");
names.Add("Denny");
Console.WriteLine("添加元素后:");
//再次出发表达式的执行
foreach (string name in namesWithJ)
{
    Console.WriteLine(name);
}
```

上述执行结果如下：

```
以J开头的有：
Juan
添加元素后:
Jack
Jim
John
Juan
> 
```

通过这个示例可以很清楚的说明，在定义LINQ表达式赋值语句时，并没有马上得到结果，而是在需要遍历时，才将结果返回。这就是为什么在第二次添加元素后 ，不用再重新使用赋值语句，就可以直接遍历得到结果。我们把这种不会立即反馈结果的情况，叫做 延迟加载或推迟查询。

解决延迟加载的办法就是使用`ToArray()`、`ToList()`等方法，它们可以在定义好查询表达式后立即返回查询的结果。例如：

```c#
var names = new List<string> { "Nino", "Alberto", "Juan", "Mike", "Phil" };
var namesWithJToList = (from n in names
                        where n.StartsWith("J")
                        orderby n
                        select n).ToList(); //注意：此处使用了ToList()方法
 

Console.WriteLine("调用ToList():");
foreach (string name in namesWithJToList)
{
    Console.WriteLine(name);
}
names.Add("John");
names.Add("Jim");
names.Add("Jack");
names.Add("Denny");
Console.WriteLine("添加了新的元素后：");
foreach (string name in namesWithJToList)
{
    Console.WriteLine(name);
}
```

上述执行结果：

```
调用ToList():
Juan
添加了新的元素后：
Juan
> 
```

可以很清楚的看到，在调用`ToList()`方法之后，LINQ表达式会立即执行，此时变量`namesWithJToList`存储的就是查询得到的结果集，它是一个独立的List集合，当再次添加元素时，该集合中的结果并没有发生改变，遍历时，依旧得到的是第一次的结果。



## 标准的查询操作符

| 标准查询操作符                                               | 说明                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| `Where` `OfType<TResult>`                                    | 筛选操作符定义了返回元素的条件。在`Where`查询操作符中可以使用谓词，例如，lambda表达式定义的谓词，来返回布尔值。`OfType<IResult>`根据类型筛选元素，只返回`TResult`类型的元素 |
| `Select` `SelectMany`                                        | 投射操作符用于把对象转换为另一个类型的新对象。`Select`和`SelectMany`定义了根据选择函数选择结果值的投射 |
| `OrderBy` `ThenBy` `OrderByDescending` `ThenByDescending` `Reverse` | 排序操作符改变所返回的元素的顺序。`OrderBy`按升序排序，`OrderByDescending`按降序排序。如果第一次排序的结果很类似，就可以使用`ThenBy`和`ThenBy` `Descending`操作符进行第二次排序。`Reverse`反转集合中元素的顺序。 |
| `Join` `GroupJoin`                                           | 连续操作符用于合并不直接相关的集合。使用`Join`操作符，可以根据键选择器函数联接两个集合，这类似于SQL中的`JOIN`。`GroupJoin`操作符联接两个集合，组合其结果。 |
| `GroupBy` `ToLookup`                                         | 组合操作符把数据放在组中。`GroupBy`操作符组合有公共键的元素。`ToLookup`通过创建一个一对多字典，来组合元素。 |
| `Any` `All` `Contains`                                       | 如果元素序列满足指定的条件，限定符操作符就返回布尔值。`Any`、`All`和`Contains`都是限定符操作符。`Any`确定集合中是否有满足谓词函数的元素；`All`确定集合中的所有元素是否都满足谓词函数；`Contains`检查某个元素是否在集合中。 |
| `Take` `Skip` `TakeWhile` `SkipWhile`                        | 分区操作符返回集合的一个子集。`Take`、`Skip`、`TakeWhile`和`SkipWhile`都是分区操作符。使用它们可以得到部分结果。使用`Take`必须指定要从集合中提取的元素的个数；`Skip`跳过指定的元素个数，提取其他元素；`TakeWhile`提取条件为真的元素，`SkipWhile`跳过条件为真的元素。 |
| `Distinct ` `Union` `Intersect` `Except` `Zip`               | 这些操作符都被称为`Set`操作符，因为它们最终返回一个集合。`Distinct`从集合中删除重复的元素。除了`Distinct`之外，其他`Set`操作符都需要两个集合。`Union`返回出现在其中一个集合中的唯一元素。`Intersect`返回两个集合中都有的元素。`Except`返回只出现在一个集合中的元素。`Zip`把两个集合合并为一个。 |
| `First` `FirstOrDefault` `Last` `LastOrDefault` `ElementAt` `ElementAtDefault` `Single` `SingleOrDefault` | 这些元素操作符仅返回一个元素。`First`返回第一个满足条件的元素。`FirstOrDefault`类似于`First`，但如果没有找到满足条件的元素，就返回类型的默认值。`Last`返回最后一个满足条件的元素。`ElementAt`指定了要返回的元素的位置。`Single`只返回一个满足条件的元素。如果有多个元素都满足条件，就抛出一个异常。所有的`XXXOrDefault`方法都类似于以相同前缀开头的方法，但如果没有找到该元素，他们就返回类型的默认值。 |
| `Count` `Sum` `Min` `Max` `Average` `Aggregate`              | 聚合操作符计算集合的一个值。利用这些聚合操作符，可以计算所有值的总和。所有元素的个数、值最大和最小的元素，以及平均值等。 |
| `ToArray` `AsEnumerable` `ToList` `ToDictionary` `Cast<TResult>` | 这些转换操作符将集合转换为数组：`IEnumerable`、`IList`、`IDictionary`等。`Cast`方法把集合的每个元素类型转换为泛型参数类型。 |
| `Empty` `Range` `Repeat`                                     | 这些生成操作符返回一个新集合。使用`Empty`时集合时空的；`Range`返回一系列数字；`Repeat`返回一个始终重复一个值的集合。 |

#### 筛选（Where）

LINQ中使用[`Where`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.where?view=netcore-2.1)对结果集进行过滤和筛选。在使用LINQ查询表达式时，`where`子句 可以合并多个表达式。

```c#
var racrers = from r in Formulal.GetChampions()
              where r.Wins > 15 && (r.Country == "China" || r.Country == "UK")
              select r;
```

上述使用LINQ扩展方法形式：

```c#
var racres2 = Formulal.GetChampions()
    .Where(r => r.Wins > 15 && (r.Country == "China" || r.Country == "UK"))
    .Select(r => r);
```

这两种执行的结果都是一样的。但是并不是所有的LINQ查询都可以使用LINQ查询表达式完成，也不是所有的LINQ扩展方法都能够映射到LINQ查询表达式语句上。LINQ扩展方法使用的范围要比查询表达式更广泛，尤其是一些高级查询，只能或更多的是使用扩展方法完成。同时，这两种形式也可以组合使用。

例如，使用`Where()`的另一种重载方法用索引进行筛选：

```c#
//使用索引筛选，只能使用扩展方法，不能使用Linq查询语句
var racers3 = Formulal.GetChampions()
    .Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0);
```

关于查询表达式和查询语法应该如何选择，这里引用了官方说明：

> **在编译时，查询表达式根据 C# 规范规则转换成标准查询运算符方法调用**。 可使用查询语法表示的任何查询都可以使用方法语法进行表示。 不过，在大多数情况下，查询语法的可读性更高，也更为简洁。  
>
> 通常，我们建议在编写 LINQ 查询时尽量使用查询语法，并在必要时尽可能使用方法语法。 这两种不同的形式在语义或性能上毫无差异。 查询表达式通常比使用方法语法编写的等同表达式更具可读性。 
>
> 一些查询操作（如 [`Count`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.count) 或 [`Max`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.max)）没有等效的查询表达式子句，因此必须表示为方法调用。 可以各种方式结合使用方法语法和查询语法。 有关详细信息，请参阅 [LINQ 中的查询语法和方法语法](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/query-syntax-and-method-syntax-in-linq)。 
>
> 本段文字原文链接：https://docs.microsoft.com/zh-cn/dotnet/csharp/linq/index



#### 类型筛选（OfType）

可以使用[`OfType()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.oftype?view=netframework-4.7.2)扩展方法基于类型进行筛选。

```c#
object[] data = { "one", 2, 3, "four", "five", 6 };
var query = data.OfType<string>();
foreach (var s in query)
{
    Console.WriteLine(s);
}
```

#### 复合的from子句（SelectMany）

如果一个集合中的某个对象依旧是一个集合，就可以使用`from`子句进行筛选。

```c#
//Formulal.GetChampions()返回Racer集合，每一个Racer的属性Cars是一个字符串数组
var ferrariDrivers = from r in Formulal.GetChampions()
                     from c in r.Cars
                     where c == "Ferrari"
                     orderby r.LastName
                     select r.FirstName + " " + r.LastName;
```

C#编译器把复合的`from`子句和LINQ查询转换为`SelectMany()`扩展方法。[SelectMany()](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.selectmany?view=netframework-4.7.2)方法可以迭代序列的序列。

```c#
var ferrariDrivers2 = Formulal.GetChampions()
    .SelectMany(r => r.Cars, (r, c) => new { Racer = r, Car = c })
    .Where(r => r.Car == "Ferrari")
    .OrderBy(r => r.Racer.LastName)
    .Select(r => r.Racer.FirstName + " " + r.Racer.LastName);
```

#### 排序（OrderBy/OrderByDescending/ThenBy/ThenByDescending）

- [`OrderBy()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.orderby?view=netframework-4.7.2)：按升序对序列的元素进行排序。 
- [`ThenBy()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.thenby?view=netframework-4.7.2)：按升序对序列中的元素进行后续排序。 
- [`OrderByDescending()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.orderbydescending?view=netframework-4.7.2)：按降序对序列的元素进行排序。 
- [`ThenByDescending()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.thenbydescending?view=netframework-4.7.2)：按降序对序列中的元素进行后续排序。 

```c#
var racers3 = (from r in Formulal.GetChampions()
               orderby r.Country, r.LastName, r.FirstName ascending
               select r).Take(10);
```

使用扩展方法：

```c#
var racers4 = Formulal.GetChampions()
    .OrderBy(r => r.Country)
    .ThenBy(r => r.LastName)
    .ThenByDescending(r => r.FirstName)
    .Take(10);
```

#### 分组（GroupBy）

如果要根据一个关键字值 对查询结果分组，可以使用`group`子句。对应的扩展方法为[`GroupBy()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.groupby?view=netframework-4.7.2)。

```c#
var countries = from r in Formulal.GetChampions()
                group r by r.Country into g //将分组信息放入标识符g中
                orderby g.Count() descending, g.Key
                where g.Count() >= 2
                select new
                {
                    Country = g.Key,
                    Count = g.Count()
                };
```

使用扩展方法的形式：

```c#
var countries2 = Formulal.GetChampions()
    .GroupBy(r => r.Country)
    .OrderByDescending(g => g.Count())
    .ThenBy(g => g.Key)
    .Where(g => g.Count() >= 2)
    .Select(g => new { Country = g.Key, Count = g.Count() });
```

#### 在LINQ查询语句中使用变量

上述的分组查询语句，中间多次调用了`Count`方法，使用`let`子句可以改变这种方式。`let`允许在LINQ查询中定义变量。

```c#
var countries = from r in Formulal.GetChampions()
                group r by r.Country into g
                let count = g.Count()
                orderby count descending, g.Key
                where count >= 2
                select new
                {
                    Country = g.Key,
                    Count = count
                };
```

使用方法语法时，为了避免`Count`方法被调用多次，可以使用`Select`方法创建一个匿名类型，将`Count`方法的结果作为匿名类的属性进行传递：

```c#
var countries2 = Formulal.GetChampions()
    .GroupBy(r => r.Country)
    .Select(g => new { Group = g, Count = g.Count() })
    .OrderByDescending(g => g.Count)
    .ThenBy(g => g.Group.Key)
    .Where(g => g.Count >= 2)
    .Select(g => new
    {
        Country = g.Group.Key,
        Count = g.Count
    });
```

注意：应考虑根据` let`子句或`Select`方法创建的临时对象的数量 。查询大列表时，创建的大量对象需要以后进行垃圾收集 ，这可能对性能产生巨大影响。

#### 对嵌套的对象分组

如果分组的对象应包含嵌套的序列，就可以改变`select`子句创建的匿名类型。

```c#
var countries = from r in Formulal.GetChampions()
                group r by r.Country into g
                let count = g.Count()
                orderby count descending, g.Key
                where count >= 2
                select new
                {
                    Country = g.Key,
                    Count = count,
                    //使用内部子句嵌套
                    Racers = from r1 in g
                             orderby r1.LastName
                             select r1.FirstName + " " + r1.LastName
                };
foreach (var item in countries)
{
    Console.WriteLine($"{item.Country,-10} {item.Count}");
    foreach (var name in item.Racers)
    {
        Console.Write(name + ";");
    }
    Console.WriteLine();
}
```

#### 内联接

使用`join`子句可以根据特定的条件合并两个数据源，需要指定两个要联接的列表。

```c#
var racers = from r in Formulal.GetChampions()
             from y in r.Years
             select new
             {
                 Year = y,
                 Name = r.FirstName + " " + r.LastName
             };
var teams = from t in Formulal.GetContructorChampions()
            from y in t.Years
            select new
            {
                Year = y,
                Name = t.Name
            };

var racersAndTeams = (from r in racers
                      join t in teams on r.Year equals t.Year
                      orderby t.Year
                      select new
                      {
                          r.Year,
                          Champion = r.Name,
                          Constructor = t.Name
                      }).Take(10);
```

可以将上述的两个语句合成一个LINQ查询语句 ：

```c#
var racersAndTeams2 =
     (from r in from r1 in Formulal.GetChampions()
                from yr in r1.Years
                select new
                {
                    Year = yr,
                    Name = r1.FirstName + " " + r1.LastName
                }
      join t in
      from t1 in Formulal.GetContructorChampions()
      from yt in t1.Years
      select new
      {
          Year = yt,
          Name = t1.Name
      }
      on r.Year equals t.Year
      orderby t.Year
      select new
      {
          Year = r.Year,
          Champion = r.Name,
          Constructor = t.Name
      }).Take(10);

Console.WriteLine("输出结果：");
foreach (var item in racersAndTeams2)
{
    Console.WriteLine($"{item.Year}:{item.Champion,-20} {item.Constructor}");
}
```

#### 左外联接

> 左外部联接是这样定义的：返回第一个集合的每个元素，无论该元素在第二个集合中是否有任何相关元素。 可以使用 LINQ 通过对分组联接的结果调用 [`DefaultIfEmpty`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.defaultifempty) 方法来执行左外部联接。 

在左外部联接中，将返回左侧源序列中的所有元素，即使右侧序列中没有其匹配元素也是如此。 若要在 LINQ 中执行左外部联接，请结合使用 `DefaultIfEmpty` 方法与分组联接，指定要在某个左侧元素不具有匹配元素时生成的默认右侧元素。 可以使用 `null` 作为任何引用类型的默认值，也可以指定用户定义的默认类型。  

```c#
var racers = from r in Formulal.GetChampions()
             from y in r.Years
             select new
             {
                 Year = y,
                 Name = r.FirstName + " " + r.LastName
             };
var teams = from t in Formulal.GetContructorChampions()
            from y in t.Years
            select new
            {
                Year = y,
                Name = t.Name
            };

var racersAndTeams = (from r in racers
                      join t in teams on r.Year equals t.Year into rt
                      from t in rt.DefaultIfEmpty()
                      orderby r.Year
                      select new
                      {
                          r.Year,
                          Champion = r.Name,
                          Constructor = t == null ? "no constructor" : t.Name
                      }).Take(10);
Console.WriteLine("输出结果：");
foreach (var item in racersAndTeams)
{
    Console.WriteLine($"{item.Year}:{item.Champion,-10} {item.Constructor}");
}
```

#### 组联接

含有 `into` 表达式的 `join` 子句称为分组联接。 

使用组联接时，可以联接两个独立的序列，对于其中一个序列中的某个元素，另一个序列中存在对应的一个项列表。

> 分组联接会生成分层的结果序列，该序列将左侧源序列中的元素与右侧源序列中的一个或多个匹配元素相关联。 分组联接没有等效的关系术语；它本质上是一个对象数组序列。
>
> 如果在右侧源序列中找不到与左侧源中的元素相匹配的元素，则 `join` 子句会为该项生成一个空数组。 因此，分组联接基本上仍然是一种内部同等联接，区别在于分组联接将结果序列组织为多个组。
>
> 如果只选择分组联接的结果，则可访问各项，但无法识别结果所匹配的项。 因此，通常更为有用的做法是：选择分组联接的结果并将其放入一个也包含该项名的新类型中。

```c#
var q = (from r in Formulal.GetChampions()
         join r2 in racers on
         new
         {
             FirstName = r.FirstName,
             LastName = r.LastName
         }
         equals
         new
         {
             r2.FirstName,
             r2.LastName
         }
         into yearResults
         select new
         {
             r.FirstName,
             r.LastName,
             r.Wins,
             r.Starts,
             Results = yearResults
         });

foreach(var r in q)
{
    Console.WriteLine(r.FirstName+" "+r.LastName);
    foreach(var results in r.Results)
    {
        Console.WriteLine(results.Year +" "+results.Positon);
    }
}
```

使用扩展方法可以调用[`SelectMany()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.selectmany?view=netframework-4.7.2)方法，该方法可以将序列的每个元素映射到`IEnumerable<T>`，并将生成的序列展平为一个序列。

```c#
var racers = Formulal.GetChampionships()
    .SelectMany(cs => new List<RacerInfo>()
    {
        new RacerInfo
        {
            Year=cs.Year,
            Positon=1,
            FirstName=cs.First.FirstName(),
            LastName=cs.First.LastName()
        },
        new RacerInfo
        {
            Year=cs.Year,
            Positon=2,
            FirstName=cs.Second.FirstName(),
            LastName=cs.Second.LastName()
        },
        new RacerInfo
        {
            Year=cs.Year,
            Positon=3,
            FirstName=cs.Third.FirstName(),
            LastName=cs.Third.LastName()
        }
    });

foreach(var r in racers)
{
    Console.WriteLine(r.FirstName+" "+r.LastName);
}  
```

#### 集合操作

- [`Intersect()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.intersect?view=netframework-4.7.2)：通过使用的默认相等比较器对值进行比较，生成两个序列的交集。 
- [`Distinct()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.distinct?view=netframework-4.7.2)：返回序列中通过使用指定的非重复元素。
- [`Union()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.Union?view=netframework-4.7.2)：通过使用默认的相等比较器生成的两个序列的并集。
- [`Except()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.Except?view=netframework-4.7.2)：通过使用默认的相等比较器对值进行比较，生成两个序列的差集。

```c#
Func<string, IEnumerable<Racer>> racersByCar =
    car => from r in Formulal.GetChampions()
           from c in r.Cars
           where c == car
           orderby r.LastName
           select r;

Console.WriteLine("调用Intersect()方法，显示结果：");
foreach(var racer in racersByCar("Ferrari").Intersect(racersByCar("Lotus")))
{
    Console.WriteLine(racer);
}
```

#### 合并（Zip）

[`Zip()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.zip?view=netframework-4.7.2)将指定的函数应用于两个序列的相应元素，生成一系列结果。 

```c#
var racernames = from r in Formulal.GetChampions()
                 where r.Country == "Italy"
                 orderby r.Wins descending
                 select new
                 {
                     Name = r.FirstName + " " + r.LastName
                 };
var racerNamesAndStarts = from r in Formulal.GetChampions()
                          where r.Country == "Italy"
                          orderby r.Wins descending
                          select new
                          {
                              LastName = r.LastName,
                              Starts = r.Starts
                          };
//第一个集合中的第一项会与第二个集合中的第一项合并
//第一个集合中的第二项会与第二个集合中的第二项合并，依次类推
//如果两个序列的项数不同，Zip()方法就在到达较小集合的末尾时停止
var racers = racernames.Zip(racerNamesAndStarts
    , (first, second) => first.Name + ", starts: " + second.Starts);
```

#### 分区（Take和TakeWhile，Skip和SkipWhile）

- [`Take()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.take?view=netframework-4.7.2)：从序列的开头返回指定的数量的连续元素。
- [`TakeWhile()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.takewhile?view=netframework-4.7.2)：只要指定的条件为`true`，就返回序列中的元素，然后跳过其余元素。 
- [`Skip()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.skip?view=netframework-4.7.2)：跳过指定的数量的序列中的元素，然后返回剩余元素。
- [`SkipWhile()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.skipwhile?view=netframework-4.7.2)：只要指定的条件为真，就会跳过序列中的元素，然后返回剩余的元素。 

```c#
int pageSize = 5;
int numberPages = (int)Math.Ceiling(Formulal.GetChampions().Count() / (double)pageSize);

for (int page = 0; page < numberPages; page++)
{
    Console.WriteLine("Page "+page);

    var racers = (from r in Formulal.GetChampions()
                  orderby r.LastName, r.FirstName
                  select r.FirstName + " " + r.LastName)
                .Skip(page * pageSize).Take(pageSize);

    foreach(var name in racers)
    {
        Console.WriteLine(name);
    }
    Console.WriteLine(  );
}
```

#### 聚合操作符

聚合操作符（如`Count`、`Sum`、`Min`、`Max`、`Average`和`Aggregate`操作符）不返回一个序列，而返回一个值。

- [`Count()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.count?view=netframework-4.7.2)：返回序列中的元素数。 
- [`Sum()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.sum?view=netframework-4.7.2)：计算一系列数值的总和。 
- [`Min()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.min?view=netframework-4.7.2)：返回值序列中的最小值。 
- [`Max()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.max?view=netframework-4.7.2)：返回值序列中的最大值。 
- [`Average()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.average?view=netframework-4.7.2)：计算一系列数值的平均值。 
- [`Aggregate()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.aggregate?view=netframework-4.7.2)： 对一个序列应用累加器函数。

```c#
Console.WriteLine("Count():");
var query = from r in Formulal.GetChampions()
            let numberYears = r.Years.Count()
            where numberYears >= 3
            orderby numberYears descending, r.LastName
            select new
            {
                Name = r.FirstName + " " + r.LastName,
                TimesChampion = numberYears
            };

foreach(var r in query)
{
    Console.WriteLine(r.Name+" "+r.TimesChampion);
}

Console.WriteLine();
Console.WriteLine("Sum():");

var countries = (from c in from r in Formulal.GetChampions()
                           group r by r.Country into c
                           select new
                           {
                               Country = c.Key,
                               Wins = (from r1 in c select r1.Wins).Sum()
                           }
                 orderby c.Wins descending, c.Country
                 select c).Take(5);
foreach(var country in countries)
{
    Console.WriteLine(country.Country+" "+country.Wins);
}
```

#### 转换操作符（ToList、ToLookup、Cast）

在之前的内容曾提到，查询可以推迟到访问数据项时再执行。在迭代中使用查询时，查询会执行。而使用转换操作符会立即执行查询，把查询结果放在数组、列表或字典中。

- [ToList()](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.tolist?view=netframework-4.7.2)：从`IEnumerable<T>`创建`List<T>`。
- [ToLookup()](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.tolookup?view=netframework-4.7.2)：从`IEnumerable <T>`创建一个通用的`Lookup <TKek,TElement >`。注意：`Dictionary<TKey,TValue>`类只支持一个键对应一个值。类`Lookup<TKey,TElement>`中，一个键可以对应多个值。
- [`Cast()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.cast?view=netframework-4.7.2)：将`IEnumerable`的元素转换为指定的类型。

```c#
List<Racer> racers = (from r in Formulal.GetChampions()
                      where r.Starts > 150
                      orderby r.Starts descending
                      select r).ToList();
//ToLookup
var racers2 = (from r in Formulal.GetChampions()
              from c in r.Cars
              select new
              {
                  Car = c,
                  Racer = r
              })
              .ToLookup(cr => cr.Car, cr => cr.Racer);
if (racers2.Contains("Williams"))
{
    foreach (var winll in racers2["Williams"])
    {
        Console.WriteLine(winll);
    }
}
```

如果需要在非类型化的集合上（如`ArrayList`）使用LINQ查询，就可以使用`Cast()`方法。

```c#
var list = new System.Collections.ArrayList(Formulal.GetChampions()
    as System.Collections.ICollection);
//基于Object类型的ArrayList集合用Racer对象填充
var query = from r in list.Cast<Racer>()
            where r.Country == "China"
            orderby r.Wins descending
            select r;
```

#### 生成操作符（Range、Empty、Repeat）

生成操作符`Range()`、`Empty()`和`Repeat()`不是扩展方法，而是返回序列的正常静态方法。在LINQ to Objects中，这些方法可以用于`Enumerable`类。

- [`Range()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.range?view=netframework-4.7.2)：生成指定范围内的整数序列。 注意：该方法不返回填充了所定义值的集合，这个方法与其他方法一样，也推迟执行查询。
- [`Empty()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.empty?view=netframework-4.7.2)：返回具有指定类型参数的空`IEnumerable <T>`。它可以用于需要一个集合的参数，其中可以给参数传递空集合。
- [`Repeat()`](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable.repeat?view=netframework-4.7.2)：生成包含一个重复值的序列。 

```c#
var values = Enumerable.Range(1, 20);
foreach (var item in values)
{
    Console.WriteLine($"{item}");
}
```



## 并行LINQ（Parallel LINQ，PLINQ）

`System.Linq`命名空间中包含的类`ParallelEnumerable`可以分解查询的工作，使其分布在多个线程上。

`ParallelEnumerable`类的大多数扩展方法是基于`ParallelQuery<TSource>`类的扩展。在`ParallelEnumerable`类中，存在一个重要的方法`AsParallel()`，该方法扩展了`IEnumerable<TSource>`接口，所以正常的集合类都可以调用该方法以并行方式查询。

```c#
public static ParallelQuery<TSource> AsParallel<TSource>(this IEnumerable<TSource> source);
public static ParallelQuery<TSource> AsParallel<TSource>(this Partitioner<TSource> source);
public static ParallelQuery AsParallel(this IEnumerable source);
```

一般使用并行LINQ，需要一个大型集合，对于可以放在CPU的缓存中的小集合，并行LINQ看不出效果。

```c#
public static IEnumerable<int> SampleData()
{
    const int arraySize = 50000000;
    var r = new Random();
    //连续50000000次随机取出小于140的数字
    return Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();
}
var data = SampleData();
//查询表达式写法
var res = (from x in data.AsParallel() where Math.Log(x) < 4 select x).Average();
//扩展方法写法
var res2 = data.AsParallel().Where(x => Math.Log(x) < 4).Select(x => x).Average();
```

上述`AsParallel()`方法返回`ParallelQuery<TSource>`，之后的`Where`、`Select`、`Average`都是来自于`ParallelEnumerable`类。对于`ParallelEnumerable`类，查询是分区的，以便多个线程可以同时处理该查询。集合可以分为多个部分，其中每个部分由不同的线程处理，以筛选其余项。完成分区的工作后，就需要合并，获得所有部分的总和。

#### 分区器

`AsParallel()`方法不仅扩展了`IEnumerable<TSource>`接口，还扩展了`Partitioner<TSource>`类。

`Partitioner`类用于为数组，列表和枚举提供通用的分区策略。 该类只提供了`Create()`方法，`Create()`方法有很多的变体，返回的是`OrderablePartitioner<TSource>`，而`OrderablePartitioner<TSource>`继承自`Partitioner<TSource>`，所以通过调用`Partitioner.Create()`可以影响要创建的分区。

`Partitioner.Create()`重载方法如下：

```c#
public static OrderablePartitioner<TSource> Create<TSource>(IList<TSource> list, bool loadBalance);
public static OrderablePartitioner<TSource> Create<TSource>(TSource[] array, bool loadBalance);
public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source);
public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions);
public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive);
public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive, long rangeSize);
public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive);
public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive, int rangeSize);
```

> `Create()`方法接受实现了`ILIst<T>`类的数组或对象。根据这一点，以及Boolean类型的参数`loadBalance`和该方法的一些重载版本，会返回一个不同的`Partitioner`类型（`Partitioner<TSource>`）。对于数组，使用派生自抽象基类`OrderablePartitioner<TSource>`的`DynamicPartitionerForArray<TSource>`类和`StaticPartitionerForArray<TSource>`类。

```c#
var result= (from x in Partitioner.Create((List<int>)data, true)
             .AsParallel() where Math.Log(x) < 4 select x)
             .Average();
```

> 也可以调用`WithExecutionMode()`和`WithDegreeOfParallelism()`方法，来影响并行机制。对于`WithExecutionMode()`方法可以传递`ParallelExecutionMode`的一个`Default`值或者`ForceParallelism`值。默认情况下，并行LINQ避免使用系统开销很高的并行机制。对于`WithDegreeOfParallelism()`方法，可以传递一个整数值，以指定应并行运行的最大任务数。查询不应使用全部CPU，这个方法会很有用。

#### 取消

.NET提供了一中标准方式，来取消长时间运行的任务。这也适用于并行LINQ。

要取消长时间运行的查询，可以给查询添加`WithCancellation()`方法，并传递一个`CancellationToken`令牌作为参数。`CancellationToken`令牌从`CancellationTokenSource`类中创建。该查询在单独的线程中运行，在该线程中，捕获一个`OperationCanceledException`类型的异常。如果取消了查询，就触发这个异常。在主线程中，调用`CancellationTokenSource`类的`Cancel()`方法可以取消任务。

```c#
var cts = new CancellationTokenSource();
var data = SampleData();
   Task.Run(()=> {
    try
    {
        var res = (from x in data.AsParallel().WithCancellation(cts.Token)
                   where Math.Log(x) < 4
                   select x).Average();
        Console.WriteLine(res);
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine(ex.Message);
    }

});

Console.WriteLine("取消吗?");
string input = Console.ReadLine();
if (input.ToLower().Equals("y"))
{
    cts.Cancel();
}
```









------



#### 参考资源

- [C# 查询关键字](https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/query-keywords)
- [C# 语言集成查询 (LINQ)](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/index)
- [C# LINQ查询表达式](https://docs.microsoft.com/zh-cn/dotnet/csharp/linq/index)
- [C# join子句](https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/join-clause)
- [C# 执行左外部联接](https://docs.microsoft.com/zh-cn/dotnet/csharp/linq/perform-left-outer-joins)



本文后续会随着知识的积累不断补充和更新，内容如有错误，欢迎指正。

本文最后一次更新时间：2018-07-17

------

