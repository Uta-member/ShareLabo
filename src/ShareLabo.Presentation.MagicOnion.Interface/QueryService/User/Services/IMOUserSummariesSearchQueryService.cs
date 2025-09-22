using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOUserSummariesSearchQueryService
        : IMOQueryService<IMOUserSummariesSearchQueryService, IMOUserSummariesSearchQueryService.Req, IUserSummariesSearchQueryService.Req, IMOUserSummariesSearchQueryService.Res, IUserSummariesSearchQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IUserSummariesSearchQueryService.Req, Req>
        {
            public static Req FromDTO(IUserSummariesSearchQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IUserSummariesSearchQueryService.Req ToDTO()
            {
                return this.Adapt<IUserSummariesSearchQueryService.Req>();
            }

            [Key(0)]
            public required MPOptional<string> AccountIdStrOptional { get; init; }

            [Key(1)]
            public required MPOptional<int> LimitOptional { get; init; }

            [Key(2)]
            public required MPOptional<int> StartIndexOptional { get; init; }

            [Key(3)]
            public required MPOptional<List<UserEntity.StatusEnum>> TargetStatusesOptional
            {
                get;
                init;
            }

            [Key(4)]
            public required MPOptional<string> UserIdStrOptional { get; init; }

            [Key(5)]
            public required MPOptional<string> UserNameStrOptional { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserSummariesSearchQueryService.Res, Res>
        {
            public static Res FromDTO(IUserSummariesSearchQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }

            public IUserSummariesSearchQueryService.Res ToDTO()
            {
                return this.Adapt<IUserSummariesSearchQueryService.Res>();
            }

            [Key(0)]
            public required List<MPUserSummaryReadModel> Users { get; init; }
        }
    }
}
