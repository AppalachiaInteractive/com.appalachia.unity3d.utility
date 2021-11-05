using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.EditorCoroutines.Editor;
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
        private readonly OnError _onError;
        private readonly OnSuccess _onSuccess;
        private readonly string _processKey;
        private Action _coroutineQuit;
        private bool _isCancellationInProgress;
        private bool _isExecuting;
        private bool _wasAutoCancelled;
        private bool _wasCancelled;

        private bool _wasExecuted;
        private Coroutine _coroutine;
        private double _startTime;
#if UNITY_EDITOR
        private EditorCoroutine _editorCoroutine;
#endif
        private Exception _exception;

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

        private bool ShouldAutoCancel
        {
            get
            {
#if UNITY_EDITOR

                if (UnityEditor.EditorApplication.isCompiling)
                {
                    return true;
                }

                if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    return true;
                }

#endif

                return false;
            }
        }

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

#if UNITY_EDITOR

        public void ExecuteAsEditorCoroutine()
        {
            _editorCoroutine =
                EditorCoroutineUtility.StartCoroutine(Execute(), SafeCoroutineManager.instance);

            Application.quitting += AutoCancel;
        }
#endif

        public void ExecuteAsRuntimeCoroutine()
        {
            _coroutine = SafeCoroutineManager.instance.StartCoroutine(Execute());

            Application.quitting += AutoCancel;
        }

        public void ExecuteSynchronous()
        {
            Application.quitting += AutoCancel;

            var enumeration = Execute();

            while (enumeration.MoveNext())
            {
            }
        }

        private void AutoCancel()
        {
            _isCancellationInProgress = true;
            _wasCancelled = true;
            _wasAutoCancelled = true;

#if UNITY_EDITOR
            if (_editorCoroutine != null)
            {
                EditorCoroutineUtility.StopCoroutine(_editorCoroutine);
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
                    if (ShouldAutoCancel)
                    {
                        AutoCancel();
                    }

                    if (_isCancellationInProgress)
                    {
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
                        Debug.LogError($"Coroutine [{ProcessKey}] threw ANOTHER an exception! [{ex2.Message}]");
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

        private void OnCompleteInternal()
        {
            Application.quitting -= AutoCancel;
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
    }
}
