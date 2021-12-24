#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;

namespace Appalachia.Utility.Async
{
    public partial struct AppaTask
    {
        /*#region OBSOLETE_RUN

        // Run is a confusing name, use only RunOnThreadPool in the future.

        /// <summary>[Obsolete]recommend to use RunOnThreadPool(or AppaTask.Void(async void), AppaTask.Create(async AppaTask)).</summary>
        public static async AppaTask Run(Action action, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppaTask.SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    action();
                }
                finally
                {
                    await AppaTask.Yield();
                }
            }
            else
            {
                action();
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>[Obsolete]recommend to use RunOnThreadPool(or AppaTask.Void(async void), AppaTask.Create(async AppaTask)).</summary>
        public static async AppaTask Run(Action<object> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppaTask.SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    action(state);
                }
                finally
                {
                    await AppaTask.Yield();
                }
            }
            else
            {
                action(state);
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>[Obsolete]recommend to use RunOnThreadPool(or AppaTask.Void(async void), AppaTask.Create(async AppaTask)).</summary>
        public static async AppaTask Run(Func<AppaTask> action, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppaTask.SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    await action();
                }
                finally
                {
                    await AppaTask.Yield();
                }
            }
            else
            {
                await action();
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>[Obsolete]recommend to use RunOnThreadPool(or AppaTask.Void(async void), AppaTask.Create(async AppaTask)).</summary>
        public static async AppaTask Run(Func<object, AppaTask> action, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppaTask.SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    await action(state);
                }
                finally
                {
                    await AppaTask.Yield();
                }
            }
            else
            {
                await action(state);
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>[Obsolete]recommend to use RunOnThreadPool(or AppaTask.Void(async void), AppaTask.Create(async AppaTask)).</summary>
        public static async AppaTask<T> Run<T>(Func<T> func, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppaTask.SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    return func();
                }
                finally
                {
                    await AppaTask.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            else
            {
                return func();
            }
        }

        /// <summary>[Obsolete]recommend to use RunOnThreadPool(or AppaTask.Void(async void), AppaTask.Create(async AppaTask)).</summary>
        public static async AppaTask<T> Run<T>(Func<AppaTask<T>> func, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppaTask.SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    return await func();
                }
                finally
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await AppaTask.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            else
            {
                var result = await func();
                cancellationToken.ThrowIfCancellationRequested();
                return result;
            }
        }

        /// <summary>[Obsolete]recommend to use RunOnThreadPool(or AppaTask.Void(async void), AppaTask.Create(async AppaTask)).</summary>
        public static async AppaTask<T> Run<T>(Func<object, T> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppaTask.SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    return func(state);
                }
                finally
                {
                    await AppaTask.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            else
            {
                return func(state);
            }
        }

        /// <summary>[Obsolete]recommend to use RunOnThreadPool(or AppaTask.Void(async void), AppaTask.Create(async AppaTask)).</summary>
        public static async AppaTask<T> Run<T>(Func<object, AppaTask<T>> func, object state, bool configureAwait = true, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await AppaTask.SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    return await func(state);
                }
                finally
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await AppaTask.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            else
            {
                var result = await func(state);
                cancellationToken.ThrowIfCancellationRequested();
                return result;
            }
        }

        #endregion*/

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async AppaTask RunOnThreadPool(
            Action action,
            bool configureAwait = true,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    action();
                }
                finally
                {
                    await Yield();
                }
            }
            else
            {
                action();
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async AppaTask RunOnThreadPool(
            Action<object> action,
            object state,
            bool configureAwait = true,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    action(state);
                }
                finally
                {
                    await Yield();
                }
            }
            else
            {
                action(state);
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async AppaTask RunOnThreadPool(
            Func<AppaTask> action,
            bool configureAwait = true,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    await action();
                }
                finally
                {
                    await Yield();
                }
            }
            else
            {
                await action();
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async AppaTask RunOnThreadPool(
            Func<object, AppaTask> action,
            object state,
            bool configureAwait = true,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    await action(state);
                }
                finally
                {
                    await Yield();
                }
            }
            else
            {
                await action(state);
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async AppaTask<T> RunOnThreadPool<T>(
            Func<T> func,
            bool configureAwait = true,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    return func();
                }
                finally
                {
                    await Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            return func();
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async AppaTask<T> RunOnThreadPool<T>(
            Func<AppaTask<T>> func,
            bool configureAwait = true,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    return await func();
                }
                finally
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            var result = await func();
            cancellationToken.ThrowIfCancellationRequested();
            return result;
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async AppaTask<T> RunOnThreadPool<T>(
            Func<object, T> func,
            object state,
            bool configureAwait = true,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    return func(state);
                }
                finally
                {
                    await Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            return func(state);
        }

        /// <summary>Run action on the threadPool and return to main thread if configureAwait = true.</summary>
        public static async AppaTask<T> RunOnThreadPool<T>(
            Func<object, AppaTask<T>> func,
            object state,
            bool configureAwait = true,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await SwitchToThreadPool();

            cancellationToken.ThrowIfCancellationRequested();

            if (configureAwait)
            {
                try
                {
                    return await func(state);
                }
                finally
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            var result = await func(state);
            cancellationToken.ThrowIfCancellationRequested();
            return result;
        }
    }
}
