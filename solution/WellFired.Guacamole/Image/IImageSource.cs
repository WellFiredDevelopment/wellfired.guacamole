using System;

namespace WellFired.Guacamole.Image
{
    public interface IImageSource
    {
        void Load();
        void Cancel();
        bool InProgress { get; }
        Action<LoadedImage> OnComplete { get; set; }
    }
}