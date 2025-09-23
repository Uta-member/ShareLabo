using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Domain.Aggregate.User;
using System.Collections.Immutable;

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
                return new Req()
                {
                    AccountIdStrOptional = dto.AccountIdStrOptional.ToMPOptional(),
                    LimitOptional = dto.LimitOptional.ToMPOptional(),
                    StartIndexOptional = dto.StartIndexOptional.ToMPOptional(),
                    TargetStatusesOptional = dto.TargetStatusesOptional.ToMPOptional(x => x.ToList()),
                    UserIdStrOptional = dto.UserIdStrOptional.ToMPOptional(),
                    UserNameStrOptional = dto.UserNameStrOptional.ToMPOptional(),
                };
            }

            public IUserSummariesSearchQueryService.Req ToDTO()
            {
                return new IUserSummariesSearchQueryService.Req()
                {
                    AccountIdStrOptional = AccountIdStrOptional.ToOptional(),
                    LimitOptional = LimitOptional.ToOptional(),
                    StartIndexOptional = StartIndexOptional.ToOptional(),
                    TargetStatusesOptional = TargetStatusesOptional.ToOptional(x => x.ToImmutableList()),
                    UserIdStrOptional = UserIdStrOptional.ToOptional(),
                    UserNameStrOptional = UserNameStrOptional.ToOptional(),
                };
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
                return new Res()
                {
                    Users = dto.Users.Select(x => MPUserSummaryReadModel.FromDTO(x)).ToList(),
                };
            }

            public IUserSummariesSearchQueryService.Res ToDTO()
            {
                return new IUserSummariesSearchQueryService.Res()
                {
                    Users = Users.Select(x => x.ToDTO()).ToImmutableList(),
                };
            }

            [Key(0)]
            public required List<MPUserSummaryReadModel> Users { get; init; }
        }
    }
}
