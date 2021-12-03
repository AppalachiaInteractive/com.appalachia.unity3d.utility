using System;
using System.Reflection;
using Appalachia.Utility.Reflection.Extensions;

namespace Appalachia.Utility.Reflection.Delegated
{
    public class InternalStaticFunction<T0, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(method, flags, null, typeof(T0));

            _invoke = (Func<T0, TR>)Delegate.CreateDelegate(typeof(Func<T0, TR>), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Func<T0, TR> _invoke;

        #endregion

        public Func<T0, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticFunction<T0, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<T0, T1, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(method, flags, null, typeof(T0), typeof(T1));

            _invoke = (Func<T0, T1, TR>)Delegate.CreateDelegate(typeof(Func<T0, T1, TR>), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Func<T0, T1, TR> _invoke;

        #endregion

        public Func<T0, T1, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticFunction<T0, T1, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<T0, T1, T2, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2)
            );

            _invoke = (Func<T0, T1, T2, TR>)Delegate.CreateDelegate(typeof(Func<T0, T1, T2, TR>), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Func<T0, T1, T2, TR> _invoke;

        #endregion

        public Func<T0, T1, T2, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticFunction<T0, T1, T2, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<T0, T1, T2, T3, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3)
            );

            _invoke = (Func<T0, T1, T2, T3, TR>)Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, TR>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Func<T0, T1, T2, T3, TR> _invoke;

        #endregion

        public Func<T0, T1, T2, T3, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticFunction<T0, T1, T2, T3, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<T0, T1, T2, T3, T4, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4)
            );

            _invoke = (Func<T0, T1, T2, T3, T4, TR>)Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, TR>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Func<T0, T1, T2, T3, T4, TR> _invoke;

        #endregion

        public Func<T0, T1, T2, T3, T4, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticFunction<T0, T1, T2, T3, T4, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<T0, T1, T2, T3, T4, T5, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5)
            );

            _invoke = (Func<T0, T1, T2, T3, T4, T5, TR>)Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, TR>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Func<T0, T1, T2, T3, T4, T5, TR> _invoke;

        #endregion

        public Func<T0, T1, T2, T3, T4, T5, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticFunction<T0, T1, T2, T3, T4, T5, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<T0, T1, T2, T3, T4, T5, T6, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6)
            );

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, TR>)Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, TR>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Func<T0, T1, T2, T3, T4, T5, T6, TR> _invoke;

        #endregion

        public Func<T0, T1, T2, T3, T4, T5, T6, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticFunction<T0, T1, T2, T3, T4, T5, T6, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<T0, T1, T2, T3, T4, T5, T6, T7, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7)
            );

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, T7, TR>)Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, TR>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Func<T0, T1, T2, T3, T4, T5, T6, T7, TR> _invoke;

        #endregion

        public Func<T0, T1, T2, T3, T4, T5, T6, T7, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX =
            nameof(InternalStaticFunction<T0, T1, T2, T3, T4, T5, T6, T7, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8)
            );

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>)Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> _invoke;

        #endregion

        public Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX =
            nameof(InternalStaticFunction<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9)
            );

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>)Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> _invoke;

        #endregion

        public Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX =
            nameof(InternalStaticFunction<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>) + ".";

        #endregion
    }

    public class InternalStaticFunction<TR>
    {
        public InternalStaticFunction(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(method, flags, null);

            _invoke = (Func<TR>)Delegate.CreateDelegate(typeof(Func<TR>), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Func<TR> _invoke;

        #endregion

        public Func<TR> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticFunction<TR>) + ".";

        #endregion
    }
}
