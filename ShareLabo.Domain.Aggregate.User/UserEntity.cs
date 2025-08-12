using CSStack.TADA;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.User
{
    public sealed class UserEntity : EntityBase<UserEntity, UserId>
    {
        private UserEntity(UserId id, UserName name, UserAccountId accountId, StatusEnum status)
        {
            Id = id;
            Name = name;
            AccountId = accountId;
            Status = status;
        }

        public static UserEntity Create(CreateCommand command)
        {
            var entity = new UserEntity(command.Id, command.Name, command.AccountId, StatusEnum.Enabled);
            entity.Validate();
            return entity;
        }

        public UserEntity Delete()
        {
            if(Status == StatusEnum.Deleted)
            {
                throw new DomainInvalidOperationException();
            }
            var entity = new UserEntity(Id, Name, AccountId, StatusEnum.Deleted);
            return entity;
        }

        public UserEntity Disable()
        {
            if(Status != StatusEnum.Enabled)
            {
                throw new DomainInvalidOperationException();
            }
            var entity = new UserEntity(Id, Name, AccountId, StatusEnum.Disabled);
            return entity;
        }

        public UserEntity Enable()
        {
            if(Status != StatusEnum.Disabled)
            {
                throw new DomainInvalidOperationException();
            }
            var entity = new UserEntity(Id, Name, AccountId, StatusEnum.Enabled);
            return entity;
        }

        public static UserEntity Reconstruct(ReconstructCommand command)
        {
            return new UserEntity(command.Id, command.Name, command.AccountId, command.Status);
        }

        public UserEntity Update(UpdateCommand command)
        {
            if(Status == StatusEnum.Deleted)
            {
                throw new DomainInvalidOperationException();
            }
            var entity = new UserEntity(
                Id,
                command.NameOptional.GetValue(Name),
                command.AccountIdOptional.GetValue(AccountId),
                Status);
            entity.Validate();
            return entity;
        }

        public override void Validate()
        {
            var validateHelper = new KeyedValidateHelper<ValidateTypeEnum>();
            validateHelper.Add(ValidateTypeEnum.UserId, () => Id.Validate());
            validateHelper.Add(ValidateTypeEnum.UserAccountId, () => AccountId.Validate());
            validateHelper.Add(ValidateTypeEnum.UserName, () => Name.Validate());
            validateHelper.ExecuteValidateWithThrowException();
        }

        public UserAccountId AccountId { get; }

        public UserId Id { get; }

        public override UserId Identifier => Id;

        public UserName Name { get; }

        public StatusEnum Status { get; }

        public sealed record UpdateCommand
        {
            public Optional<UserAccountId> AccountIdOptional { get; init; } = Optional<UserAccountId>.Empty;

            public Optional<UserName> NameOptional { get; init; } = Optional<UserName>.Empty;
        }

        public sealed record ReconstructCommand
        {
            public required UserAccountId AccountId { get; init; }

            public required UserId Id { get; init; }

            public required UserName Name { get; init; }

            public required StatusEnum Status { get; init; }
        }

        public sealed record CreateCommand
        {
            public required UserAccountId AccountId { get; init; }

            public required UserId Id { get; init; }

            public required UserName Name { get; init; }
        }

        public enum StatusEnum
        {
            Enabled = 0,
            Disabled = 1,
            Deleted = 2,
        }

        public enum ValidateTypeEnum
        {
            UserId = 0,
            UserAccountId = 1,
            UserName = 2,
        }
    }
}
