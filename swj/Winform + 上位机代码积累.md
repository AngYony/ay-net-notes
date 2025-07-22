# Winform + 上位机代码积累

### 多线程下的UI操作

```csharp
/// <summary>
/// 添加日志
/// </summary>
/// <param name="level"></param>
/// <param name="info"></param>
private void AddLog(int level, string info)
{
    if (this.lst_Info.InvokeRequired)
    {
        //委托
        this.lst_Info.Invoke(new Action<int, string>(AddLog), level, info);
    }
    else
    {
        ListViewItem listViewItem = new ListViewItem("  " + CurrentTime, level);
        listViewItem.SubItems.Add(info);
        this.lst_Info.Items.Add(listViewItem);
    }
}

```

