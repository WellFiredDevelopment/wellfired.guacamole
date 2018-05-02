namespace WellFired.Guacamole.Cells
{
	/// <summary>
	/// KeyValueCell is a cell divided in two. The left part corresponds to the text of the key value, the right part corresponds to the text
	/// of the value value. This is useful to display a list of settings with the following fashion :
	/// Automatically commit       yes
	/// API used                   OpenGL
	/// .....
	/// </summary>
	public partial class KeyValueCell : Cell
	{
		/// <summary>
		/// This is the fixed width occupied by the value content. If for example the cell is filling all the available space horizontally,
		/// then the value part will still have the same width, only key part will expand. This ensures that when key value cells are placed under
		/// each other, the values are all aligned.
		/// </summary>
		public int ValueWidth { get; set; } = 100;
		
		public KeyValueCell()
		{
			Style = Styling.Styles.KeyValueCell.Style;
		}
	}
}