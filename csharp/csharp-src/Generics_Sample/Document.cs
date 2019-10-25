namespace Generics_Sample
{
    public class Document : IDocument
    {
        public Document(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }

        public string Content { get; set; }
        public string Title { get; set; }
    }
}



 