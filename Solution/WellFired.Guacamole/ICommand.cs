using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole
{
    [PublicAPI]
    public interface ICommand
    {
        [PublicAPI]
        void Execute();

        [PublicAPI]
        bool CanExecute();
    }
}