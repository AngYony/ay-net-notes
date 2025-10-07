using AY.LearningTag.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.ApplicationServices
{
    public abstract record rdemo(string Name, string Text)
    {
        public Chapter Chapter { get; init; } = new Chapter();
    }
}