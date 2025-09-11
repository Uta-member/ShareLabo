using CSStack.TADA;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Follow
{
    public sealed class FollowEntity : EntityBase<FollowEntity, FollowIdentifier>
    {
        private FollowEntity(FollowIdentifier followId, DateTime followStartDateTime)
        {
            FollowId = followId;
            FollowStartDateTime = followStartDateTime;
        }

        public static FollowEntity Create(CreateCommand command)
        {
            var entity = new FollowEntity(
                command.FollowId,
                command.FollowStartDateTime);
            entity.Validate();
            return entity;
        }

        public static FollowEntity Reconstruct(ReconstructCommand command)
        {
            var entity = new FollowEntity(
                command.FollowId,
                command.FollowStartDateTime);
            return entity;
        }

        public override void Validate()
        {
        }

        public FollowIdentifier FollowId { get; init; }

        public DateTime FollowStartDateTime { get; init; }

        public override FollowIdentifier Identifier => FollowId;

        public sealed record CreateCommand
        {
            public required FollowIdentifier FollowId { get; init; }

            public required DateTime FollowStartDateTime { get; init; }
        }

        public sealed record ReconstructCommand
        {
            public required FollowIdentifier FollowId { get; init; }

            public required DateTime FollowStartDateTime { get; init; }
        }
    }
}
