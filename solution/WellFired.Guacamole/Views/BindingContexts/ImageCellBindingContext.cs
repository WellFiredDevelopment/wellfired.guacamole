using System;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Views.BindingContexts
{
    public class ImageCellBindingContext : CellBindingContextBase
    {
        private IImageSource _imageSource;

        [PublicAPI]
        public IImageSource ImageSource
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }

        public ImageCellBindingContext(Uri uri)
        {
            ImageSource = Image.ImageSource.From(uri);
        }
    }
}