namespace Generics_Sample
{
    public class LinkedListNode
    {
        public LinkedListNode(object value)
        {
            Value = value;
        }

        public LinkedListNode Next { get; internal set; }
        public LinkedListNode Prev { get; internal set; }
        public object Value { get; private set; }
    }

    public class LinkedListNode<T>
    {
        public LinkedListNode(T value)
        {
            Value = value;
        }

        public LinkedListNode<T> Next { get; internal set; }
        public LinkedListNode<T> Prev { get; internal set; }
        public T Value { get; private set; }
    }
}