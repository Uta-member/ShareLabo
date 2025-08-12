using CSStack.TADA;
using CSStack.TADA.MagicOnionHelper.Abstractions;
using CSStack.TADA.MagicOnionHelper.Client;
using Grpc.Core;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public abstract class MOSHQueryServiceClientBase<TMOQueryService, TMPReq, TReq, TMPRes, TRes>
        : MOQueryServiceClientBase<TMOQueryService, TMPReq, TReq, TMPRes, TRes>
        where TMOQueryService : IMOQueryService<TMOQueryService, TMPReq, TReq, TMPRes, TRes>
        where TMPReq : IMPDTO<TReq, TMPReq>
        where TReq : IQueryServiceDTO
        where TMPRes : IMPDTO<TRes, TMPRes>
        where TRes : IQueryServiceDTO
    {
        protected MOSHQueryServiceClientBase(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }

        public override async ValueTask<TRes> ExecuteAsyncCore(TReq req, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.ExecuteAsyncCore(req, cancellationToken);
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