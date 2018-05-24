using System;
using System.Collections.Generic;

namespace WellFired.Guacamole.Platforms
{
	public class MainThreadRunner
	{
		private static readonly Queue<Action> MainThreadDelegates = new Queue<Action>();
		private static readonly Queue<Action> PreLayoutDelegates = new Queue<Action>();
		private static readonly object DelegatesLock = new object();

		/// <summary>
		/// Queues an action to be executed on the main thread.
		/// </summary>
		/// <param name="action"></param>
		public static void ExecuteOnMainThread(Action action)
		{
			lock (DelegatesLock)
			{
				MainThreadDelegates.Enqueue(action);
			}
		}
		
		/// <summary>
		/// Queues an action to be executed on the main thread before the UI is layouted. Any action having an impact on the UI should be executed here to ensure
		/// the UI changes are layouted correctly before to be rended.
		/// </summary>
		/// <param name="action"></param>
		public static void ExecuteBeforeLayout(Action action)
		{
			lock (DelegatesLock)
			{
				PreLayoutDelegates.Enqueue(action);
			}
		}
		
		/// <summary>
		/// Execute actions on the main thread before the UI is layouted.
		/// </summary>
		public void ProcessPreLayoutActions()
		{
			ProcessActions(PreLayoutDelegates);
		}

		/// <summary>
		/// Execute actions on the main thread.
		/// </summary>
		public void ProcessMainThreadActions()
		{
			ProcessActions(MainThreadDelegates);
		}

		private static void ProcessActions(Queue<Action> actions)
		{
			lock (DelegatesLock)
			{
				while (actions.Count > 0)
				{
					var action = actions.Dequeue();
					action();
				}
			}
		}
	}
}