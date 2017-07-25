using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Views
{
    public partial class ImageView
    {
        [PublicAPI] public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create<ImageView, IImageSource>(
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