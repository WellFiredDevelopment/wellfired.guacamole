using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.Page.Bindable
{
	public class ContextObject : NotifyBase
	{
		private string _title;

		public string Title
		{
			get => _title;
			set => SetProperty(ref _title, value, nameof(Title));
		}
	}
}