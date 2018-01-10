using System;
using JetBrains.Annotations;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.DataBinding.Cells
{
    public class ImageCellBindingContext : CellBindingContextBase
    {
        private IImageSource _imageSource;

        [PublicAPI]
        public IImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public ImageCellBindingContext(Uri uri)
        {
            ImageSource = Image.ImageSource.From(uri);
        }
    }
}