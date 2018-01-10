using System.ComponentModel;
using JetBrains.Annotations;

namespace WellFired.Guacamole
{
	[PublicAPI]
	public interface ICommand : INotifyPropertyChanged
	{
		[PublicAPI]
		bool CanExecute { get; set; }

		[PublicAPI]
		void Execute();
	}
}