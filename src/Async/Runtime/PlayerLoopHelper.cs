#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Linq;
using System.Threading;
using Appalachia.Utility.Async.Internal;
using UnityEngine;
#if UNITY_2019_3_OR_NEWER
using UnityEngine.LowLevel;
using PlayerLoopType = UnityEngine.PlayerLoop;
#else
using UnityEngine.Experimental.LowLevel;
using PlayerLoopType = UnityEngine.Experimental.PlayerLoop;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Appalachia.Utility.Async
{
    public static class AppaTaskLoopRunners
    {
        public struct AppaTaskLoopRunnerInitialization
        {
        }

        public struct AppaTaskLoopRunnerEarlyUpdate
        {
        }

        public struct AppaTaskLoopRunnerFixedUpdate
        {
        }

        public struct AppaTaskLoopRunnerPreUpdate
        {
        }

        public struct AppaTaskLoopRunnerUpdate
        {
        }

        public struct AppaTaskLoopRunnerPreLateUpdate
        {
        }

        public struct AppaTaskLoopRunnerPostLateUpdate
        {
        }

        // Last

        public struct AppaTaskLoopRunnerLastInitialization
        {
        }

        public struct AppaTaskLoopRunnerLastEarlyUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastFixedUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastPreUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastPreLateUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastPostLateUpdate
        {
        }

        // Yield

        public struct AppaTaskLoopRunnerYieldInitialization
        {
        }

        public struct AppaTaskLoopRunnerYieldEarlyUpdate
        {
        }

        public struct AppaTaskLoopRunnerYieldFixedUpdate
        {
        }

        public struct AppaTaskLoopRunnerYieldPreUpdate
        {
        }

        public struct AppaTaskLoopRunnerYieldUpdate
        {
        }

        public struct AppaTaskLoopRunnerYieldPreLateUpdate
        {
        }

        public struct AppaTaskLoopRunnerYieldPostLateUpdate
        {
        }

        // Yield Last

        public struct AppaTaskLoopRunnerLastYieldInitialization
        {
        }

        public struct AppaTaskLoopRunnerLastYieldEarlyUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastYieldFixedUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastYieldPreUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastYieldUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastYieldPreLateUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastYieldPostLateUpdate
        {
        }

#if UNITY_2020_2_OR_NEWER
        public struct AppaTaskLoopRunnerTimeUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastTimeUpdate
        {
        }

        public struct AppaTaskLoopRunnerYieldTimeUpdate
        {
        }

        public struct AppaTaskLoopRunnerLastYieldTimeUpdate
        {
        }
#endif
    }

    public enum PlayerLoopTiming
    {
        Initialization = 0,
        LastInitialization = 1,

        EarlyUpdate = 2,
        LastEarlyUpdate = 3,

        FixedUpdate = 4,
        LastFixedUpdate = 5,

        PreUpdate = 6,
        LastPreUpdate = 7,

        Update = 8,
        LastUpdate = 9,

        PreLateUpdate = 10,
        LastPreLateUpdate = 11,

        PostLateUpdate = 12,
        LastPostLateUpdate = 13,

#if UNITY_2020_2_OR_NEWER

        // Unity 2020.2 added TimeUpdate https://docs.unity3d.com/2020.2/Documentation/ScriptReference/PlayerLoop.TimeUpdate.html
        TimeUpdate = 14,
        LastTimeUpdate = 15,
#endif
    }

    [Flags]
    public enum InjectPlayerLoopTimings
    {
        /// <summary>
        ///     Preset: All loops(default).
        /// </summary>
        All = Initialization |
              LastInitialization |
              EarlyUpdate |
              LastEarlyUpdate |
              FixedUpdate |
              LastFixedUpdate |
              PreUpdate |
              LastPreUpdate |
              Update |
              LastUpdate |
              PreLateUpdate |
              LastPreLateUpdate |
              PostLateUpdate |
              LastPostLateUpdate
#if UNITY_2020_2_OR_NEWER
             |
              TimeUpdate |
              LastTimeUpdate,
#else
            ,
#endif

        /// <summary>
        ///     Preset: All without last except LastPostLateUpdate.
        /// </summary>
        Standard = Initialization |
                   EarlyUpdate |
                   FixedUpdate |
                   PreUpdate |
                   Update |
                   PreLateUpdate |
                   PostLateUpdate |
                   LastPostLateUpdate
#if UNITY_2020_2_OR_NEWER
                  |
                   TimeUpdate
#endif
        ,

        /// <summary>
        ///     Preset: Minimum pattern, Update | FixedUpdate | LastPostLateUpdate
        /// </summary>
        Minimum = Update | FixedUpdate | LastPostLateUpdate,

        // PlayerLoopTiming

        Initialization = 1,
        LastInitialization = 2,

        EarlyUpdate = 4,
        LastEarlyUpdate = 8,

        FixedUpdate = 16,
        LastFixedUpdate = 32,

        PreUpdate = 64,
        LastPreUpdate = 128,

        Update = 256,
        LastUpdate = 512,

        PreLateUpdate = 1024,
        LastPreLateUpdate = 2048,

        PostLateUpdate = 4096,
        LastPostLateUpdate = 8192

#if UNITY_2020_2_OR_NEWER
        ,

        // Unity 2020.2 added TimeUpdate https://docs.unity3d.com/2020.2/Documentation/ScriptReference/PlayerLoop.TimeUpdate.html
        TimeUpdate = 16384,
        LastTimeUpdate = 32768
#endif
    }

    public interface IPlayerLoopItem
    {
        bool MoveNext();
    }

    public static class PlayerLoopHelper
    {
        private static readonly ContinuationQueue ThrowMarkerContinuationQueue =
            new ContinuationQueue(PlayerLoopTiming.Initialization);

        private static readonly PlayerLoopRunner ThrowMarkerPlayerLoopRunner =
            new PlayerLoopRunner(PlayerLoopTiming.Initialization);

        public static SynchronizationContext UnitySynchronizationContext => unitySynchronizationContext;
        public static int MainThreadId => mainThreadId;
        internal static string ApplicationDataPath => applicationDataPath;

        public static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == mainThreadId;

        private static int mainThreadId;
        private static string applicationDataPath;
        private static SynchronizationContext unitySynchronizationContext;
        private static ContinuationQueue[] yielders;
        private static PlayerLoopRunner[] runners;
        internal static bool IsEditorApplicationQuitting { get; private set; }

        private static PlayerLoopSystem[] InsertRunner(
            PlayerLoopSystem loopSystem,
            bool injectOnFirst,
            Type loopRunnerYieldType,
            ContinuationQueue cq,
            Type loopRunnerType,
            PlayerLoopRunner runner)
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += state =>
            {
                if ((state == PlayModeStateChange.EnteredEditMode) ||
                    (state == PlayModeStateChange.ExitingEditMode))
                {
                    IsEditorApplicationQuitting = true;

                    // run rest action before clear.
                    if (runner != null)
                    {
                        runner.Run();
                        runner.Clear();
                    }

                    if (cq != null)
                    {
                        cq.Run();
                        cq.Clear();
                    }

                    IsEditorApplicationQuitting = false;
                }
            };
#endif

            var yieldLoop = new PlayerLoopSystem { type = loopRunnerYieldType, updateDelegate = cq.Run };

            var runnerLoop = new PlayerLoopSystem { type = loopRunnerType, updateDelegate = runner.Run };

            // Remove items from previous initializations.
            var source = RemoveRunner(loopSystem, loopRunnerYieldType, loopRunnerType);
            var dest = new PlayerLoopSystem[source.Length + 2];

            Array.Copy(source, 0, dest, injectOnFirst ? 2 : 0, source.Length);
            if (injectOnFirst)
            {
                dest[0] = yieldLoop;
                dest[1] = runnerLoop;
            }
            else
            {
                dest[dest.Length - 2] = yieldLoop;
                dest[dest.Length - 1] = runnerLoop;
            }

            return dest;
        }

        private static PlayerLoopSystem[] RemoveRunner(
            PlayerLoopSystem loopSystem,
            Type loopRunnerYieldType,
            Type loopRunnerType)
        {
            return loopSystem.subSystemList
                             .Where(ls => (ls.type != loopRunnerYieldType) && (ls.type != loopRunnerType))
                             .ToArray();
        }

        private static PlayerLoopSystem[] InsertAppaTaskSynchronizationContext(PlayerLoopSystem loopSystem)
        {
            var loop = new PlayerLoopSystem
            {
                type = typeof(AppaTaskSynchronizationContext),
                updateDelegate = AppaTaskSynchronizationContext.Run
            };

            // Remove items from previous initializations.
            var source = loopSystem.subSystemList
                                   .Where(ls => ls.type != typeof(AppaTaskSynchronizationContext))
                                   .ToArray();

            var dest = new System.Collections.Generic.List<PlayerLoopSystem>(source);

            var index = dest.FindIndex(x => x.type.Name == "ScriptRunDelayedTasks");
            if (index == -1)
            {
                index = dest.FindIndex(x => x.type.Name == "AppaTaskLoopRunnerUpdate");
            }

            dest.Insert(index + 1, loop);

            return dest.ToArray();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            // capture default(unity) sync-context.
            unitySynchronizationContext = SynchronizationContext.Current;
            mainThreadId = Thread.CurrentThread.ManagedThreadId;
            try
            {
                applicationDataPath = Application.dataPath;
            }
            catch
            {
                // ignored
            }

#if UNITY_EDITOR && UNITY_2019_3_OR_NEWER

            // When domain reload is disabled, re-initialization is required when entering play mode; 
            // otherwise, pending tasks will leak between play mode sessions.
            var domainReloadDisabled = EditorSettings.enterPlayModeOptionsEnabled &&
                                       EditorSettings.enterPlayModeOptions.HasFlag(
                                           EnterPlayModeOptions.DisableDomainReload
                                       );
            if (!domainReloadDisabled && (runners != null))
            {
                return;
            }
#else
            if (runners != null) return; // already initialized
#endif

            var playerLoop =
#if UNITY_2019_3_OR_NEWER
                PlayerLoop.GetCurrentPlayerLoop();
#else
                PlayerLoop.GetDefaultPlayerLoop();
#endif

            Initialize(ref playerLoop);
        }

#if UNITY_EDITOR

        [InitializeOnLoadMethod]
        private static void InitOnEditor()
        {
            // Execute the play mode init method
            Init();

            // register an Editor update delegate, used to forcing playerLoop update
            EditorApplication.update += ForceEditorPlayerLoopUpdate;
        }

        private static void ForceEditorPlayerLoopUpdate()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode ||
                EditorApplication.isCompiling ||
                EditorApplication.isUpdating)
            {
                // Not in Edit mode, don't interfere
                return;
            }

            // EditorApplication.QueuePlayerLoopUpdate causes performance issue, don't call directly.
            // EditorApplication.QueuePlayerLoopUpdate();

            if (yielders != null)
            {
                foreach (var item in yielders)
                {
                    if (item != null)
                    {
                        item.Run();
                    }
                }
            }

            if (runners != null)
            {
                foreach (var item in runners)
                {
                    if (item != null)
                    {
                        item.Run();
                    }
                }
            }

            AppaTaskSynchronizationContext.Run();
        }

