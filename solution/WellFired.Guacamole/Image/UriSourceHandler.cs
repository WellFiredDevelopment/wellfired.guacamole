using System;
using System.Threading;
using System.Threading.Tasks;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.WebRequestHandler;

namespace WellFired.Guacamole.Image
{
    public class UriSourceHandler : ISourceHandler
    {
        private readonly Uri _uri;
        private readonly IWebRequestHandler _webRequestHandler;

        public UriSourceHandler(Uri uri, IWebRequestHandler webRequestHandler)
        {
            _uri = uri;
            _webRequestHandler = webRequestHandler;
        }

        public async Task<IImageSourceWrapper> Handle(CancellationToken cancellationToken)
        {
            return new ImageSourceWrapper(await _webRequestHandler.GetStream(_uri, cancellationToken), ImageType.Image);
        }

        public override string ToString()
        {
            return $"{_uri}";
        }
    }
}