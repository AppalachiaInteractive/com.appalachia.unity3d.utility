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

        #region Nested type: InverseScope

        #region Nested Types

        internal readonly struct InverseScope : IDisposable
        {
            internal InverseScope(ProfilerMarker markerPtr)
            {
                m_Ptr = markerPtr;
                markerPtr.End();
            }

            internal readonly ProfilerMarker m_Ptr;

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                m_Ptr.Begin();
            }

            #endregion
        }

        #endregion
    }
}
