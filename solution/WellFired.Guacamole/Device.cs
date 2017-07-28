using System;
using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class Device
	{
		private const string ExtraPath = "Assets/GuacamoleApplication/Editor/";

		private enum RuntimePlatform
		{
			UnityEditor
		}

		private const RuntimePlatform Platform = RuntimePlatform.UnityEditor;
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

		public static string AdjustPath(string location)
		{
			switch (Platform)
			{
				case RuntimePlatform.UnityEditor:
					return ExtraPath + location;
			}
		}
	}
}