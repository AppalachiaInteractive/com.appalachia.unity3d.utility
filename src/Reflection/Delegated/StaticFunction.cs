using System;
using System.Reflection;
using Appalachia.Utility.Reflection.Extensions;

namespace Appalachia.Utility.Reflection.Delegated
{
    public static class StaticFunction
    {
        public static Func<TR> CreateDelegate<TR>(MethodInfo method)
        {
            return (Func<TR>) Delegate.CreateDelegate(typeof(Func<TR>), method);
        }
        
        public static Func<T1, TR> CreateDelegate<T1, TR>(MethodInfo method)
        {
            return (Func<T1, TR>) Delegate.CreateDelegate(typeof(Func<T1, TR>), method);
        }
        
        public static Func<T1, T2, TR> CreateDelegate<T1, T2, TR>(MethodInfo method)
        {
            return (Func<T1, T2, TR>) Delegate.CreateDelegate(typeof(Func<T1, T2, TR>), method);
        }
        
        public static Func<T1, T2, T3, TR> CreateDelegate<T1, T2, T3, TR>(MethodInfo method)
        {
            return (Func<T1, T2, T3, TR>) Delegate.CreateDelegate(typeof(Func<T1, T2, T3, TR>), method);
        }
        
        public static Func<T1, T2, T3, T4, TR> CreateDelegate<T1, T2, T3, T4, TR>(MethodInfo method)
        {
            return (Func<T1, T2, T3, T4, TR>) Delegate.CreateDelegate(typeof(Func<T1, T2, T3, T4, TR>), method);
        }
        
        public static Func<T1, T2, T3, T4, T5, TR> CreateDelegate<T1, T2, T3, T4, T5, TR>(MethodInfo method)
        {
            return (Func<T1, T2, T3, T4, T5, TR>) Delegate.CreateDelegate(typeof(Func<T1, T2, T3, T4, T5, TR>), method);
        }
        
        public static Func<T1, T2, T3, T4, T5, T6, TR> CreateDelegate<T1, T2, T3, T4, T5, T6, TR>(MethodInfo method)
        {
            return (Func<T1, T2, T3, T4, T5, T6, TR>) Delegate.CreateDelegate(typeof(Func<T1, T2, T3, T4, T5, T6, TR>), method);
        }
        
        public static Func<T1, T2, T3, T4, T5, T6, T7, TR> CreateDelegate<T1, T2, T3, T4, T5, T6, T7, TR>(MethodInfo method)
        {
            return (Func<T1, T2, T3, T4, T5, T6, T7, TR>) Delegate.CreateDelegate(typeof(Func<T1, T2, T3, T4, T5, T6, T7, TR>), method);
        }
        
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TR> CreateDelegate<T1, T2, T3, T4, T5, T6, T7, T8, TR>(MethodInfo method)
        {
            return (Func<T1, T2, T3, T4, T5, T6, T7, T8, TR>) Delegate.CreateDelegate(typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, TR>), method);
        }
        
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> CreateDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>(MethodInfo method)
        {
            return (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>) Delegate.CreateDelegate(typeof(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>), method);
        }

        public static Func<TR> CreateDelegate<TR>(
            Type t,
            string methodName,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = t.PrepareAndGetBestMethod(methodName, flags, null);

            return (Func<TR>) Delegate.CreateDelegate(typeof(Func<TR>), bestMethod);
        }
        
