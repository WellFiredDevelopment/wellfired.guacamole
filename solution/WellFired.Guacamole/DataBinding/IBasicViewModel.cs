using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platform;

namespace WellFired.Guacamole.DataBinding
{
	public interface IBasicViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// Three services will be injected into your base ViewModel automatically by Guacamole.
		/// </summary>
		/// <param name="logger">The system logger</param>
		/// <param name="persistentData">This might be a valid object or could be null depending on how you've configured your window</param>
		/// <param name="platformProvider">Providing platform specific functionalities</param>
		void Inject(ILogger logger, INotifyPropertyChanged persistentData, IPlatformProvider platformProvider);
	}
}