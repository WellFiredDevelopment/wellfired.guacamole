using WellFired.Guacamole.DataBinding.Cells;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.ViewModel
{
    public class Filter : CellBindingContextBase
    {
        private string _filterName;
        private IImageSource _filterImage;

        public string FilterName
        {
            get => _filterName;
            set => SetProperty(ref _filterName, value);
        }

        public IImageSource FilterImage
        {
            get => _filterImage;
            set => SetProperty(ref _filterImage, value);
        }
    }
}