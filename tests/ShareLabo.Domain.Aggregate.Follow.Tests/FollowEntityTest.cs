using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Follow.Tests
{
    public sealed class FollowEntityTest
    {
        [Fact]
        public void Create_正常()
        {
            var fromId = UserId.Create(new string('a', 8));
            var toId = UserId.Create(new string('b', 8));
            var followId = FollowIdentifier.Create(fromId, toId);
            var followStartDateTime = new DateTime(2025, 9, 1);

            var createCommand = new FollowEntity.CreateCommand()
            {
                FollowId = followId,
                FollowStartDateTime = followStartDateTime,
            };

            var entity = FollowEntity.Create(createCommand);

            Assert.Equal(followId, entity.FollowId);
            Assert.Equal(followStartDateTime, entity.FollowStartDateTime);
        }

        [Fact]
        public void Reconstruct_正常()
        {
            var fromId = UserId.Reconstruct(new string('a', 7));
            var toId = UserId.Reconstruct(new string('b', 7));
            var followId = FollowIdentifier.Reconstruct(fromId, toId);
            var followStartDateTime = new DateTime(2025, 9, 1);

            var createCommand = new FollowEntity.ReconstructCommand()
            {
                FollowId = followId,
                FollowStartDateTime = followStartDateTime,
            };

            var entity = FollowEntity.Reconstruct(createCommand);

            Assert.Equal(followId, entity.FollowId);
            Assert.Equal(followStartDateTime, entity.FollowStartDateTime);
        }
    }
}
