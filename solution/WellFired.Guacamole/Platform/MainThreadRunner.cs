using System;
using System.Collections.Generic;

namespace WellFired.Guacamole.Platform
{
	public class MainThreadRunner : IMainThreadRunner
	{
		private static readonly Queue<Action> Delegates = new Queue<Action>();
		private static readonly object DelegatesLock = new object();

		public static void ExecuteOnMainThread(Action action)
		{
			lock (DelegatesLock)
			{
				Delegates.Enqueue(action);
			}
		}

		public void ProcessActions()
		{
			lock (DelegatesLock)
			{
				while (Delegates.Count > 0)
				{
					var action = Delegates.Dequeue();
					action();
				}
			}
		}
	}
}