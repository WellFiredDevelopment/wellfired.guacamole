using System;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole
{
	public class Command : NotifyBase, ICommand
	{
		public delegate bool CanExecuteDelegate();

		private bool _canExecute = true;

		public Action ExecuteAction { private get; set; }
		
		public void Execute()
		{
			ExecuteAction?.Invoke();
		}

		public bool CanExecute
		{
			get => _canExecute;
			set => SetProperty(ref _canExecute, value);
		}
	}
}