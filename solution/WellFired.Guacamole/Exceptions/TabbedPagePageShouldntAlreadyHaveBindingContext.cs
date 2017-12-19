using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Exceptions
{
    public class TabbedPagePageShouldntAlreadyHaveBindingContext : GuacamoleUserFacingException
    {
        private readonly TabbedPage _tabbedPage;
        private readonly object _bindingContext;
        private readonly IBindableObject _newPage;

        public TabbedPagePageShouldntAlreadyHaveBindingContext(TabbedPage tabbedPage, object bindingContext, IBindableObject newPage)
        {
            _tabbedPage = tabbedPage;
            _bindingContext = bindingContext;
            _newPage = newPage;
        }

        public override string UserFacingError()
        {
            return $"The {_newPage.GetType()} created by {_tabbedPage.GetType()} for bindingContext {_bindingContext} already has a bindingContext, when you're using a {_newPage.GetType()}, " +
                   $"the {_newPage.GetType()} will automatically manage the BindingContext for you. It will be set according to the ItemSource provided.";
        }
    }
}