using System;

namespace CurrencyConverter.Helper.ResultType
{
    public struct Result<T>
    {
        public T Ok { get; }
        public Exception Error { get; }
        public bool IsOk
        {
            get { return Error == null; }
        }

        public bool IsFailed
        {
            get { return Error != null; }
        }
        
        public Result (T ok)
        {
            Ok = ok;
            Error = default(Exception);
        }

        public Result(Exception error)
        {
            Ok = default(T);
            Error = error;
        }

        public void Match(Action<T> okAction, Action<Exception> failureAction)
        {
            if (IsOk)
                okAction(this.Ok);
            else
                failureAction(this.Error);
        }

        public R Match<R>(Func<T, R> okAction, Func<Exception, R> failureAction)
        {
            if (IsOk)
                return okAction(Ok);
            return failureAction(Error);
        }
        
        public static implicit operator Result<T>(T ok) => new Result<T>(ok);

        public static implicit operator Result<T>(Exception error) => new Result<T>(error);

    }
}