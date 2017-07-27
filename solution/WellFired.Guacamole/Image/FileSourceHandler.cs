using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WellFired.Guacamole.Exceptions;

namespace WellFired.Guacamole.Image
{
    internal class FileSourceHandler : ISourceHandler
    {
        private readonly string _location;
        private readonly string _adjustedPath;

        public FileSourceHandler(string location)
        {
            _location = location;
            _adjustedPath = Device.AdjustPath(_location);
        }

#pragma warning disable 1998
        public async Task<IImageSourceWrapper> Handle(CancellationToken cancellationToken)
#pragma warning restore 1998
        {
            try
            {
                return new ImageSourceWrapper(new FileStream(_adjustedPath, FileMode.Open), ImageType.Image);
            }
            catch (FileNotFoundException)
            {
                throw new ImageSourceCouldntFindFileException(_location);
            }
            catch (DirectoryNotFoundException)
            {
                throw new ImageSourceCouldntFindFileException(_location);
            }
            catch (UnauthorizedAccessException)
            {
                throw new ImageSourceDoesntHaveAccessException(_location);
            }
        }

        public override string ToString()
        {
            return $"{_location}";
        }
    }
}