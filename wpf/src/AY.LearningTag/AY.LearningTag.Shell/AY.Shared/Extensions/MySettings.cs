using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.Shared.Extensions
{
    public sealed class MySettings
    {
        public required int KeyOne { get; set; }
        public required bool KeyTwo { get; set; }
        public required NestedSettings KeyThree { get; set; } = null!;
    }


    public sealed class NestedSettings
    {
        public required string Message { get; set; } = null!;
    }

}
