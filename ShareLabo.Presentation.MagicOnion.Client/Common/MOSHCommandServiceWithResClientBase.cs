using CSStack.TADA;
using CSStack.TADA.MagicOnionHelper.Abstractions;
using CSStack.TADA.MagicOnionHelper.Client;
using Grpc.Core;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public abstract class MOSHCommandServiceWithResClientBase<TMOCommandServiceWithRes, TMPReq, TReq, TMPRes, TRes>
        : MOCommandServiceWithResClientBase<TMOCommandServiceWithRes, TMPReq, TReq, TMPRes, TRes>
        where TMOCommandServiceWithRes : IMOCommandServiceWithRes<TMOCommandServiceWithRes, TMPReq, TReq, TMPRes, TRes>
        where TMPReq : IMPDTO<TReq, TMPReq>
        where TReq : ICommandServiceDTO
        where TMPRes : IMPDTO<TRes, TMPRes>
        where TRes : ICommandServiceDTO
    {
        public MOSHCommandServiceWithResClientBase(IMOClientChannelFactory channelFactory)
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