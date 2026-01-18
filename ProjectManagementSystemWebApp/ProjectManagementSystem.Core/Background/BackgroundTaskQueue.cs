using System.Threading.Channels;
using ProjectManagementSystem.Services.Contracts;

namespace ProjectManagementSystem.Services.Services.Background
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, Task>> _queue =
         Channel.CreateUnbounded<Func<CancellationToken, Task>>();

        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null) throw new Exception("No Task Enqueued.");
            _queue.Writer.TryWrite(workItem);
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
