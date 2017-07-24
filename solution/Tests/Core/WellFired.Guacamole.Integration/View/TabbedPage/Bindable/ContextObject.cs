using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.TabbedPage.Bindable
{
	public class ContextObject : NotifyBase
	{
		private string _selectedPage;

		public string SelectedPage
		{
			get { return _selectedPage; }
			set { SetProperty(ref _selectedPage, value, nameof(SelectedPage)); }
		}
	}
}