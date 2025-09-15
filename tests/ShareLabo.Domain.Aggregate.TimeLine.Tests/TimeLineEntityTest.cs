using CSStack.TADA;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.Aggregate.TimeLine.Tests
{
    public sealed class TimeLineEntityTest
    {
        [Fact]
        public void Create_Filter同一オブジェクト存在()
        {
            var timeLineId = TimeLineId.Create(new string('a', 8));
            var timeLineName = TimeLineName.Create(new string('a', 8));
            var ownerId = UserId.Reconstruct(new string('a', 8));

            ImmutableList<UserId> timeLineFilters = [];
            for(var i = 0; i < 50; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
                if(i == 0)
                {
                    timeLineFilters = timeLineFilters.Add(filterId);
                }
            }

            var ex = Assert.Throws<KeyedMultiReasonException<TimeLineEntity.ValidateTypeEnum>>(
                () => TimeLineEntity.Create(
                    new TimeLineEntity.CreateCommand()
                {
                    FilterMembers = timeLineFilters,
                    Id = timeLineId,
                    Name = timeLineName,
                    OwnerId = ownerId,
                }));
            Assert.Contains(
                ex.Exceptions,
                x => x.Key == TimeLineEntity.ValidateTypeEnum.Filter && x.Value is ValueObjectInvalidException);
        }

        [Fact]
        public void Create_正常()
        {
            var timeLineId = TimeLineId.Create(new string('a', 8));
            var timeLineName = TimeLineName.Create(new string('a', 8));
            var ownerId = UserId.Reconstruct(new string('a', 8));

            ImmutableList<UserId> timeLineFilters = [];
            for(var i = 0; i < 50; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
            }

            var entity = TimeLineEntity.Create(
                new TimeLineEntity.CreateCommand()
                {
                    FilterMembers = timeLineFilters,
                    Id = timeLineId,
                    Name = timeLineName,
                    OwnerId = ownerId,
                });

            Assert.Equal(timeLineId, entity.Id);
            Assert.Equal(timeLineName, entity.Name);
            Assert.Equal(timeLineFilters, entity.FilterMembers);
            Assert.Equal(ownerId, entity.OwnerId);

            timeLineName = TimeLineName.Create(new string('b', 8));
            for(var i = 50; i < 100; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
            }

            entity = entity.Update(
                new TimeLineEntity.UpdateCommand()
                {
                    FilterMembersOptional = timeLineFilters,
                    NameOptional = timeLineName,
                });
            Assert.Equal(timeLineId, entity.Id);
            Assert.Equal(timeLineName, entity.Name);
            Assert.Equal(timeLineFilters, entity.FilterMembers);
            Assert.Equal(ownerId, entity.OwnerId);
        }

        [Fact]
        public void Reconstruct_正常()
        {
            var timeLineId = TimeLineId.Reconstruct(new string('a', 8));
            var timeLineName = TimeLineName.Reconstruct(new string('a', 8));
            var ownerId = UserId.Reconstruct(new string('a', 8));

            ImmutableList<UserId> timeLineFilters = [];
            for(var i = 0; i < 50; i++)
            {
                var filterId = UserId.Reconstruct($"filter-user-{i}");
                timeLineFilters = timeLineFilters.Add(filterId);
            }

            var entity = TimeLineEntity.Reconstruct(
                new TimeLineEntity.ReconstructCommand()
                {
                    FilterMembers = timeLineFilters,
                    Id = timeLineId,
                    Name = timeLineName,
                    OwnerId = ownerId,
                });

            Assert.Equal(timeLineId, entity.Id);
            Assert.Equal(timeLineName, entity.Name);
            Assert.Equal(timeLineFilters, entity.FilterMembers);
            Assert.Equal(ownerId, entity.OwnerId);
        }
    }
}
