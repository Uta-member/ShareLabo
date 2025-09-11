using CSStack.TADA;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.Aggregate.TimeLine
{
    public sealed class TimeLineEntity : EntityBase<TimeLineEntity, TimeLineId>
    {
        private TimeLineEntity(TimeLineId id, TimeLineName name, UserId ownerId, ImmutableList<UserId> filterMembers)
        {
            Id = id;
            Name = name;
            OwnerId = ownerId;
            FilterMembers = filterMembers;
        }

        public static TimeLineEntity Create(CreateCommand command)
        {
            var entity = new TimeLineEntity(
                command.Id,
                command.Name,
                command.OwnerId,
                command.FilterMembers);
            entity.Validate();
            return entity;
        }

        public static TimeLineEntity Reconstruct(ReconstructCommand command)
        {
            var entity = new TimeLineEntity(
                command.Id,
                command.Name,
                command.OwnerId,
                command.FilterMembers);
            return entity;
        }

        public TimeLineEntity Update(UpdateCommand command)
        {
            var entity = new TimeLineEntity(
                Id,
                command.NameOptional.GetValue(Name),
                OwnerId,
                command.FilterMembersOptional.GetValue(FilterMembers));
            entity.Validate();
            return entity;
        }

        public override void Validate()
        {
            var validateHelper = new KeyedValidateHelper<ValidateTypeEnum>();
            validateHelper.Add(ValidateTypeEnum.Id, () => Id.Validate());
            validateHelper.Add(ValidateTypeEnum.Name, () => Name.Validate());
            validateHelper.ExecuteValidateWithThrowException();
        }

        public ImmutableList<UserId> FilterMembers { get; }

        public TimeLineId Id { get; }

        public override TimeLineId Identifier => Id;

        public TimeLineName Name { get; }

        public UserId OwnerId { get; }

        public enum ValidateTypeEnum
        {
            Id,
            Name
        }

        public sealed record CreateCommand
        {
            public required ImmutableList<UserId> FilterMembers { get; init; }

            public required TimeLineId Id { get; init; }

            public required TimeLineName Name { get; init; }

            public required UserId OwnerId { get; init; }
        }

        public sealed record ReconstructCommand
        {
            public required ImmutableList<UserId> FilterMembers { get; init; }

            public required TimeLineId Id { get; init; }

            public required TimeLineName Name { get; init; }

            public required UserId OwnerId { get; init; }
        }

        public sealed record UpdateCommand
        {
            public Optional<ImmutableList<UserId>> FilterMembersOptional
            {
                get;
                init;
            } = Optional<ImmutableList<UserId>>.Empty;

            public Optional<TimeLineName> NameOptional { get; init; } = Optional<TimeLineName>.Empty;
        }
    }
}
