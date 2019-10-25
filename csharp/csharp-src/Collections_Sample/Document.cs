using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collections_Sample
{
    
    //public class Document
    //{
    //    public string Title { get; }
    //    public string Content { get; }

    //    public Document(string title, string content)
    //    {
    //        this.Title = title;
    //        this.Content = content;
    //    }
    //}
  
    //public class DocumentManager
    //{
    //    private readonly Queue<Document> _documentQueue = new Queue<Document>();

    //    public void AddDocument(Document doc)
    //    {
    //        lock (this)
    //        {
    //            _documentQueue.Enqueue(doc);
    //        }
    //    }

    //    public Document GetDocument()
    //    {
    //        Document doc = null;
    //        lock (this)
    //        {
    //            doc = _documentQueue.Dequeue();
    //        }
    //        return doc;
    //    }

    //    public bool IsDocumentAvailable => _documentQueue.Count > 0;
    //}
    //[Obsolete("暂时不用", true)]
    //public class ProcessDocuments
    //{
    //    private DocumentManager _documentManager;

    //    protected ProcessDocuments(DocumentManager dm)
    //    {
    //        _documentManager = dm ?? throw new ArgumentNullException(nameof(dm));
    //    }

    //    protected async Task Run()
    //    {
    //        while (true)
    //        {
    //            if (_documentManager.IsDocumentAvailable)
    //            {
    //                Document doc = _documentManager.GetDocument();
    //                Console.WriteLine(doc.Title + ":" + doc.Content);
    //            }
    //            await Task.Delay(new Random().Next(1000));
    //        }
    //    }

    //    public static void Start(DocumentManager dm)
    //    {
    //        Task.Run(new ProcessDocuments(dm).Run);
    //    }
    //}
    
    //public class ProcessDocuments_Program
    //{
    //    public static void Run()
    //    {
    //        var dm = new DocumentManager();
    //        ProcessDocuments.Start(dm);

    //        for (int i = 0; i < 1000; i++)
    //        {
    //            var doc = new Document("Doc_" + i, "Content_" + i);
    //            dm.AddDocument(doc);
    //            Console.WriteLine("添加了document:Doc_" + i);
    //            System.Threading.Thread.Sleep(new Random().Next(1000));
    //        }
    //    }
    //}
}