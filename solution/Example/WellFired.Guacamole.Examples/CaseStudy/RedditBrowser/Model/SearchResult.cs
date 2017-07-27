using System;

namespace WellFired.Guacamole.Examples.CaseStudy.RedditBrowser.Model
{
    /// <summary>
    /// This is the data structure that reddits API returns.
    /// A search result has a listing, which has a series of children.
    /// </summary>
    public class SearchResult
    {
        public Listing Data { get; set; }
    }

    public class Listing
    {
        public Child [] Children { get; set; }
    }

    public class Child
    {
        public ChildData Data { get; set; }
    }

    public class ChildData
    {
        public string Author { get; set; }
        public Uri Thumbnail { get; set; }
        public string Title { get; set; }
    }
}