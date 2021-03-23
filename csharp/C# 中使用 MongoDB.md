# C# 中使用 MongoDB

1、安装Nuget包：MongoDB.Driver





## MongoDB 的连接字符串

方式一：无用户名和密码

```csharp
var client=new MongoClient("mongodb://localhost:27017");
```

如果实例化MongoClient对象时，不指定任何的参数，默认为：127.0.0.1:27017

方式二：指定用户名和密码

```csharp
var client=new MongoClient("mongodb://admin:password@localhost:27017");
```

方式三：连接到一个副本集，客户端服务发现

```csharp
var client=new MongoClient("mongodb://localhost:27017,localhost:28018,localhost:27019");
```



## 获取数据库和 Collection

```csharp
private readonly IMongoCollection<Question> _questionColleciton;
private readonly IMongoCollection<Vote> _voteCollection;
private readonly IMongoCollection<Answer> _answerCollection;

public QuestionController(IMongoClient mongoClient)
{
    var database = mongoClient.GetDatabase("lighter");
    _questionColleciton = database.GetCollection<Question>("questions");
    _voteCollection = database.GetCollection<Vote>("votes");
    _answerCollection = database.GetCollection<Answer>("answers");
}
```

mongo不需要关注数据库的结构，即使mongo中不存在数据库lighter，运行上述的代码也不会抛出异常，而是会自动创建不存在的数据库和collection，因此在指定数据库名称和Collection名称时，要特别注意名称不要写错了。





 



