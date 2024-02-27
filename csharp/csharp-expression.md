# C# 表达式树（Expression）



[1. 表达式树基础 · C# 表达式树教程大全 - 痴者工良 (whuanle.cn)](https://ex.whuanle.cn/1.base.html)





## 表达式树（`Expression<T>`）

如果一个方法的参数是`Expression<T>`类型，当把lambda表达式赋予这个参数时，C#编译器就会从lambda表达式中创建一个表达式树，并存储在程序集中，这样，就可以在运行期间分析表达式树，并进行优化，以便于查询数据源。表达式树从派生自抽象基类`Expression`的类中构建。`Expression`类与`Expression<T>`不同。继承自`Expression`类的表达式有`BinaryExpression`、`ConstantExpression`、`InvocationExpression`、`LambdaExpression`、`NewExpression`、`NewArrayExpression`、`TernaryExpression`以及`UnaryExpression`等。编译器会从Lambda表达式中创建表达式树。