        public static Func<TR> CreateDelegate<TR>(
            string typeNameWithNamespace,
            string methodName,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var type = ReflectionExtensions.GetByName(typeNameWithNamespace);
            
            var bestMethod = type.PrepareAndGetBestMethod(methodName, flags, null);

            return (Func<TR>) Delegate.CreateDelegate(typeof(Func<TR>), bestMethod);
        }
    }

    public class StaticFunction<T, T0, TR>
    {
        private const string _PRF_PFX = nameof(StaticFunction<T, T0, TR>) + ".";
        private readonly Func<T0, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(method, flags, null, typeof(T0));

            _invoke = (Func<T0, TR>) Delegate.CreateDelegate(typeof(Func<T0, TR>), bestMethod);
        }

        public Func<T0, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, T0, T1, TR>
    {
        private const string _PRF_PFX = nameof(StaticFunction<T, T0, T1, TR>) + ".";
        private readonly Func<T0, T1, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1)
            );

            _invoke = (Func<T0, T1, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, TR>),
                bestMethod
            );
        }

        public Func<T0, T1, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, T0, T1, T2, TR>
    {
        private const string _PRF_PFX = nameof(StaticFunction<T, T0, T1, T2, TR>) + ".";
        private readonly Func<T0, T1, T2, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(
                method,
                flags,
                null,
                typeof(T0),
                typeof(T1),
                typeof(T2)
            );

            _invoke = (Func<T0, T1, T2, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, TR>),
                bestMethod
            );
        }

        public Func<T0, T1, T2, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, T0, T1, T2, T3, TR>
    {
        private const string _PRF_PFX = nameof(StaticFunction<T, T0, T1, T2, T3, TR>) + ".";
        private readonly Func<T0, T1, T2, T3, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
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

            _invoke = (Func<T0, T1, T2, T3, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, TR>),
                bestMethod
            );
        }

        public Func<T0, T1, T2, T3, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, T0, T1, T2, T3, T4, TR>
    {
        private const string _PRF_PFX = nameof(StaticFunction<T, T0, T1, T2, T3, T4, TR>) + ".";
        private readonly Func<T0, T1, T2, T3, T4, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
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

            _invoke = (Func<T0, T1, T2, T3, T4, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, TR>),
                bestMethod
            );
        }

        public Func<T0, T1, T2, T3, T4, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, T0, T1, T2, T3, T4, T5, TR>
    {
        private const string _PRF_PFX = nameof(StaticFunction<T, T0, T1, T2, T3, T4, T5, TR>) + ".";
        private readonly Func<T0, T1, T2, T3, T4, T5, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
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

            _invoke = (Func<T0, T1, T2, T3, T4, T5, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, TR>),
                bestMethod
            );
        }

        public Func<T0, T1, T2, T3, T4, T5, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, T0, T1, T2, T3, T4, T5, T6, TR>
    {
        private const string _PRF_PFX =
            nameof(StaticFunction<T, T0, T1, T2, T3, T4, T5, T6, TR>) + ".";

        private readonly Func<T0, T1, T2, T3, T4, T5, T6, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
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

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, TR>),
                bestMethod
            );
        }

        public Func<T0, T1, T2, T3, T4, T5, T6, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, T0, T1, T2, T3, T4, T5, T6, T7, TR>
    {
        private const string _PRF_PFX =
            nameof(StaticFunction<T, T0, T1, T2, T3, T4, T5, T6, T7, TR>) + ".";

        private readonly Func<T0, T1, T2, T3, T4, T5, T6, T7, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
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

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, T7, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, TR>),
                bestMethod
            );
        }

        public Func<T0, T1, T2, T3, T4, T5, T6, T7, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>
    {
        private const string _PRF_PFX =
            nameof(StaticFunction<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>) + ".";

        private readonly Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
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

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>),
                bestMethod
            );
        }

        public Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>
    {
        private const string _PRF_PFX =
            nameof(StaticFunction<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>) + ".";

        private readonly Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
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

            _invoke = (Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>) Delegate.CreateDelegate(
                typeof(Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>),
                bestMethod
            );
        }

        public Func<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> Invoke => _invoke;
    }

    public class StaticFunction<T, TR>
    {
        private const string _PRF_PFX = nameof(StaticFunction<T, TR>) + ".";
        private readonly Func<TR> _invoke;

        public StaticFunction(
            string method,
            BindingFlags flags = ReflectionExtensions.AllStatic)
        {
            var bestMethod = typeof(T).PrepareAndGetBestMethod(method, flags, null);

            _invoke = (Func<TR>) Delegate.CreateDelegate(typeof(Func<TR>), bestMethod);
        }

        public Func<TR> Invoke => _invoke;
    }
}
