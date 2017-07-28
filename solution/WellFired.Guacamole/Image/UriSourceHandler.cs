using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;
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
            Stream stream;
            try
            {
                stream = await _webRequestHandler.GetStream(_uri, cancellationToken);
            }
            catch (Exception)
            {
                Logger.LogWarning($"Failed to retrieve Image {_uri}");
                return await new ImageShapeDefinitionHandler(
                    new ImageShapeDefinition {
                        Shape = ImageShape.Circle,
                        Size = 64,
                        Color = UIColor.Burlywood,
                        OutlineColor = UIColor.BlueViolet
                    })
                    .Handle(cancellationToken);
            }

            return new ImageSourceWrapper(stream, ImageType.Image);
        }

        public override string ToString()
        {
            return $"{_uri}";
        }
    }
}