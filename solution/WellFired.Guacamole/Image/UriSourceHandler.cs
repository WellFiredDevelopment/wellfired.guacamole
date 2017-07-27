using System;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;

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

            try
            {
                await TaskEx.Run(() =>
                {
                    var httpRequest = (HttpWebRequest) WebRequest.Create(_uri);
                    httpRequest.Timeout = 10000;
                    httpRequest.UserAgent = "GuacamoleUserApplication";
                    webResponse = (HttpWebResponse) httpRequest.GetResponse();
                }, cancellationToken);
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

            return new ImageSourceWrapper(webResponse.GetResponseStream(), ImageType.Image);
        }

        public override string ToString()
        {
            return $"{_uri}";
        }
    }
}