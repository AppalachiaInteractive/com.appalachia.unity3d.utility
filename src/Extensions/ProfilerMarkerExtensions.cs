using System;
using Unity.Profiling;

namespace Appalachia.Utility.Extensions
{
    public static class ProfilerMarkerExtensions
    {
        public static IDisposable Suspend(this ProfilerMarker marker)
        {
            return new InverseScope(marker);
        }

        #region Nested Types

        internal readonly struct InverseScope : IDisposable
        {
            internal InverseScope(ProfilerMarker markerPtr)
            {
                m_Ptr = markerPtr;
                markerPtr.End();
            }

            internal readonly ProfilerMarker m_Ptr;

            public void Dispose()
            {
                m_Ptr.Begin();
            }
        }

        #endregion
    }
}