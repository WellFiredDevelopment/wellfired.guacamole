using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Examples.CaseStudy.RedditBrowser.ViewModel
{
    public class SearchResult : ObservableBase
    {
        private string _subReddit;
        private ICommand _search;
        private IList<Post> _searchResults;

        public string SubReddit
        {
            get => _subReddit;
            set => SetProperty(ref _subReddit, value);
        }

        public ICommand Search
        {
            get => _search;
            set => SetProperty(ref _search, value);
        }

        public IList<Post> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
        }

        public SearchResult()
        {
            // Default this to something sensible
            SubReddit = "r/awww";
            
            // Our search command, calls reddits api.
            Search = new Command { 
                ExecuteAction = async () => {
                    try
                    {
                        Search.CanExecute = false;
                        var results = await DoSearch();
                        Device.ExecuteOnMainThread(() => SearchResults = results);
                    }
                    finally
                    {
                        Search.CanExecute = true;
                    }
                }
            };
            
            Search.Execute();
        }
        
        private async Task<IList<Post>> DoSearch()
        {
            var jsonResponse = string.Empty;
            await TaskEx.Run(() =>
            {
                var httpWebRequest = (HttpWebRequest) WebRequest.Create($"https://www.reddit.com/{SubReddit}/hot.json?sort=new");
                httpWebRequest.Method = WebRequestMethods.Http.Get;
                httpWebRequest.Accept = "application/json";

                var response = httpWebRequest.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                    jsonResponse = sr.ReadToEnd();
            });
            
            // The returned data from the web API is converted here into our view models.
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.SearchResult>(jsonResponse);
            return result.Data.Children.Select(o => new Post(o)).ToList();
        }
    }
}