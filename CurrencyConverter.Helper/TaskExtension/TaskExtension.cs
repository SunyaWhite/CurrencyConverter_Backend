using System;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.Helper.TaskExtension
{
    public static class TaskExtension
    {

        public async static Task<T> Otherwise<T>(this Task<T> task, Func<Task<T>> orTask) =>
            await task.ContinueWith(async innerTask =>
            {
                if(innerTask.Status != TaskStatus.Faulted)
                    return await orTask();
                return await innerTask;
            }).Unwrap();

        /*public async static Task<T> Retry<T>(Func<Task<T>> task, int reties, TimeSpan delay,
            CancellationToken cts = default(CancellationToken)) =>
            await task().ContinueWith(async innerTask =>
            {
                cts.ThrowIfCancellationRequested();
                if (innerTask.Status != TaskStatus.Faulted)
                    return await innerTask;
                else if (reties == 0)
                    throw innerTask.Exception.InnerException ?? new Exception("Task has failed");
                return await Retry<T>(task, reties - 1, delay);
            }).Unwrap();*/

        public async static Task<R> Bind<T, R>(this Task<T> task, Func<T, Task<R>> map) =>
            await map(await task.ConfigureAwait(false)).ConfigureAwait(false);

        public async static Task<T> Tap<T>(this Task<T> task, Action<T> act)
        {
            act(await task);
            return await task;
        }
    }
}