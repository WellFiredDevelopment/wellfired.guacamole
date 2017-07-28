using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WellFired.Guacamole.WebRequestHandler
{
    public interface IWebRequestHandler
    {
        Task<Stream> GetStream(Uri uri, CancellationToken cancellationToken);
    }
}