using System.IO;
using System.Threading.Tasks;

namespace WellFired.Guacamole.FileSystem
{
    public class FileSystem : IFileSystem
    {
        public Task<Stream> GetStream(string path, FileMode mode)
        {
            return TaskEx.FromResult((Stream)new FileStream(path, mode));
        }
    }
}