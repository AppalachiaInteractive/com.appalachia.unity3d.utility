namespace Appalachia.Utility.Timing.Contracts
{
    public interface ICoreTimeProvider : IReadOnlyCoreTimeProvider
    {
        /// <summary>
        ///     The game world age in seconds.
        /// </summary>
        new double WorldAge { get; set; }

        /// <summary>
        ///     <para>The interval in seconds at which physics and other fixed frame rate updates (like MonoBehaviour's MonoBehaviour.FixedUpdate) are performed.</para>
        /// </summary>
        new float FixedDeltaTime { get; set; }

        /// <summary>
        ///     <para>The scale at which time passes.</para>
        /// </summary>
        new float TimeScale { get; set; }

        /*
        /// <summary>
        ///     <para>Slows your application’s playback time to allow Unity to save screenshots in between frames.</para>
        /// </summary>
        new float CaptureDeltaTime { get; set; }

        /// <summary>
        ///     <para>The maximum value of Time.deltaTime in any given frame. This is a time in seconds that limits the increase of Time.time between two frames.</para>
        /// </summary>
        new float MaximumDeltaTime { get; set; }

        /// <summary>
        ///     <para>
        ///         The maximum time a frame can spend on particle updates. If the frame takes longer than this, then updates are split into multiple smaller
        ///         updates.
        ///     </para>
        /// </summary>
        new float MaximumParticleDeltaTime { get; set; }

        /// <summary>
        ///     <para>The reciprocal of Time.captureDeltaTime.</para>
        /// </summary>
        new int CaptureFramerate { get; set; }
        */
    }
}
