using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views.BindingContexts
{
    public class PageBindingContextBase : ObservableBase
    {
        private string _title;

        [PublicAPI]
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}