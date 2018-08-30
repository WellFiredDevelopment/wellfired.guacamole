using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.TabbedPage.Bindable
{
	public class ContextObject : ObservableBase
	{
		private string _selectedPage;

		public string SelectedPage
		{
			get => _selectedPage;
			set => SetProperty(ref _selectedPage, value, nameof(SelectedPage));
		}
	}
}