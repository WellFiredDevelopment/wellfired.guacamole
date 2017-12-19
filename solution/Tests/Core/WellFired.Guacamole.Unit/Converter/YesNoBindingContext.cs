using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.Converter
{
	public class YesNoBindingContext : ObservableBase
	{
		private bool _yesNo; 

		public bool YesNo
		{
			get => _yesNo;
			set => SetProperty(ref _yesNo, value);
		}
	}
}