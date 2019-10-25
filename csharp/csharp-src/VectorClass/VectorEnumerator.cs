using System;
using System.Collections;
using System.Collections.Generic;
using WhatsNewAttributes;
namespace VectorClass
{
    [LastModified("2017-08-12", "内容一")]
    [LastModified("2017-08-13", "内容二")]
    internal class VectorEnumerator : IEnumerable<double>
    {
        public IEnumerator<double> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
