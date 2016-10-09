using System;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole
{
	public class Command : NotifyBase, ICommand
	{
		public delegate bool CanExecuteDelegate();

		private bool? _canExecute;

		public Action ExecuteAction { private get; set; }
		public CanExecuteDelegate CanExecuteAction { private get; set; }

		public void Execute()
		{
			ExecuteAction?.Invoke();
		}

		public bool CanExecute
		{
			get
			{
				var ce = CanExecuteAction?.Invoke() ?? true;

				var existingValue = _canExecute ?? !ce;
				var newValue = ce;
				_canExecute = ce;

				SetProperty(ref existingValue, newValue, nameof(CanExecute));

				return _canExecute.Value;
			}
			set { _canExecute = value; }
		}
	}
}