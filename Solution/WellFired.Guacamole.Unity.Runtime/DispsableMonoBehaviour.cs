using System;
using UnityEngine;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Unity.Runtime
{
    public abstract class DispsableMonoBehaviour : MonoBehaviour, IDisposable
    {
        private bool _disposed;
        private Disposable _disposable;

        [UsedImplicitly]
        public virtual void Awake()
        {
            _disposable = new Disposable();
        }

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

        [UsedImplicitly]
        public void OnDestroy()
        {
            Dispose();
        }

        protected virtual void OnDispose()
        {
        }

        void IDisposable.AddDisposedCallback(Action action)
        {
            _disposable.AddDisposedCallback(action);
        }

        [PublicAPI]
        protected void DisposeOf(params System.IDisposable[] disposables)
        {
            foreach (var disposable in disposables)
                disposable?.Dispose();
        }
    }
}