#region

using System;
using System.Reflection;
using Appalachia.Utility.Reflection.Extensions;

#endregion

namespace Appalachia.Utility.Reflection.Delegated
{
    public static class StaticRoutine
    {
        public static Action CreateDelegate(MethodInfo method)
        {
            return (Action)Delegate.CreateDelegate(typeof(Action), method);
        }

        public static Action<T> CreateDelegate<T>(MethodInfo method)
        {
            return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), method);
        }

        public static Action<TA> CreateDelegate<T, TA>(
            string methodName,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            return CreateDelegate<TA>(typeof(T), methodName, flags);
        }

        public static Action<TA> CreateDelegate<TA>(
            Type t,
            string methodName,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(methodName, flags, null);

            return (Action<TA>)Delegate.CreateDelegate(typeof(Action<TA>), bestMethod);
        }

        public static Action CreateDelegate<T>(
            string methodName,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            return CreateDelegate(typeof(T), methodName, flags);
        }

        public static Action CreateDelegate(
            Type t,
            string methodName,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(methodName, flags, null);

            return (Action)Delegate.CreateDelegate(typeof(Action), bestMethod);
        }

        public static Action CreateDelegate(
            string typeNameWithNamespace,
            string methodName,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var type = ReflectionExtensions.GetByName(typeNameWithNamespace);

            var bestMethod = type.PrepareAndGetBestMethod(methodName, flags, null);

            return (Action)Delegate.CreateDelegate(typeof(Action), bestMethod);
        }
    }

    public class StaticRoutine<T>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(method, flags, null);

            _invoke = (Action)Delegate.CreateDelegate(typeof(Action), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Action _invoke;

        #endregion

        public Action Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(StaticRoutine<T>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(method, flags, null, typeof(T0));

            _invoke = (Action<T0>)Delegate.CreateDelegate(typeof(Action<T0>), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Action<T0> _invoke;

        #endregion

        public Action<T0> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(StaticRoutine<T, T0>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0, T1>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(method, flags, null, typeof(T0), typeof(T1));

            _invoke = (Action<T0, T1>)Delegate.CreateDelegate(typeof(Action<T0, T1>), bestMethod);
        }

        #region Fields and Autoproperties

        private readonly Action<T0, T1> _invoke;

        #endregion

        public Action<T0, T1> Invoke => _invoke;

        #region Profiling

        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0, T1, T2>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
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

        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0, T1, T2, T3>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
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

        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
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

        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3, T4>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
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

        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
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

        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
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

        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
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

        private const string _PRF_PFX = nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>) + ".";

        #endregion
    }

    public class StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public StaticRoutine(string method, BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
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
            nameof(StaticRoutine<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>) + ".";

        #endregion
    }
}
