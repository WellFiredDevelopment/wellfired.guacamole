using System;

namespace WellFired.Guacamole.Image
{
    public interface IImageSource
    {
        void Cancel();
        bool InProgress { get; }
        Action OnComplete { get; set; }
    }
}