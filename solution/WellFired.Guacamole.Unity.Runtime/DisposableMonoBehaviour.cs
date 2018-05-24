using System;
using JetBrains.Annotations;
using UnityEngine;

namespace WellFired.Guacamole.Unity.Runtime
{
	public abstract class DisposableMonoBehaviour : MonoBehaviour, IDisposable
	{
		private Disposable _disposable;
		private bool _disposed;

		[PublicAPI]
		public void Dispose()
		{
			if (_disposed)
				return;

			_disposed = true;
			_disposable.Dispose();
			OnDispose();
			Destroy(gameObject);
		}

		void IDisposable.AddDisposedCallback(Action action)
		{
			_disposable.AddDisposedCallback(action);
		}

		[UsedImplicitly]
		public virtual void Awake()
		{
			_disposable = new Disposable();
		}

		[UsedImplicitly]
		public void OnDestroy()
		{
			Dispose();
		}

		protected virtual void OnDispose()
		{
		}

		[PublicAPI]
		protected void DisposeOf(params System.IDisposable[] disposables)
		{
			foreach (var disposable in disposables)
				disposable?.Dispose();
		}
	}
}