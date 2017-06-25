using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beast
{
    public class AsyncAutoResetEvent : IDisposable
    {
        protected internal bool IsSignaled { get { return _signaled; } }

        readonly static Task _completed = Task.FromResult(true);
        readonly Queue<TaskCompletionSource<bool>> _waits = new Queue<TaskCompletionSource<bool>>();
        bool _signaled;

        public virtual Task WaitAsync()
        {
            lock (_waits)
            {
                if (_signaled)
                {
                    _signaled = false;
                    return _completed;
                }
                else
                {
                    var tcs = new TaskCompletionSource<bool>();
                    _waits.Enqueue(tcs);
                    return tcs.Task;
                }
            }
        }
        protected Task EnqueueWait()
        {
            lock (_waits)
            {
                var tcs = new TaskCompletionSource<bool>();
                _waits.Enqueue(tcs);
                return tcs.Task;
            }
        }

        public virtual void Set()
        {
            TaskCompletionSource<bool> toRelease = null;
            lock (_waits)
            {
                if (_waits.Count > 0)
                {
                    toRelease = _waits.Dequeue();
                }
                else if (!_signaled)
                {
                    _signaled = true;
                }
            }
            if (toRelease != null)
            {
                toRelease.TrySetResult(true);
            }
        }

        public virtual void Dispose()
        {
            lock (_waits)
            {
                _waits.Clear();
            }
        }
    }
}
