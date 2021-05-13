using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AuraCpuSync
{
    class ServiceRunner
    {
        readonly Timer _timer;
        public ServiceRunner(Func<bool> callback, int duration)
        {
            _timer = new Timer(duration) { AutoReset = true };
            _timer.Stop();
            _timer.Elapsed += (sender, eventArgs) =>
            {
                callback();
            };
        }
        public bool Start() { _timer.Start(); return true; }
        public bool Stop() { _timer.Stop(); return true; }
    }
}
