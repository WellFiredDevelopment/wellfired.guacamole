using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WellFired.Guacamole.WebRequestHandler
{
    public class WebRequestHandler : IWebRequestHandler
    {
        public async Task<Stream> GetStream(Uri uri, CancellationToken cancellationToken)
        {
            HttpWebResponse response = null;
            await TaskEx.Run(() =>
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpRequest.Timeout = 10000;
                httpRequest.UserAgent = "GuacamoleUserApplication";
                response = (HttpWebResponse) httpRequest.GetResponse();
            }, cancellationToken);
            return response.GetResponseStream();
        }
    }
}