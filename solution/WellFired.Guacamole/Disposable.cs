using System;
using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public interface IDisposable : System.IDisposable
	{
		void AddDisposedCallback(Action action);
	}

	public class Disposable : IDisposable
	{
		private readonly List<Action> _onDisposed;

		public Disposable()
		{
			_onDisposed = new List<Action>();
		}

		private bool IsDisposed { get; set; }

		public void AddDisposedCallback(Action action)
		{
			if (IsDisposed)
				throw new InvalidOperationException("You are adding an action to an already disposed Disposable.");

			_onDisposed.Add(action);
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;

			IsDisposed = true;

			var l = _onDisposed.ToArray();

			_onDisposed.Clear();

			foreach (var a in l)
				a?.Invoke();
		}
	}
}