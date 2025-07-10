using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.Setting
{
    public interface ISettingApplicationCommands
    {
        CompositeCommand SaveAllCompositeCommand { get; }
    }

    public class SettingApplicationCommands : ISettingApplicationCommands
    {
        public CompositeCommand SaveAllCompositeCommand { get; } = new CompositeCommand();
    }
}