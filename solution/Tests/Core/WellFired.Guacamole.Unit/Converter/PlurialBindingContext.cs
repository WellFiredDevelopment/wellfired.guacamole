using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.Converter
{
	public class PlurialBindingContext : ObservableBase
	{
		private string _word = "dog"; 

		public string Word
		{
			get => _word;
			set => SetProperty(ref _word, value);
		}
	}
}