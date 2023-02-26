using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

public static class IDisposableExtension
{
    public static IDisposable Empty(this IDisposable target)
    {
        return EmptyDisposer.Value;
    }

    public static IDisposable Concat(this IDisposable first, IDisposable second)
    {
        if (first is ConcatDisposer concatDisposer)
        {
            return concatDisposer.Add(second);
        }

        return new ConcatDisposer(first, second);
    }


    private sealed class ConcatDisposer : IDisposable
    {
        private ConcurrentQueue<IDisposable> _disposables = new ConcurrentQueue<IDisposable>();
        private bool disposedValue;

        public bool IsDisposed => disposedValue;

        public ConcatDisposer(IDisposable first, IDisposable second)
        {
            _disposables.Enqueue(first);
            _disposables.Enqueue(second);
        }

        public IDisposable Add(IDisposable target)
        {
            if (this.IsDisposed) return target;

            _disposables.Enqueue(target);
            return target;
        }

        private void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
            {
                if (disposing)
                {
                    while(_disposables.TryDequeue(out var disposer))
                    {
                        using (disposer) { }
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }
    }



    private class EmptyDisposer : IDisposable
    {
        public static IDisposable Value { get; } = new EmptyDisposer();

        public void Dispose(){}
    }

}
