using CSStack.TADA;

namespace ShareLabo.Application.UseCase.QueryService.Post
{
    public interface IPostDetailFindByIdQueryService
        : IQueryService<IPostDetailFindByIdQueryService.Req, IPostDetailFindByIdQueryService.Res>
    {
        sealed record Req : IQueryServiceDTO
        {
            public required string PostId { get; init; }
        }

        sealed record Res : IQueryServiceDTO
        {
            public Optional<PostDetailReadModel> PostDetailOptional
            {
                get;
                init;
            } = Optional<PostDetailReadModel>.Empty;
        }
    }
}
