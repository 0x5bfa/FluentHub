namespace FluentHub.Octokit.Domain
{
    public class OctokitResult
    {
        public Exception Exception { get; }
        public DateTime? RateLimitReset { get; }

        public bool Tracked { get; set; }
        public bool Logged { get; set; }

        public bool IsRateLimited => RateLimitReset != null;
        public bool Faulted => IsRateLimited || Exception != null;

        protected OctokitResult(Exception exception, DateTime? rateLimitReset)
        {
            Exception = exception;
            RateLimitReset = rateLimitReset;
        }

        public OctokitResult<TOther> Convert<TOther>(TOther value = default)
        {
            return new OctokitResult<TOther>(value, Exception, RateLimitReset)
            {
                Tracked = Tracked,
                Logged = Logged,
            };
        }

        public static OctokitResult Ok()
        {
            return new OctokitResult(null, null);
        }

        public static OctokitResult Err(Exception ex)
        {
            return new OctokitResult(ex, null);
        }

        public static OctokitResult RateLimited(DateTime reset)
        {
            return new OctokitResult(null, reset);
        }

        public static OctokitResult<T> Ok<T>(T data = default)
        {
            return new OctokitResult<T>(data, null, null);
        }

        public static OctokitResult<T> Err<T>(Exception ex)
        {
            return new OctokitResult<T>(default, ex, null);
        }

        public static OctokitResult<T> RateLimited<T>(DateTime reset)
        {
            return new OctokitResult<T>(default, null, reset);
        }
    }

    public class OctokitResult<T> : OctokitResult
    {
        private readonly T _data;

        public OctokitResult(T data, Exception exception, DateTime? rateLimitReset) : base(exception, rateLimitReset)
        {
            _data = data;
        }

        public T Unwrap()
        {
            if (Faulted)
            {
                throw new InvalidOperationException("Cannot unwrap faulted result.");
            }

            return _data;
        }
    }
}
