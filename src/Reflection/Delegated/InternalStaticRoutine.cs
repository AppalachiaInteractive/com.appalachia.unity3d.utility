#region

using System;
using System.Reflection;
using Appalachia.Utility.Reflection.Extensions;

#endregion

namespace Appalachia.Utility.Reflection.Delegated
{
    public class InternalStaticRoutine
    {
        public InternalStaticRoutine(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(method, flags, null);

            _invoke = (Action)Delegate.CreateDelegate(typeof(Action), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Action _invoke;

        #endregion

        public Action Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticRoutine) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0>
    {
        public InternalStaticRoutine(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(method, flags, null, typeof(T0));

            _invoke = (Action<T0>)Delegate.CreateDelegate(typeof(Action<T0>), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Action<T0> _invoke;

        #endregion

        public Action<T0> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticRoutine<T0>) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0, T1>
    {
        public InternalStaticRoutine(
            Type t,
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(method, flags, null, typeof(T0), typeof(T1));

            _invoke = (Action<T0, T1>)Delegate.CreateDelegate(typeof(Action<T0, T1>), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1> _invoke;

        #endregion

        public Action<T0, T1> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticRoutine<T0, T1>) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0, T1, T2>
    {
        public InternalStaticRoutine(
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

            _invoke = (Action<T0, T1, T2>)Delegate.CreateDelegate(typeof(Action<T0, T1, T2>), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1, T2> _invoke;

        #endregion

        public Action<T0, T1, T2> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticRoutine<T0, T1, T2>) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0, T1, T2, T3>
    {
        public InternalStaticRoutine(
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

            _invoke = (Action<T0, T1, T2, T3>)Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1, T2, T3> _invoke;

        #endregion

        public Action<T0, T1, T2, T3> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticRoutine<T0, T1, T2, T3>) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0, T1, T2, T3, T4>
    {
        public InternalStaticRoutine(
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

            _invoke = (Action<T0, T1, T2, T3, T4>)Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1, T2, T3, T4> _invoke;

        #endregion

        public Action<T0, T1, T2, T3, T4> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticRoutine<T0, T1, T2, T3, T4>) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0, T1, T2, T3, T4, T5>
    {
        public InternalStaticRoutine(
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

            _invoke = (Action<T0, T1, T2, T3, T4, T5>)Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1, T2, T3, T4, T5> _invoke;

        #endregion

        public Action<T0, T1, T2, T3, T4, T5> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticRoutine<T0, T1, T2, T3, T4, T5>) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0, T1, T2, T3, T4, T5, T6>
    {
        public InternalStaticRoutine(
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

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6>)Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1, T2, T3, T4, T5, T6> _invoke;

        #endregion

        public Action<T0, T1, T2, T3, T4, T5, T6> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticRoutine<T0, T1, T2, T3, T4, T5, T6>) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0, T1, T2, T3, T4, T5, T6, T7>
    {
        public InternalStaticRoutine(
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

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6, T7>)Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7> _invoke;

        #endregion

        public Action<T0, T1, T2, T3, T4, T5, T6, T7> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(InternalStaticRoutine<T0, T1, T2, T3, T4, T5, T6, T7>) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        public InternalStaticRoutine(
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

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>)Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> _invoke;

        #endregion

        public Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX =
            nameof(InternalStaticRoutine<T0, T1, T2, T3, T4, T5, T6, T7, T8>) + ".";

        #endregion
    }

    public class InternalStaticRoutine<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public InternalStaticRoutine(
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

            _invoke = (Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>)Delegate.CreateDelegate(
                typeof(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>),
                bestMethod
            );
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> _invoke;

        #endregion

        public Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX =
            nameof(InternalStaticRoutine<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>) + ".";

        #endregion
    }
}
