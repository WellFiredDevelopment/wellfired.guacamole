using System;

namespace WellFired.Guacamole
{
    public class Command : ICommand
    {
        public void Execute()
        {
            ExecuteAction?.Invoke();
        }

        public bool CanExecute()
        {
            return true;
        }

        public Action ExecuteAction { private get; set; }
    }
}