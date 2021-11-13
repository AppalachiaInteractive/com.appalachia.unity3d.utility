using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Appalachia.Utility.Logging;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Appalachia.Utility.Execution
{
    public class SafeCoroutineWrapper
    {
        public delegate void OnError(Exception ex);

        public delegate void OnSuccess();

        public SafeCoroutineWrapper(
            IEnumerator enumerator,
            string processKey = null,
            OnSuccess onSuccess = null,
            OnError onError = null,
            double timeoutSeconds = 0.0,
            [CallerMemberName] string callerMemberName = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            _enumerator = enumerator;

            if (processKey == null)
            {
                _processKey = $"{callerFilePath} > {callerMemberName}:{callerLineNumber}";
            }
            else
            {
                _processKey = processKey;
            }

            _onSuccess = onSuccess;
            _onError = onError;
            _timeoutSeconds = timeoutSeconds;

            SafeCoroutineManager.instance.Add(this);
        }

        private readonly double _timeoutSeconds;
        private readonly IEnumerator _enumerator;
        private readonly string _processKey;
        private bool _isCancellationInProgress;
        private bool _isExecuting;
        private bool _wasAutoCancelled;
        private bool _wasCancelled;
        private bool _rejectsCancellation;

        private bool _wasExecuted;
        private Coroutine _coroutine;
        private double _startTime;
#if UNITY_EDITOR
        private Unity.EditorCoroutines.Editor.EditorCoroutine _editorCoroutine;
#endif
        private Exception _exception;
        private OnError _onError;
        private OnSuccess _onSuccess;

        public bool HadErrors => exception != null;

        public bool IsCancellationInProgress => _isCancellationInProgress;
        public bool IsExecuting => _isExecuting;

        public bool WasAutoCancelled => _wasAutoCancelled;

        public bool WasCancelled => _wasCancelled;
        public double ExecutionTime => Time.realtimeSinceStartupAsDouble - _startTime;
        public double StartTime => _startTime;

        public double TimeoutSeconds => _timeoutSeconds;
        public Exception exception => _exception;

        public IEnumerator enumerator => _enumerator;

        public string ProcessKey => _processKey;

        public void Cancel()
        {
            _isCancellationInProgress = true;
        }

        public void ExecuteAsCoroutine()
        {
#if UNITY_EDITOR
            ExecuteAsEditorCoroutine();
#else
            ExecuteAsRuntimeCoroutine();
#endif
        }

        public void ExecuteAsRuntimeCoroutine()
        {
            _coroutine = SafeCoroutineManager.instance.StartCoroutine(Execute());

            RegisterAutoCancelEvents();
        }

        public void ExecuteSynchronous()
        {
            RegisterAutoCancelEvents();

            var enumeration = Execute();

            while (enumeration.MoveNext())
            {
            }
        }

        public SafeCoroutineWrapper OnComplete(OnSuccess onSuccess)
        {
            _onSuccess = onSuccess;

            return this;
        }

        public SafeCoroutineWrapper OnFailure(OnError onError)
        {
            _onError = onError;

            return this;
        }

        private void AutoCancel(string reason)
        {
            if (_isCancellationInProgress)
            {
                return;
            }
            
            AppaLog.Warn($"Auto-cancelling a coroutine [{ProcessKey}] due to: [{reason}].");

            _isCancellationInProgress = true;
            _wasCancelled = true;
            _wasAutoCancelled = true;

#if UNITY_EDITOR
            if (_editorCoroutine != null)
            {
                Unity.EditorCoroutines.Editor.EditorCoroutineUtility.StopCoroutine(_editorCoroutine);
            }
#endif
            if (_coroutine != null)
            {
                SafeCoroutineManager.instance.StopCoroutine(_coroutine);
            }
        }

        private void CheckTimeout()
        {
            if (ShouldTimeout())
            {
                var message = $"| {ExecutionTime:F2} / {TimeoutSeconds:F2} | {ProcessKey}";
                throw new TimeoutException(message);
            }
        }

        private IEnumerator Execute()
        {
            if (_isExecuting)
            {
                throw new NotSupportedException("Already executing.");
            }

            if (_wasExecuted)
            {
                throw new NotSupportedException("Already executed.");
            }

            _wasExecuted = true;
            _isExecuting = true;
            _startTime = Time.realtimeSinceStartupAsDouble;

            while (true)
            {
                try
                {
                    if (_isCancellationInProgress)
                    {
                        if (_rejectsCancellation)
                        {
                            AppaLog.Warn($"Coroutine [{ProcessKey}] rejects cancellation and will finish.");
                            while (enumerator.MoveNext())
                            {
                            }
                            
                            AppaLog.Info($"Coroutine [{ProcessKey}] has finished after rejecting cancellation.");
                        }
                        
                        _wasCancelled = true;
                        _isCancellationInProgress = false;
                        break;
                    }

                    // DANGER

                    CheckTimeout();

                    if (!enumerator.MoveNext())
                    {
                        break;
                    }

                    // NO MORE DANGER
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Coroutine [{ProcessKey}] threw an exception! [{ex.Message}]");
                    Debug.LogException(ex);

                    _exception = ex;

                    try
                    {
                        _onError?.Invoke(_exception);
                    }
                    catch (Exception ex2)
                    {
                        Debug.LogError(
                            $"Coroutine [{ProcessKey}] threw ANOTHER an exception! [{ex2.Message}]"
                        );
                        Debug.LogException(ex2);
                    }

                    break;
                }

                yield return enumerator.Current;
            }

            try
            {
                if (_exception == null)
                {
                    _onSuccess?.Invoke();
                }
            }
            finally
            {
                _isExecuting = false;

                OnCompleteInternal();
                SafeCoroutineManager.instance.Completed(this);
            }
        }

        private void OnApplicationQuit()
        {
            AutoCancel("Application Quit");
        }

        private void OnCompilationFinished(object context)
        {
            AutoCancel("Compilation Finished");
        }

        private void OnCompilationStarted(object context)
        {
            AutoCancel("Compilation Started");
        }

        private void OnCompleteInternal()
        {
            UnregisterAutoCancelEvents();
        }

        private void RegisterAutoCancelEvents()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            UnityEditor.EditorApplication.quitting += OnApplicationQuit;
            UnityEditor.Compilation.CompilationPipeline.compilationStarted += OnCompilationStarted;
            UnityEditor.Compilation.CompilationPipeline.compilationFinished += OnCompilationFinished;
#endif
            Application.quitting += OnApplicationQuit;
        }

        private bool ShouldTimeout()
        {
            if (_timeoutSeconds == 0.0)
            {
                return false;
            }

            if (ExecutionTime > _timeoutSeconds)
            {
                if (!Debugger.IsAttached)
                {
                    return true;
                }
            }

            return false;
        }

        private void UnregisterAutoCancelEvents()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            UnityEditor.EditorApplication.quitting -= OnApplicationQuit;
            UnityEditor.Compilation.CompilationPipeline.compilationStarted -= OnCompilationStarted;
            UnityEditor.Compilation.CompilationPipeline.compilationFinished -= OnCompilationFinished;
#endif
            Application.quitting -= OnApplicationQuit;
        }

        internal void SetNonCancellable()
        {
            _rejectsCancellation = true;
        }
        
#if UNITY_EDITOR

        private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange obj)
        {
            AutoCancel(obj.ToString());
        }

        public void ExecuteAsEditorCoroutine()
        {
            _editorCoroutine =
                Unity.EditorCoroutines.Editor.EditorCoroutineUtility.StartCoroutine(
                    Execute(),
                    SafeCoroutineManager.instance
                );

            RegisterAutoCancelEvents();
        }

        [UnityEditor.MenuItem(
            PKG.Menu.Appalachia.Tasks.Base + "Cancel All",
            true,
            priority = PKG.Menu.Appalachia.Tools.Priority
        )]
        private static bool CancelAllValidate()
        {
            return SafeCoroutineManager.instance.Count > 0;
        }

        [UnityEditor.MenuItem(
            PKG.Menu.Appalachia.Tasks.Base + "Cancel All",
            priority = PKG.Menu.Appalachia.Tools.Priority
        )]
        private static void CancelAll()
        {
           Debug.LogWarning("Cancelling all executing coroutines from the Editor menu.");

            var routines = SafeCoroutineManager.instance.GetAll();

            foreach (var routine in routines)
            {
                routine.Cancel();
            }

            routines.Clear();
        }
#endif
    }
}
