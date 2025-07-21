namespace StackTutorial.Models
{
    public class CommonClass
    {
        public class CommonModel
        {
            public long TutorialID { get; set; }
            public long CategoryID { get; set; }
            public int ContentCount { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string Category { get; set; }
            public bool ShowBreadcrumbs { get; set; } = true;
        }
    }
}
