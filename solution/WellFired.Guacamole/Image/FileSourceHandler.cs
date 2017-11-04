using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.FileSystem;

namespace WellFired.Guacamole.Image
{
    public class FileSourceHandler : ISourceHandler
    {
        private readonly string _location;
        private readonly IFileSystem _fileSystem;

        public FileSourceHandler(string location, IFileSystem fileSystem)
        {
            _location = location;
            _fileSystem = fileSystem;
        }

        public async Task<IImageSourceWrapper> Handle(CancellationToken cancellationToken)
        {
            try
            {
                var stream = await _fileSystem.GetStream(_location, FileMode.Open, cancellationToken);
                return new ImageSourceWrapper(stream, ImageType.Image);
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