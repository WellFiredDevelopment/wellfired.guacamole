using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Exceptions
{
    public class DataTemplateTypeDidntCreateAPage : GuacamoleUserFacingException
    {
        private readonly View _view;
        private readonly object _bindableObject;
        private readonly IBindableObject _newPage;

        public DataTemplateTypeDidntCreateAPage(View view, object bindableObject, IBindableObject newPage)
        {
            _view = view;
            _bindableObject = bindableObject;
            _newPage = newPage;
        }

        public override string UserFacingError()
        {
            return
                $"The {nameof(DataTemplate)} you used for view {_view.GetType()} did not return a page. When you're creating a tabbed view, the visual entries should all be pages. Your {nameof(DataTemplate)} returned a {_newPage} for bindableObject {_bindableObject}";
        }
    }
}