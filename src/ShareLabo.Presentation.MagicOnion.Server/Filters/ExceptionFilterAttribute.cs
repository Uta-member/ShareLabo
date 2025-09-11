using Grpc.Core;
using MagicOnion.Server;

namespace ShareLabo.Presentation.MagicOnion.Server
{
    internal class ExceptionFilterAttribute : MagicOnionFilterAttribute
    {
        public override async ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                var metadata = new Metadata()
                {
                    { "exception-type", ex.GetType().FullName ?? string.Empty },
                    { "exception-message", ex.Message }
                };

                throw new RpcException(
                    new Status(StatusCode.Internal, "An error occurred"),
                    metadata);
            }
        }
    }
}
