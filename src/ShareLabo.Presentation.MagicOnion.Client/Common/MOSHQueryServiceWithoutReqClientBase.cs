using CSStack.TADA;
using CSStack.TADA.MagicOnionHelper.Abstractions;
using CSStack.TADA.MagicOnionHelper.Client;
using Grpc.Core;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public abstract class MOSHQueryServiceWithoutReqClientBase<TMOQueryServiceWithoutReq, TMPRes, TRes>
        : MOQueryServiceWithoutReqClientBase<TMOQueryServiceWithoutReq, TMPRes, TRes>
        where TMOQueryServiceWithoutReq : IMOQueryServiceWithoutReq<TMOQueryServiceWithoutReq, TMPRes, TRes>
        where TMPRes : IMPDTO<TRes, TMPRes>
        where TRes : IQueryServiceDTO
    {
        protected MOSHQueryServiceWithoutReqClientBase(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }

        public override async ValueTask<TRes> ExecuteAsyncCore(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.ExecuteAsyncCore(cancellationToken);
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