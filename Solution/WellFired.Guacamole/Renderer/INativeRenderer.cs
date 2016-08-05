using System.ComponentModel;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Renderer
{
	public interface INativeRenderer
	{
		ViewBase Control { set; }
	    UISize? NativeSize { get; }

	    void Render(UIRect renderRect);
		void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
	}
}