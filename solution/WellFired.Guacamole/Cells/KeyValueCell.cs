namespace WellFired.Guacamole.Cells
{
	public partial class KeyValueCell : Cell
	{
		public int ValueWidth { get; set; } = 100;
		
		public KeyValueCell()
		{
			Style = Styling.Styles.KeyValueCell.Style;
		}
	}
}