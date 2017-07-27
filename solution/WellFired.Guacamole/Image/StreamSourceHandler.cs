using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WellFired.Guacamole.Image
{
    internal class StreamSourceHandler : ISourceHandler
    {
        private readonly Stream _stream;

        public StreamSourceHandler(Stream stream)
        {
            _stream = stream;
        }

#pragma warning disable 1998
        public async Task<IImageSourceWrapper> Handle(CancellationToken cancellationToken)
#pragma warning restore 1998
        {
            return new ImageSourceWrapper(_stream, ImageType.Image);;
        }

        public override string ToString()
        {
            return $"Stream";
        }
    }
}