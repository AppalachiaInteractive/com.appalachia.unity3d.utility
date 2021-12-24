#if UNITY_EDITOR
using System;
using System.Collections;
using System.Linq;
using Appalachia.Utility.Framing.Extensions;
using Appalachia.Utility.Logging;
using Appalachia.Utility.Strings;
using Unity.Profiling;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Appalachia.Utility.Framing
{
    [UnityEditor.InitializeOnLoad]
    internal static class FramingInputHandler
    {
        
        
        
        #region Profiling

        private const string _PRF_PFX = nameof(FramingInputHandler) + ".";

        private static readonly ProfilerMarker _PRF_ExecuteFrameCheck =
            new ProfilerMarker(_PRF_PFX + nameof(ExecuteFrameCheck));

        private static readonly ProfilerMarker _PRF_GetKeyPresses =
            new ProfilerMarker(_PRF_PFX + nameof(GetKeyPresses));

        #endregion

        static FramingInputHandler()
        {
            shouldCancel = false;
            
            Unity.EditorCoroutines.Editor.EditorCoroutineUtility.StartCoroutineOwnerless(
                ExecuteCheckEnumerator()
            );
        }

        public static bool shouldCancel;

        private static int exceptionCount = 0;
        
        private static IEnumerator ExecuteCheckEnumerator()
        {
            while (!shouldCancel)
            {
                bool framed = false;
                
                try
                {
                    framed = ExecuteFrameCheck();
                }
                catch (Exception ex)
                {
                    exceptionCount += 1;
                    AppaLog.Context.Utility.Error("Exception while framing", null, ex);

                    if (exceptionCount > 10)
                    {
                        shouldCancel = true;
                    }
                }

                if (framed)
                {
                    yield return new Unity.EditorCoroutines.Editor.EditorWaitForSeconds(.5f);                    
                }
                
                yield return new Unity.EditorCoroutines.Editor.EditorWaitForSeconds(.05f);
            }
        }

        [MenuItem(PKG.Menu.Appalachia.Tools.Base + "Reset Text Editing Flag", false, PKG.Priority)]
        public static void ResetTextEditingField()
        {
            UnityEditor.EditorGUIUtility.editingTextField = false;
        }
        
        private static bool ExecuteFrameCheck()
        {
            using (_PRF_ExecuteFrameCheck.Auto())
            {
                if (UnityEditor.EditorGUIUtility.editingTextField)
                {
                    return false;
                }
                
                var focused = UnityEditor.EditorWindow.focusedWindow;
                var mouseWindow = UnityEditor.EditorWindow.mouseOverWindow;

                var sceneFocused = (focused != null) && (focused.GetType() == typeof(UnityEditor.SceneView));
                var mouseOverScene = (mouseWindow != null) &&
                                     (mouseWindow.GetType() == typeof(UnityEditor.SceneView));
                
                if (!(sceneFocused || mouseOverScene))
                {
                    return false;
                }

                GetKeyPresses(
                    out var shouldProcess,
                    out var ctrlKey,
                    out var numpad0Key,
                    out var numpad1Key,
                    out var numpad2Key,
                    out var numpad3Key,
                    out var numpad4Key,
                    out var numpad5Key,
                    out var numpad6Key,
                    out var numpad7Key,
                    out var numpad8Key,
                    out var numpad9Key
                );

                if (!shouldProcess)
                {
                    return false;
                }

                var selection = UnityEditor.Selection.gameObjects;

                if (selection.Length == 0)
                {
                    return false;
                }

                var direction = FramingDirection.Current;
                var perspective = FramingPerspective.Current;

                if (numpad1Key)
                {
                    direction = ctrlKey ? FramingDirection.Back : FramingDirection.Front;
                }
                else if (numpad3Key)
                {
                    direction = ctrlKey ? FramingDirection.Right : FramingDirection.Left;
                }
                else if (numpad7Key)
                {
                    direction = ctrlKey ? FramingDirection.Bottom : FramingDirection.Top;
                }
                else if (numpad9Key)
                {
                    direction = FramingDirection.Opposite;
                }

                if (numpad5Key)
                {
                    perspective = FramingPerspective.Opposite;
                }

                var set = selection.ToFramedSet();

                FrameManager.CalculateFraming(set, FrameTarget.SceneView, direction, perspective, true);

                return true;
            }
        }

        private static void GetKeyPresses(
            out bool anyPressed,
            out bool ctrlKey,
            out bool numpad0Key,
            out bool numpad1Key,
            out bool numpad2Key,
            out bool numpad3Key,
            out bool numpad4Key,
            out bool numpad5Key,
            out bool numpad6Key,
            out bool numpad7Key,
            out bool numpad8Key,
            out bool numpad9Key)
        {
            using (_PRF_GetKeyPresses.Auto())
            {
                var keyboard = Keyboard.current;

                if (!keyboard.enabled)
                {
                    AppaLog.Context.Utility.Warn("Keyboard is not enabled!");
                }

                //LogState(keyboard);

                ctrlKey = keyboard.ctrlKey.isPressed;

                numpad0Key = keyboard.digit0Key.isPressed || keyboard.numpad0Key.isPressed;
                numpad1Key = keyboard.digit1Key.isPressed || keyboard.numpad1Key.isPressed;
                numpad2Key = keyboard.digit2Key.isPressed || keyboard.numpad2Key.isPressed;
                numpad3Key = keyboard.digit3Key.isPressed || keyboard.numpad3Key.isPressed;
                numpad4Key = keyboard.digit4Key.isPressed || keyboard.numpad4Key.isPressed;
                numpad5Key = keyboard.digit5Key.isPressed || keyboard.numpad5Key.isPressed;
                numpad6Key = keyboard.digit6Key.isPressed || keyboard.numpad6Key.isPressed;
                numpad7Key = keyboard.digit7Key.isPressed || keyboard.numpad7Key.isPressed;
                numpad8Key = keyboard.digit8Key.isPressed || keyboard.numpad8Key.isPressed;
                numpad9Key = keyboard.digit9Key.isPressed || keyboard.numpad9Key.isPressed;

                anyPressed = false;

                //anyPressed |= numpad0Key;
                anyPressed |= numpad1Key;

                //anyPressed |= numpad2Key;
                anyPressed |= numpad3Key;

                //anyPressed |= numpad4Key;
                anyPressed |= numpad5Key;

                //anyPressed |= numpad6Key;
                anyPressed |= numpad7Key;

                //anyPressed |= numpad8Key;
                anyPressed |= numpad9Key;
            }
        }

        private static void LogState(Keyboard keyboard, Func<ButtonControl, bool> stateCheck, string name)
        {
            if (stateCheck(keyboard.anyKey))
            {
                AppaLog.Context.Utility.Debug(ZString.Format("{0}.{1}", keyboard.anyKey.path, name));

                foreach (var k in keyboard.allKeys.Where(k => stateCheck(k)))
                {
                    AppaLog.Context.Utility.Debug(ZString.Format("{0}.{1}", k.path, name));
                }
            }
        }

        private static void LogState(Keyboard keyboard)
        {
            LogState(keyboard, control => control.wasPressedThisFrame,  "wasPressedThisFrame");
            LogState(keyboard, control => control.wasReleasedThisFrame, "wasReleasedThisFrame");
            LogState(keyboard, control => control.isPressed,            "isPressed");
        }
    }
}

#endif