using CSStack.TADA;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.Aggregate.Group
{
    public sealed class GroupEntity : EntityBase<GroupEntity, GroupId>
    {
        private GroupEntity(GroupId id, GroupName name, ImmutableList<UserId> members)
        {
            Id = id;
            Name = name;
            Members = members;
        }

        public static GroupEntity Create(CreateCommand command)
        {
            var entity = new GroupEntity(command.Id, command.Name, command.Members);
            entity.Validate();
            return entity;
        }

        public static GroupEntity Reconstruct(ReconstructCommand command)
        {
            return new GroupEntity(
                command.Id,
                command.Name,
                command.Members);
        }

        public GroupEntity Update(UpdateCommand command)
        {
            var entity = new GroupEntity(
                Id,
                command.NameOptional.GetValue(Name),
                command.MembersOptional.GetValue(Members));
            entity.Validate();
            return entity;
        }

        public override void Validate()
        {
            var validateHelper = new KeyedValidateHelper<ValidateTypeEnum>();
            validateHelper.Add(ValidateTypeEnum.GroupId, () => Id.Validate());
            validateHelper.Add(ValidateTypeEnum.GroupName, () => Name.Validate());
            validateHelper.Add(
                ValidateTypeEnum.Member,
                () =>
                {
                    if(Members.IsEmpty)
                    {
                        throw new DomainInvalidOperationException();
                    }
                });
        }

        public GroupId Id { get; }

        public override GroupId Identifier => Id;

        public ImmutableList<UserId> Members { get; }

        public GroupName Name { get; }

        public sealed record ReconstructCommand
        {
            public required GroupId Id { get; init; }

            public required ImmutableList<UserId> Members { get; init; }

            public required GroupName Name { get; init; }
        }

        public sealed record UpdateCommand
        {
            public Optional<ImmutableList<UserId>> MembersOptional
            {
                get;
                init;
            } = Optional<ImmutableList<UserId>>.Empty;

            public Optional<GroupName> NameOptional { get; init; } = Optional<GroupName>.Empty;
        }

        public sealed record CreateCommand
        {
            public required GroupId Id { get; init; }

            public required ImmutableList<UserId> Members { get; init; }

            public required GroupName Name { get; init; }
        }

        public enum ValidateTypeEnum
        {
            GroupId = 0,
            GroupName = 1,
            Member = 2,
        }
    }
}
