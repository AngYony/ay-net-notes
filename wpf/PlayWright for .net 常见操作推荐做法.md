# PlayWright for .net 常见操作推荐做法



不适用等待多个元素返回的函数：

- WaitForSelectorAsync(".item")：只要其中一个元素出现就会返回，不适用于多个元素的查找。
- Locator(".item").WaitForAsync()：只要其中一个元素出现就会返回，不适用于多个元素的查找。
- page.WaitForLoadStateAsync(LoadState.NetworkIdle)：



将js对象转换为PlayWright对象：

```
  var handle = await page1.EvaluateHandleAsync("document.querySelector(\"div[role='textbox'][contenteditable='true']\")");
  var inputBox = handle.AsElement();
```