#endif

        private static int FindLoopSystemIndex(PlayerLoopSystem[] playerLoopList, Type systemType)
        {
            for (var i = 0; i < playerLoopList.Length; i++)
            {
                if (playerLoopList[i].type == systemType)
                {
                    return i;
                }
            }

            throw new Exception("Target PlayerLoopSystem does not found. Type:" + systemType.FullName);
        }

        private static void InsertLoop(
            PlayerLoopSystem[] copyList,
            InjectPlayerLoopTimings injectTimings,
            Type loopType,
            InjectPlayerLoopTimings targetTimings,
            int index,
            bool injectOnFirst,
            Type loopRunnerYieldType,
            Type loopRunnerType,
            PlayerLoopTiming playerLoopTiming)
        {
            var i = FindLoopSystemIndex(copyList, loopType);
            if ((injectTimings & targetTimings) == targetTimings)
            {
                copyList[i].subSystemList = InsertRunner(
                    copyList[i],
                    injectOnFirst,
                    loopRunnerYieldType,
                    yielders[index] = new ContinuationQueue(playerLoopTiming),
                    loopRunnerType,
                    runners[index] = new PlayerLoopRunner(playerLoopTiming)
                );
            }
            else
            {
                copyList[i].subSystemList = RemoveRunner(copyList[i], loopRunnerYieldType, loopRunnerType);
            }
        }

        public static void Initialize(
            ref PlayerLoopSystem playerLoop,
            InjectPlayerLoopTimings injectTimings = InjectPlayerLoopTimings.All)
        {
#if UNITY_2020_2_OR_NEWER
            yielders = new ContinuationQueue[16];
            runners = new PlayerLoopRunner[16];
#else
            yielders = new ContinuationQueue[14];
            runners = new PlayerLoopRunner[14];
#endif

            var copyList = playerLoop.subSystemList.ToArray();

            // Initialization
            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.Initialization),
                InjectPlayerLoopTimings.Initialization,
                0,
                true,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerYieldInitialization),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerInitialization),
                PlayerLoopTiming.Initialization
            );

            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.Initialization),
                InjectPlayerLoopTimings.LastInitialization,
                1,
                false,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastYieldInitialization),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastInitialization),
                PlayerLoopTiming.LastInitialization
            );

            // EarlyUpdate
            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.EarlyUpdate),
                InjectPlayerLoopTimings.EarlyUpdate,
                2,
                true,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerYieldEarlyUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerEarlyUpdate),
                PlayerLoopTiming.EarlyUpdate
            );

            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.EarlyUpdate),
                InjectPlayerLoopTimings.LastEarlyUpdate,
                3,
                false,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastYieldEarlyUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastEarlyUpdate),
                PlayerLoopTiming.LastEarlyUpdate
            );

            // FixedUpdate
            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.FixedUpdate),
                InjectPlayerLoopTimings.FixedUpdate,
                4,
                true,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerYieldFixedUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerFixedUpdate),
                PlayerLoopTiming.FixedUpdate
            );

            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.FixedUpdate),
                InjectPlayerLoopTimings.LastFixedUpdate,
                5,
                false,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastYieldFixedUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastFixedUpdate),
                PlayerLoopTiming.LastFixedUpdate
            );

            // PreUpdate
            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.PreUpdate),
                InjectPlayerLoopTimings.PreUpdate,
                6,
                true,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerYieldPreUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerPreUpdate),
                PlayerLoopTiming.PreUpdate
            );

            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.PreUpdate),
                InjectPlayerLoopTimings.LastPreUpdate,
                7,
                false,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastYieldPreUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastPreUpdate),
                PlayerLoopTiming.LastPreUpdate
            );

            // Update
            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.Update),
                InjectPlayerLoopTimings.Update,
                8,
                true,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerYieldUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerUpdate),
                PlayerLoopTiming.Update
            );

            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.Update),
                InjectPlayerLoopTimings.LastUpdate,
                9,
                false,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastYieldUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastUpdate),
                PlayerLoopTiming.LastUpdate
            );

            // PreLateUpdate
            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.PreLateUpdate),
                InjectPlayerLoopTimings.PreLateUpdate,
                10,
                true,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerYieldPreLateUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerPreLateUpdate),
                PlayerLoopTiming.PreLateUpdate
            );

            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.PreLateUpdate),
                InjectPlayerLoopTimings.LastPreLateUpdate,
                11,
                false,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastYieldPreLateUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastPreLateUpdate),
                PlayerLoopTiming.LastPreLateUpdate
            );

            // PostLateUpdate
            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.PostLateUpdate),
                InjectPlayerLoopTimings.PostLateUpdate,
                12,
                true,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerYieldPostLateUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerPostLateUpdate),
                PlayerLoopTiming.PostLateUpdate
            );

            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.PostLateUpdate),
                InjectPlayerLoopTimings.LastPostLateUpdate,
                13,
                false,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastYieldPostLateUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastPostLateUpdate),
                PlayerLoopTiming.LastPostLateUpdate
            );

