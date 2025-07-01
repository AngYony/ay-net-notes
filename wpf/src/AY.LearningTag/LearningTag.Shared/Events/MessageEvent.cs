using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.Shared.Events
{
    public class MessageEvent : PubSubEvent<MassgeInfo>
    {
    }
    
    public class MassgeInfo
    {
        public PubSubEventMessageType MessageType { get; set; }
    }

    public enum PubSubEventMessageType
    {
        SaveSettings
    }
}
