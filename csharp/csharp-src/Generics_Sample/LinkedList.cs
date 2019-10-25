using System.Collections;
using System.Collections.Generic;

namespace Generics_Sample
{
    public class LinkedList : IEnumerable
    {
        public LinkedListNode First { get; private set; }
        public LinkedListNode Last { get; private set; }

        //在链表尾部添加一个新元素
        public LinkedListNode AddLast(object node)
        {
            var newNode = new LinkedListNode(node);
            if (First == null)
            {
                First = newNode;
                Last = First;
            }
            else
            {
                LinkedListNode previous = Last;
                Last.Next = newNode;
                Last = newNode;
                Last.Prev = previous;
            }
            return newNode;
        }

        //实现GetEnumerator()方法
        public IEnumerator GetEnumerator()
        {
            LinkedListNode current = First;
            while (current != null)
            {
                //使用yield语句创建一个枚举器类型
                yield return current.Value;
                current = current.Next;
            }
        }
    }

    public class LinkedList<T> : IEnumerable<T>
    {
        public LinkedListNode<T> First { get; private set; }
        public LinkedListNode<T> Last { get; private set; }

        public LinkedListNode<T> AddLast(T node)
        {
            var newNode = new LinkedListNode<T>(node);
            if (First == null)
            {
                First = newNode;
                Last = First;
            }
            else
            {
                LinkedListNode<T> previous = Last;
                Last.Next = newNode;
                Last = newNode;
                Last.Prev = previous;
            }
            return newNode;
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}