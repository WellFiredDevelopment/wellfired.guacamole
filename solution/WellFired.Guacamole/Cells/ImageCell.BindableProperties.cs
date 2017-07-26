using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Cells
{
    public partial class ImageCell
    {
        [PublicAPI] public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create<ImageCell, IImageSource>(
            default(IImageSource),
            BindingMode.TwoWay,
            v => v.ImageSource
        );
        
        public IImageSource ImageSource
        {
            get { return (IImageSource) GetValue(ImageSourceProperty); }
            set
            {
                var prevValue = ImageSource; 
                if(SetValue(ImageSourceProperty, value))
                    prevValue?.Cancel();
            }
        }
    }
}