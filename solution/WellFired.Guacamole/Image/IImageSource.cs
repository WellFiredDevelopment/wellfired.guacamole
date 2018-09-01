using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Image
{
    public interface IImageSource
    {
        [PublicAPI]
        Task<LoadedImage> Load();
        
        [PublicAPI]
        void Cancel();
        
        [PublicAPI]
        Action<LoadedImage> OnComplete { get; set; }
        
        [PublicAPI]
        UIPadding? NineSliceDefinition { get; }
    }
}