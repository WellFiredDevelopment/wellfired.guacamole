using System.ComponentModel;

namespace WellFired.Guacamole
{
	public interface INativeRenderer
	{
		ViewBase Control
		{
			get;
			set;
		}

		void Render(UIRect renderRect);
		void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
	}
}