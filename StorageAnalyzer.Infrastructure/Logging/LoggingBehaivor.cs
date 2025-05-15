using MediatR;
using Microsoft.Extensions.Logging;

namespace StorageAnalyzer.Infrastructure.Logging
{
    public class LoggingBehavior<TReq, TRes> : IPipelineBehavior<TReq, TRes>
    {
        private readonly ILogger<LoggingBehavior<TReq, TRes>> _log;
        public LoggingBehavior(ILogger<LoggingBehavior<TReq, TRes>> log) => _log = log;

        public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken ct)
        {
            _log.LogInformation("→ Handling {Request}", typeof(TReq).Name);
            var response = await next();
            _log.LogInformation("← Handled  {Request}", typeof(TReq).Name);
            return response;
        }
    }
}