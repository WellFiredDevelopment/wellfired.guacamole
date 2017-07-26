using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WellFired.Guacamole.Image
{
    internal class UriSourceHandler : ISourceHandler
    {
        private readonly Uri _uri;

        public UriSourceHandler(Uri uri)
        {
            _uri = uri;
        }

        public async Task<IImageSourceWrapper> Handle(CancellationToken cancellationToken)
        {
            var webResponse = default(HttpWebResponse);

            await TaskEx.Run(() =>
            {
                var httpRequest = (HttpWebRequest) WebRequest.Create(_uri);
                httpRequest.Timeout = 10000;
                httpRequest.UserAgent = "GuacamoleUserApplication";
                webResponse = (HttpWebResponse) httpRequest.GetResponse(); 
            }, cancellationToken);
            
            return new ImageSourceWrapper(webResponse.GetResponseStream(), ImageType.Image);
        }
    }
}