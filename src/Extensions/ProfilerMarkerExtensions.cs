using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using Unity.Profiling.LowLevel.Unsafe;

namespace Appalachia.Utility.Extensions
{
    public static class ProfilerMarkerExtensions
    {
        public static IDisposable Suspend(this ProfilerMarker marker) => new InverseScope(marker);
       
        internal readonly struct InverseScope : IDisposable
        {
            internal readonly ProfilerMarker m_Ptr;

            internal InverseScope(ProfilerMarker markerPtr)
            {
                m_Ptr = markerPtr;
                markerPtr.End();
            }

            public void Dispose() => m_Ptr.Begin();
        }
    }
}