#if UNITY_2020_2_OR_NEWER

            // TimeUpdate
            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.TimeUpdate),
                InjectPlayerLoopTimings.TimeUpdate,
                14,
                true,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerYieldTimeUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerTimeUpdate),
                PlayerLoopTiming.TimeUpdate
            );

            InsertLoop(
                copyList,
                injectTimings,
                typeof(PlayerLoopType.TimeUpdate),
                InjectPlayerLoopTimings.LastTimeUpdate,
                15,
                false,
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastYieldTimeUpdate),
                typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerLastTimeUpdate),
                PlayerLoopTiming.LastTimeUpdate
            );
#endif

            // Insert AppaTaskSynchronizationContext to Update loop
            var i = FindLoopSystemIndex(copyList, typeof(PlayerLoopType.Update));
            copyList[i].subSystemList = InsertAppaTaskSynchronizationContext(copyList[i]);

            playerLoop.subSystemList = copyList;
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        public static void AddAction(PlayerLoopTiming timing, IPlayerLoopItem action)
        {
            var runner = runners[(int)timing];
            if (runner == null)
            {
                ThrowInvalidLoopTiming(timing);
            }

            runner.AddAction(action);
        }

        private static void ThrowInvalidLoopTiming(PlayerLoopTiming playerLoopTiming)
        {
            throw new InvalidOperationException(
                "Target playerLoopTiming is not injected. Please check PlayerLoopHelper.Initialize. PlayerLoopTiming:" +
                playerLoopTiming
            );
        }

        public static void AddContinuation(PlayerLoopTiming timing, Action continuation)
        {
            var q = yielders[(int)timing];
            if (q == null)
            {
                ThrowInvalidLoopTiming(timing);
            }

            q.Enqueue(continuation);
        }

        // Diagnostics helper

#if UNITY_2019_3_OR_NEWER

        public static void DumpCurrentPlayerLoop()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("PlayerLoop List");
            foreach (var header in playerLoop.subSystemList)
            {
                sb.AppendFormat("------{0}------", header.type.Name);
                sb.AppendLine();
                foreach (var subSystem in header.subSystemList)
                {
                    sb.AppendFormat("{0}", subSystem.type.Name);
                    sb.AppendLine();

                    if (subSystem.subSystemList != null)
                    {
                        Debug.LogWarning("More Subsystem:" + subSystem.subSystemList.Length);
                    }
                }
            }

            Debug.Log(sb.ToString());
        }

        public static bool IsInjectedAppaTaskPlayerLoop()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            foreach (var header in playerLoop.subSystemList)
            {
                foreach (var subSystem in header.subSystemList)
                {
                    if (subSystem.type == typeof(AppaTaskLoopRunners.AppaTaskLoopRunnerInitialization))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

#endif
    }
}
