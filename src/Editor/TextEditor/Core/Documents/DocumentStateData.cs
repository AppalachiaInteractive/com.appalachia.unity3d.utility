using System;

namespace Appalachia.Utility.TextEditor.Core.Documents
{
    [Serializable]
    public class DocumentStateData
    {
        public bool pending;
        public bool failed;
        public Exception exception;

        public void Reset()
        {
            pending = true;
            failed = false;
            exception = null;
        }

        public void ResetNotPending()
        {
            pending = false;
            failed = false;
            exception = null;
        }

        public void Success()
        {
            pending = false;
            failed = false;
            exception = null;
        }

        public void Failed()
        {
            pending = true;
            failed = true;
            exception = null;
        }

        public void FailedNotPending()
        {
            pending = false;
            failed = true;
            exception = null;
        }

        public void Error(Exception ex)
        {
            pending = true;
            failed = true;
            exception = ex;
        }

        public void ErrorNotPending(Exception ex)
        {
            pending = false;
            failed = true;
            exception = ex;
        }
    }
}
