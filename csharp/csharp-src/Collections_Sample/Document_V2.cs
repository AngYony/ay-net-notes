using System;
using System.Collections.Generic;

namespace Collections_Sample
{
    public class Document_V2
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public byte Priority { get; private set; }

        public Document_V2(string title, string content, byte priority)
        {
            this.Title = title;
            this.Content = content;
            this.Priority = priority;
        }
    }

    public class PriorityDocumentManager
    {
        private readonly LinkedList<Document_V2> _documentList;

        private readonly List<LinkedListNode<Document_V2>> _priorityNodes;

        public PriorityDocumentManager()
        {
            _documentList = new LinkedList<Document_V2>();
            _priorityNodes = new List<LinkedListNode<Document_V2>>(10);
            for (int i = 0; i < 10; i++)
            {
                _priorityNodes.Add(new LinkedListNode<Document_V2>(null));
            }
        }

        public void AddDocument(Document_V2 d)
        {
            if (d == null) throw new ArgumentNullException("d");
            AddDocumentToPriorityNode(d, d.Priority);
        }

        private void AddDocumentToPriorityNode(Document_V2 doc, int priority)
        {
            if (priority > 9 || priority < 0)
            {
                throw new ArgumentException("等级必须为0~9");
            }
            if (_priorityNodes[priority].Value == null)
            {
                --priority;
                if (priority >= 0)
                {
                    AddDocumentToPriorityNode(doc, priority);
                }
                else
                {
                    _documentList.AddLast(doc);
                    _priorityNodes[doc.Priority] = _documentList.Last;
                }
                return;
            }
            else
            {
                LinkedListNode<Document_V2> prioNode = _priorityNodes[priority];
                if (priority == doc.Priority)
                {
                    _documentList.AddAfter(prioNode, doc);
                    _priorityNodes[doc.Priority] = prioNode.Next;
                }
                else
                {
                    LinkedListNode<Document_V2> firstPrioNode = prioNode;
                    while (firstPrioNode.Previous != null 
                        && firstPrioNode.Previous.Value.Priority == prioNode.Value.Priority)
                    {
                        firstPrioNode = prioNode.Previous;
                        prioNode = firstPrioNode;
                    }
                    _documentList.AddBefore(firstPrioNode, doc);
                    _priorityNodes[doc.Priority] = firstPrioNode.Previous;
                }
            }
        }

        public void DisplayAllNodes()
        {
            foreach (Document_V2 doc in _documentList)
            {
                Console.WriteLine($"priority:{doc.Priority},tilte:{doc.Title}");
            }
        }

        public Document_V2 GetDocument()
        {
            Document_V2 doc = _documentList.First.Value;
            _documentList.RemoveFirst();
            return doc;
        }
    }

    public class PriorityDocumentManager_Program
    {
        public static void Run()
        {
            var pdm = new PriorityDocumentManager();

            pdm.AddDocument(new Document_V2("one", "示例一", 8));
            pdm.AddDocument(new Document_V2("two", "示例二", 3));
            pdm.AddDocument(new Document_V2("three", "示例三", 4));
            pdm.AddDocument(new Document_V2("for", "示例四", 8));
            pdm.AddDocument(new Document_V2("five", "示例五", 1));
            pdm.AddDocument(new Document_V2("six", "示例六", 9));
            pdm.AddDocument(new Document_V2("seven", "示例七", 1));
            pdm.AddDocument(new Document_V2("eight", "示例八", 1));
            pdm.DisplayAllNodes();
        }
    }
}