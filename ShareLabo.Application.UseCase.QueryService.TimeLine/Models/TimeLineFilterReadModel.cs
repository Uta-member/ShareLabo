namespace ShareLabo.Application.UseCase.QueryService.TimeLine
{
    public sealed record TimeLineFilterReadModel
    {
        public required string UserId { get; init; }

        public required string UserName { get; init; }
    }
}
