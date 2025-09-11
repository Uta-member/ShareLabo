using CSStack.TADA;
using CSStack.TADA.MagicOnionHelper.Abstractions;
using CSStack.TADA.MagicOnionHelper.Client;
using Grpc.Core;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public abstract class MOSHCommandServiceClientBase<TMOCommandService, TMOReq, TReq>
        : MOCommandServiceClientBase<TMOCommandService, TMOReq, TReq>
        where TMOCommandService : IMOCommandService<TMOCommandService, TMOReq, TReq>
        where TMOReq : IMPDTO<TReq, TMOReq>
        where TReq : ICommandServiceDTO
    {
        protected MOSHCommandServiceClientBase(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }

        public override async ValueTask ExecuteAsyncCore(TReq req, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.ExecuteAsyncCore(req, cancellationToken);
            }
            catch(RpcException ex)
            {
                var typeMatchException = ex.GetExceptionTypeMatch();
                if(typeMatchException != null)
                {
                    throw typeMatchException;
                }
                throw;
            }
        }
    }
}