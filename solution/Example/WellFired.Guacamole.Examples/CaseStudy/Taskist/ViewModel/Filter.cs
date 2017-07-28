using WellFired.Guacamole.Image;
using WellFired.Guacamole.Views.BindingContexts;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.ViewModel
{
    public class Filter : CellBindingContextBase
    {
        private string _filterName;
        private IImageSource _filterImage;

        public string FilterName
        {
            get { return _filterName; }
            set { SetProperty(ref _filterName, value); }
        }

        public IImageSource FilterImage
        {
            get { return _filterImage; }
            set { SetProperty(ref _filterImage, value); }
        }
    }
}