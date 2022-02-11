namespace Appalachia.Utility.Timing.Contracts
{
    public interface IReadOnlyCoreTimeProvider
    {
        /// <summary>
        ///     <para>Returns true if called inside a fixed time step callback (like MonoBehaviour's MonoBehaviour.FixedUpdate), otherwise returns false.</para>
        /// </summary>
        bool InFixedTimeStep { get; }

        /// <summary>
        ///     <para>
        ///         The double precision time since the last MonoBehaviour.FixedUpdate started (Read Only). This is the time in seconds since the start of the
        ///         game.
        ///     </para>
        /// </summary>

        double FixedTimeAsDouble { get; }

        /// <summary>
        ///     <para>
        ///         The double precision timeScale-independent time at the beginning of the last MonoBehaviour.FixedUpdate (Read Only). This is the time in
        ///         seconds since the start of the game.
        ///     </para>
        /// </summary>

        double FixedUnscaledTimeAsDouble { get; }

        /// <summary>
        ///     <para>The real time in seconds since the game started (Read Only). Double precision version of Time.realtimeSinceStartup. </para>
        /// </summary>

        double RealtimeSinceStartupAsDouble { get; }

        /// <summary>
        ///     <para>The double precision time at the beginning of this frame (Read Only). This is the time in seconds since the start of the game.</para>
        /// </summary>

        double TimeAsDouble { get; }

        /// <summary>
        ///     <para>
        ///         The double precision time since this frame started (Read Only). This is the time in seconds since the last non-additive scene has finished
        ///         loading.
        ///     </para>
        /// </summary>

        double TimeSinceLevelLoadAsDouble { get; }

        /// <summary>
        ///     <para>The double precision timeScale-independent time for this frame (Read Only). This is the time in seconds since the start of the game.</para>
        /// </summary>

        double UnscaledTimeAsDouble { get; }

        /// <summary>
        ///     The game world age in seconds.
        /// </summary>
        double WorldAge { get; }

        /// <summary>
        ///     <para>The interval in seconds from the last frame to the current one (Read Only).</para>
        /// </summary>
        float DeltaTime { get; }

        /// <summary>
        ///     <para>The interval in seconds at which physics and other fixed frame rate updates (like MonoBehaviour's MonoBehaviour.FixedUpdate) are performed.</para>
        /// </summary>
        float FixedDeltaTime { get; }

        /// <summary>
        ///     <para>The time since the last MonoBehaviour.FixedUpdate started (Read Only). This is the time in seconds since the start of the game.</para>
        /// </summary>
        float FixedTime { get; }

        /// <summary>
        ///     <para>The timeScale-independent interval in seconds from the last MonoBehaviour.FixedUpdate phase to the current one (Read Only).</para>
        /// </summary>
        float FixedUnscaledDeltaTime { get; }

        /// <summary>
        ///     <para>
        ///         The timeScale-independent time at the beginning of the last MonoBehaviour.FixedUpdate phase (Read Only). This is the time in seconds since
        ///         the start of the game.
        ///     </para>
        /// </summary>
        float FixedUnscaledTime { get; }

        /// <summary>
        ///     <para>The real time in seconds since the game started (Read Only).</para>
        /// </summary>

        float RealtimeSinceStartup { get; }

        /// <summary>
        ///     <para>A smoothed out Time.deltaTime (Read Only).</para>
        /// </summary>
        float SmoothDeltaTime { get; }

        /// <summary>
        ///     <para>The time at the beginning of this frame (Read Only).</para>
        /// </summary>

        float Time { get; }

        /// <summary>
        ///     <para>The scale at which time passes.</para>
        /// </summary>
        float TimeScale { get; }

        /// <summary>
        ///     <para>The time since this frame started (Read Only). This is the time in seconds since the last non-additive scene has finished loading.</para>
        /// </summary>

        float TimeSinceLevelLoad { get; }

        /// <summary>
        ///     <para>The timeScale-independent interval in seconds from the last frame to the current one (Read Only).</para>
        /// </summary>
        float UnscaledDeltaTime { get; }

        /// <summary>
        ///     <para>The timeScale-independent time for this frame (Read Only). This is the time in seconds since the start of the game.</para>
        /// </summary>
        float UnscaledTime { get; }

        /// <summary>
        ///     <para>The total number of frames since the start of the game (Read Only).</para>
        /// </summary>
        int FrameCount { get; }

        /*
         
        /// <summary>
        ///     <para>The reciprocal of Time.captureDeltaTime.</para>
        /// </summary>
        int CaptureFramerate { get; }
        
        int RenderedFrameCount { get; }
        

        /// <summary>
        ///     <para>Slows your application’s playback time to allow Unity to save screenshots in between frames.</para>
        /// </summary>
        float CaptureDeltaTime { get; }

        /// <summary>
        ///     <para>The maximum value of Time.deltaTime in any given frame. This is a time in seconds that limits the increase of Time.time between two frames.</para>
        /// </summary>
        float MaximumDeltaTime { get; }

        /// <summary>
        ///     <para>
        ///         The maximum time a frame can spend on particle updates. If the frame takes longer than this, then updates are split into multiple smaller
        ///         updates.
        ///     </para>
        /// </summary>
        float MaximumParticleDeltaTime { get; }
        
        */
    }
}
