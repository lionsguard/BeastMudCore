﻿using System.Threading;
using System.Threading.Tasks;

namespace Beast
{
    public class AsyncManualResetEvent
    {
        volatile TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        public Task WaitAsync() { return _tcs.Task; }

        public void Set() { _tcs.TrySetResult(true); }

        public void Reset()
        {
            while (true)
            {
                var tcs = _tcs;
                if (!tcs.Task.IsCompleted ||
                    Interlocked.CompareExchange(ref _tcs, new TaskCompletionSource<bool>(), tcs) == tcs)
                    return;
            }
        }
    }
}
