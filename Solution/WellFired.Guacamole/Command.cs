using System;

namespace WellFired.Guacamole
{
    public class Command : ICommand
    {
        public delegate bool CanExecuteDelegate();

        public void Execute()
        {
            ExecuteAction?.Invoke();
        }

        public bool CanExecute()
        {
            return CanExecuteAction?.Invoke() ?? true; // We invoke automatically if we didn't provide an CanExecuteDelegate.
        }

        public Action ExecuteAction { private get; set; }
        public CanExecuteDelegate CanExecuteAction { private get; set; }
    }
}