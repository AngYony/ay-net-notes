using System;
using System.Collections.Generic;

namespace Generics_Sample
{
    public class DocumentManager<TDocument> where TDocument:IDocument
    {
        private readonly Queue<TDocument> documentQueue = new Queue<TDocument>();

        public bool IsDocumentAvailable => documentQueue.Count > 0;

        public void AddDocument(TDocument doc)
        {
            lock (this)
            {
                documentQueue.Enqueue(doc);
            }
        }

        public TDocument GetDocument()
        {
            TDocument doc = default(TDocument); //default将泛型类型的值初始化为null或者0，取决于泛型类型是引用类型还是值类型。
            lock (this)
            {
                doc= documentQueue.Dequeue();
            }
            return doc;
        }
        public void DisplayAllDocuments()
        {
            foreach (TDocument doc in documentQueue)
            {
                Console.WriteLine(doc.Title);
            }
        }
    }
}