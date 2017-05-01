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
	}
}