using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsNewAttributes;

//此处的作用见文件：\LookUpWhatsNew\Program.cs中的注释描述
[assembly: SupportsWhatsNew]
namespace VectorClass
{

    [LastModified("2018-08-20", "描述一")]
    [LastModified("2018-08-19", "描述二")]
    [LastModified("2018-08-18", "描述三")]
    public class Vector : IFormattable, IEnumerable<double>
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public Vector(double x,double y ,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector(Vector vector) : this(vector.X, vector.Y, vector.Z)
        {

        }
        public IEnumerator<double> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
