using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WellFired.Guacamole.Image
{
    internal interface ISourceHandler
    {
        Task<IImageSourceWrapper> Handle(CancellationToken cancellationToken);
    }
}