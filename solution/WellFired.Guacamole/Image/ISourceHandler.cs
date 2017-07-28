using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WellFired.Guacamole.Image
{
    public interface ISourceHandler
    {
        Task<IImageSourceWrapper> Handle(CancellationToken cancellationToken);
    }
}