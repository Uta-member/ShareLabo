using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.Authentication
{
    public sealed record UserAccountModel
    {
        public required StatusEnum Status { get; init; }

        public required UserAccountId UserAccountId { get; init; }

        public required UserId UserId { get; init; }

        public enum StatusEnum
        {
            Enabled = 0,
            Disabled = 1,
            Deleted = 2,
        }
    }
}
