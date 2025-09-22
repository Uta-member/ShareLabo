using CSStack.TADA;

namespace ShareLabo.Application.Authentication.OAuthIntegration
{
    public sealed class OAuthIntegrationService<TSession> : IOAuthIntegrationService<TSession>
        where TSession : IDisposable
    {
        private readonly IOAuthIntegrationRepository<TSession> _oAuthIntegrationRepository;

        public OAuthIntegrationService(IOAuthIntegrationRepository<TSession> oAuthIntegrationRepository)
        {
            _oAuthIntegrationRepository = oAuthIntegrationRepository;
        }

        public async ValueTask AddAsync(
            IOAuthIntegrationService<TSession>.AddReq req,
            CancellationToken cancellationToken = default)
        {
            var targetEntityOptional = await _oAuthIntegrationRepository.FindByUserIdAsync(
                req.Session,
                req.UserId,
                cancellationToken);
            if(!targetEntityOptional.TryGetValue(out var targetEntity))
            {
                throw new ObjectNotFoundException();
            }
            targetEntity = new OAuthIntegrationEntity(targetEntity.UserId, targetEntity.OAuthInfos.Add(req.OAuthInfo));
            await _oAuthIntegrationRepository.SaveAsync(req.Session, targetEntity, cancellationToken);
        }

        public async ValueTask CreateAsync(
            IOAuthIntegrationService<TSession>.CreateReq req,
            CancellationToken cancellationToken = default)
        {
            var targetEntityOptional = await _oAuthIntegrationRepository.FindByUserIdAsync(
                req.Session,
                req.UserId,
                cancellationToken);
            if(targetEntityOptional.TryGetValue(out var targetEntity))
            {
                throw new ObjectAlreadyExistException();
            }
            await _oAuthIntegrationRepository.SaveAsync(
                req.Session,
                new OAuthIntegrationEntity(req.UserId, req.OAuthInfos),
                cancellationToken);
        }

        public async ValueTask DeleteAsync(
            IOAuthIntegrationService<TSession>.DeleteReq req,
            CancellationToken cancellationToken = default)
        {
            var targetEntityOptional = await _oAuthIntegrationRepository.FindByUserIdAsync(
                req.Session,
                req.UserId,
                cancellationToken);
            if(!targetEntityOptional.TryGetValue(out var targetEntity))
            {
                throw new ObjectNotFoundException();
            }
            await _oAuthIntegrationRepository.DeleteAsync(req.Session, targetEntity, cancellationToken);
        }

        public async ValueTask<Optional<OAuthIntegrationEntity>> FindEntityByUserIdAsync(
            TSession session,
            string userId,
            CancellationToken cancellationToken = default)
        {
            return await _oAuthIntegrationRepository.FindByUserIdAsync(
                session,
                userId,
                cancellationToken);
        }

        public async ValueTask RemoveAsync(
            IOAuthIntegrationService<TSession>.RemoveReq req,
            CancellationToken cancellationToken = default)
        {
            var targetEntityOptional = await _oAuthIntegrationRepository.FindByUserIdAsync(
                req.Session,
                req.UserId,
                cancellationToken);
            if(!targetEntityOptional.TryGetValue(out var targetEntity))
            {
                throw new ObjectNotFoundException();
            }
            targetEntity = new OAuthIntegrationEntity(
                targetEntity.UserId,
                targetEntity.OAuthInfos.RemoveAll(x => x.OAuthType == req.OAuthType));
            await _oAuthIntegrationRepository.SaveAsync(req.Session, targetEntity, cancellationToken);
        }

        public async ValueTask UpdateAsync(
            IOAuthIntegrationService<TSession>.UpdateReq req,
            CancellationToken cancellationToken = default)
        {
            var targetEntityOptional = await _oAuthIntegrationRepository.FindByUserIdAsync(
                req.Session,
                req.UserId,
                cancellationToken);
            if(!targetEntityOptional.TryGetValue(out var targetEntity))
            {
                throw new ObjectNotFoundException();
            }
            targetEntity = new OAuthIntegrationEntity(
                targetEntity.UserId,
                targetEntity.OAuthInfos.RemoveAll(x => x.OAuthType == req.OAuthInfo.OAuthType).Add(req.OAuthInfo));
            await _oAuthIntegrationRepository.SaveAsync(req.Session, targetEntity, cancellationToken);
        }
    }
}
