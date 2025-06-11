using MyToDo.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    internal class MemoViewModel: BindableBase
    {
		public MemoViewModel()
		{
			this.MemoInfos = new ObservableCollection<MemoInfo>();
            foreach (var item in Enumerable.Range(1, 10))
            {
                this.MemoInfos.Add(new MemoInfo { Id = item, Title = "备忘" + item, Content = "备忘内容", Color =MyHelper. GetColor() });
            }
        }



		private ObservableCollection<MemoInfo>  memoInfos;

		public ObservableCollection<MemoInfo> MemoInfos
        {
			get { return memoInfos; }
			set { memoInfos = value; }
		}


	}
}
