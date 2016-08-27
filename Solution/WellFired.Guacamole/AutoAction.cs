using System;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Extensions;

namespace WellFired.Guacamole
{
	public interface IAutoAction
	{
		[PublicAPI]
		void Add(IDisposable disposable, Action action);
		[PublicAPI]
		void Remove(Action action);
	}

	public class AutoAction : IAutoAction
	{
		private readonly Action _onAddFirst;
		private readonly Action _onRemoveLast;
		private Action _handlers;

		public AutoAction()
		{

		}

		public AutoAction(Action onAddFirst = null, Action onRemoveLast = null)
		{
			_onAddFirst = onAddFirst;
			_onRemoveLast = onRemoveLast;
		}

		public void Add(IDisposable disposable, Action action)
		{
			if (_handlers != null && _handlers.AlreadyHasSubscriber(action))
				return;

			disposable.AddDisposedCallback(() => Remove(action));

			var length = _handlers?.GetInvocationList().Length ?? 0;

			_handlers += action;

			if (length == 0)
				_onAddFirst?.Invoke();
		}

		[PublicAPI]
		public void Clear()
		{
			_handlers = null;
			_onRemoveLast?.Invoke();
		}

		[PublicAPI]
		public void Invoke()
		{
			_handlers?.Invoke();
		}

		[PublicAPI]
		public void Remove(Action action)
		{
			var length = _handlers?.GetInvocationList().Length ?? 0;

			_handlers -= action;

			if (length <= 0 || (_handlers != null && _handlers.GetInvocationList().Length != 0))
				return;

			_handlers = null;
			_onRemoveLast?.Invoke();
		}
	}
}