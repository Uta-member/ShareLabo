using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.Authentication.OAuthIntegration;
using ShareLabo.Application.UseCase.QueryService.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOUserDetailFindByOAuthIdentifierQueryService
        : IMOQueryService<IMOUserDetailFindByOAuthIdentifierQueryService, IMOUserDetailFindByOAuthIdentifierQueryService.Req, IUserDetailFindByOAuthIdentifierQueryService.Req, IMOUserDetailFindByOAuthIdentifierQueryService.Res, IUserDetailFindByOAuthIdentifierQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IUserDetailFindByOAuthIdentifierQueryService.Req, Req>
        {
            public static Req FromDTO(IUserDetailFindByOAuthIdentifierQueryService.Req dto)
            {
                return new Req
                {
                    OAuthIdentifier = dto.OAuthIdentifier,
                    OAuthType = dto.OAuthType,
                };
            }
            public IUserDetailFindByOAuthIdentifierQueryService.Req ToDTO()
            {
                return new IUserDetailFindByOAuthIdentifierQueryService.Req
                {
                    OAuthIdentifier = OAuthIdentifier,
                    OAuthType = OAuthType,
                };
            }

            [Key(0)]
            public required string OAuthIdentifier { get; init; }

            [Key(1)]
            public required OAuthType OAuthType { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserDetailFindByOAuthIdentifierQueryService.Res, Res>
        {
            public static Res FromDTO(IUserDetailFindByOAuthIdentifierQueryService.Res dto)
            {
                return new Res
                {
                    UserOptional = dto.UserOptional.ToMPOptional(x => MPUserDetailReadModel.FromDTO(x)),
                };
            }
            public IUserDetailFindByOAuthIdentifierQueryService.Res ToDTO()
            {
                return new IUserDetailFindByOAuthIdentifierQueryService.Res
                {
                    UserOptional = UserOptional.ToOptional(x => x.ToDTO()),
                };
            }

            [Key(0)]
            public required MPOptional<MPUserDetailReadModel> UserOptional { get; init; }
        }
    }
}
