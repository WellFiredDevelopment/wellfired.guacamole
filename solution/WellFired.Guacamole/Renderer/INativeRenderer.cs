using System.ComponentModel;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Renderer
{
	public interface INativeRenderer
	{
		View Control { set; }
		UISize? NativeSize { get; }

		void Create();
		void Render(UIRect renderRect);
		void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
		void FocusControl();
		
		/// <summary>
		/// When PushMaskStack is called, the rect that is passed to Push defines the mask rect.
		/// If you call Push, you must also call Pop. I.E.
		/// PushMaskStack(rect)
		/// DoSomeRendering()
		/// PopMaskStack()
		/// Not all NativeViews will push a MaskStack
		/// </summary>
		/// <param name="maskRect">The rect with which you want to mask</param>
		/// <returns>If the Native view pushed a mask stack or not.</returns>
		bool PushMaskStack(UIRect maskRect);
		
		/// <summary>
		/// You must call PopMaskStack after a call to PushMaskStack, once you've finished rendering into that 
		/// masked area.
		/// </summary>
		void PopMaskStack();
	}
}