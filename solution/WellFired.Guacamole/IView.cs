using WellFired.Guacamole.Data;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Styling;

namespace WellFired.Guacamole
{
	public interface IView
	{
	    IView Content { get; }
	    UIPadding Padding { get; }
		/// <summary>
		/// In one rendering loop, this value will be the size and position that the view is requesting, then the size and position
		/// that could be assigned to it based on the available space.
		/// </summary>
	    UIRect RectRequest { get; set; }
		UIRect ContentRectRequest { get; set; }
		/// <summary>
		/// This is the id of the view. Most algorithm using it will consider this value to be unique for each views.
		/// So it is recommended to assign it with a random and unique fashion (GUID, incremental value), or with an arbitrary value for
		/// debugging purpose.
		/// </summary>
	    string Id { get; set; }
		/// <summary>
		/// Flag to determine if <see cref="RectRequest"/> should be recalculated or not. This is used for optimization purpose to avoid recalculating
		/// the size requested by a view if there is no reason for it to have changed.
		/// </summary>
	    bool ValidRectRequest { get; set; }
	    INativeRenderer NativeRenderer { get; }
	    UISize MinSize { get; }
		UISize MaxSize { get; }
	    LayoutOptions HorizontalLayout { get; }
	    LayoutOptions VerticalLayout { get; }
		void SetStyleDictionary(IStyleDictionary styleDictionary);
	}
}