# Prism - Dialog



### Dialog

使用Dialog可以实现对话框模式的窗体交互。可以传递值。

- IDialogAware
- DialogParameters

App.xaml.cs：

```csharp
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    //注册对话
    containerRegistry.RegisterDialog<MsgView, MsgViewModel>("myDialog");//设置别名为myDialog，如果不设置默认为窗体名称msgView
}
```

MsgViewModel.cs：

```csharp
public class MsgViewModel : BindableBase, IDialogAware
{
    public DelegateCommand OkCommand { get; }
    public DelegateCommand CancelCommand { get; }
    private string title;

    public string Title
    {
        get { return title; }
        set { title = value; SetProperty(ref title, value); }
    }

    public MsgViewModel()
    {
        OkCommand = new DelegateCommand(() =>
        {
            var param = new DialogParameters();
            param.Add("text", this.Title);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, param));
        });

        CancelCommand = new DelegateCommand(() =>
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        });
    }

    public event Action<IDialogResult> RequestClose;

    public bool CanCloseDialog()
    {
        return true;
    }

    public void OnDialogClosed()
    {
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        this.Title = parameters.GetValue<string>("p1");
    }
}
```

MainWindowViewModel.cs：

```csharp
public DelegateCommand DialogCommand { get; }

public MainWindowViewModel(IRegionManager regionManager, IDialogService dialog)
{
    this.regionManager = regionManager;
    this.dialog = dialog;
  
    DialogCommand = new DelegateCommand(() =>
    {
        DialogParameters param = new DialogParameters();
        param.Add("p1", "好好学习，天天向上");
        dialog.ShowDialog("myDialog", param, (arg) =>
        {
            if (arg.Result == ButtonResult.OK)
            {
                var text = arg.Parameters.GetValue<string>("text");
                MessageBox.Show(text);
            }
            else if(arg.Result == ButtonResult.Cancel)
            {
                MessageBox.Show("关闭");
            }
        });
    });
}
```

