namespace WellFired.Guacamole.DataBinding
{
	public class SelectedItemChangedEventArgs
	{
		public SelectedItemChangedEventArgs(object selectedItem)
		{
			SelectedItem = selectedItem;
		}
		
		public object SelectedItem { get; }
	}
}