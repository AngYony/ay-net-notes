namespace Generics_Sample
{
    public abstract class Calc<T>
    {
        public abstract T Add(T x, T y);
        public abstract T Sub(T x, T y);
    }

    public class IntCalc : Calc<int>
    {
        public override int Add(int x, int y) => x + y;
        public override int Sub(int x, int y) => x - y;
    }
}