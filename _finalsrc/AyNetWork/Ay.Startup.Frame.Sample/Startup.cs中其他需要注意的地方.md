# Startup.cs中其他需要注意的地方



#### 必须调用SetCompatibilityVersion()方法

```c#
services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
```

上述语句中的.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)必不可少，缺少该语句会造成不可预估的影响。例如，在添加Razor类库时，如果少了上述语句，Razor类库中的文件就不能正确的处理。



