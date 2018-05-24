using System;
using System.Threading.Tasks;

namespace WellFired.Guacamole.Image
{
    public interface IImageSource
    {
        Task<LoadedImage> Load();
        void Cancel();
        Action<LoadedImage> OnComplete { get; set; }
    }
}