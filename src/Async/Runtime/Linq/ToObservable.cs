﻿using System;
using System.Threading;
using Appalachia.Utility.Async.Internal;

namespace Appalachia.Utility.Async.Linq
{
    public static partial class AppaTaskAsyncEnumerable
    {
        public static IObservable<TSource> ToObservable<TSource>(
            this IAppaTaskAsyncEnumerable<TSource> source)
        {
            Error.ThrowArgumentNullException(source, nameof(source));

            return new ToObservable<TSource>(source);
        }
    }

    internal sealed class ToObservable<T> : IObservable<T>
    {
        private readonly IAppaTaskAsyncEnumerable<T> source;

        public ToObservable(IAppaTaskAsyncEnumerable<T> source)
        {
            this.source = source;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var ctd = new CancellationTokenDisposable();

            RunAsync(source, observer, ctd.Token).Forget();

            return ctd;
        }

        private static async AppaTaskVoid RunAsync(
            IAppaTaskAsyncEnumerable<T> src,
            IObserver<T> observer,
            CancellationToken cancellationToken)
        {
            // cancellationToken.IsCancellationRequested is called when Rx's Disposed.
            // when disposed, finish silently.

            var e = src.GetAsyncEnumerator(cancellationToken);
            try
            {
                bool hasNext;

                do
                {
                    try
                    {
                        hasNext = await e.MoveNextAsync();
                    }
                    catch (Exception ex)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }

                        observer.OnError(ex);
                        return;
                    }

                    if (hasNext)
                    {
                        observer.OnNext(e.Current);
                    }
                    else
                    {
                        observer.OnCompleted();
                        return;
                    }
                } while (!cancellationToken.IsCancellationRequested);
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        internal sealed class CancellationTokenDisposable : IDisposable
        {
            private readonly CancellationTokenSource cts = new CancellationTokenSource();

            public CancellationToken Token => cts.Token;

            public void Dispose()
            {
                if (!cts.IsCancellationRequested)
                {
                    cts.Cancel();
                }
            }
        }
    }
}
