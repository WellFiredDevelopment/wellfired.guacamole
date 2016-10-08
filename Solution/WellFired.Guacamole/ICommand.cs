using System.ComponentModel;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole
{
    [PublicAPI]
    public interface ICommand : INotifyPropertyChanged
    {
        [PublicAPI]
        void Execute();

        [PublicAPI]
        bool CanExecute { get; set; }
    }
}