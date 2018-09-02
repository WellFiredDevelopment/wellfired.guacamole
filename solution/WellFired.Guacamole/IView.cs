using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Styling;

namespace WellFired.Guacamole
{
	public interface IView
	{
		/// <summary>
		/// Child view usually rendered inside the bound of the parent view and on top of it.
		/// </summary>
	    IView Content { get; }
		
		/// <summary>
		/// Padding between the view <see cref="RectRequest"/> and its <see cref="Content"/>.
		/// </summary>
	    UIPadding Padding { get; }
		
		/// <summary>
		/// Value used to place a view when rendering it. It is calculated while doing layouting of the different views. When layouting, we first
		/// set this value to the size requested by the view, this requested size includes the padding of the view.
		/// Then based on the available space we clamp it.
		/// </summary>
	    UIRect RectRequest { get; set; }
		
		/// <summary>
		/// Define the space available to the content. It may be different from <see cref="RectRequest"/> if for example the content of the
		/// view is centered, or if the parent view has some padding. 
		/// </summary>
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
		
		/// <summary>
		/// This is the renderer used to render the view based on its <see cref="RectRequest"/>.
		/// </summary>
	    INativeRenderer NativeRenderer { get; }
		
		/// <summary>
		/// The minimum size a view can have with padding included.
		/// </summary>
	    UISize MinSize { get; }
		
		/// <summary>
		/// The maximum size a view can have with padding included.
		/// </summary>
		UISize MaxSize { get; }
		
		/// <summary>
		/// How the view fills the available space on its horizontal axis
		/// </summary>
	    LayoutOptions HorizontalLayout { get; }
		
		/// <summary>
		/// How the view fills the available space on its vertical axis
		/// </summary>
		LayoutOptions VerticalLayout { get; }
		
		/// <summary>
		/// Applies the styles defined by a dictionary to the view's content and all its children. 
		/// </summary>
		/// <param name="styleDictionary"></param>
		void SetStyleDictionary(IStyleDictionary styleDictionary);
		
		/// <summary>
		/// This event should be raised when a Property on something bound to this view has changed.
		/// </summary>
		event PropertyChangedEventHandler PropertyChanged;
	}
}