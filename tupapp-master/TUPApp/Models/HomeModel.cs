namespace TUPApp.Models
{
    public class HomeModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Gender { get; set; }

        public List<LinksObj> Links { get; set; }
    }

    public class LinksObj
    {
        public string LinkName { get; set; }

        public string Url { get; set; }
    }
}
