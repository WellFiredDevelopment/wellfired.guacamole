using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.DataBinding.Cells
{
    public class PageBindingContextBase : ObservableBase
    {
        private string _title;

        [PublicAPI]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}