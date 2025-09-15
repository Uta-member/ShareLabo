using CSStack.TADA;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.User.Tests
{
    public sealed class UserEntityTest
    {
        [Fact]
        public void Create_正常()
        {
            var userId = UserId.Create(new string('a', 8));
            var userAccountId = UserAccountId.Create(new string('a', 8));
            var userName = UserName.Create(new string('a', 8));

            var entity = UserEntity.Create(
                new UserEntity.CreateCommand()
                {
                    AccountId = userAccountId,
                    Id = userId,
                    Name = userName,
                });

            Assert.Equal(userId, entity.Id);
            Assert.Equal(userAccountId, entity.AccountId);
            Assert.Equal(userName, entity.Name);
            Assert.Equal(UserEntity.StatusEnum.Enabled, entity.Status);
        }

        [Fact]
        public void Reconstruct_正常()
        {
            var userId = UserId.Reconstruct(new string('a', 8));
            var userAccountId = UserAccountId.Reconstruct(new string('a', 8));
            var userName = UserName.Reconstruct(new string('a', 8));

            var entity = UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    AccountId = userAccountId,
                    Id = userId,
                    Name = userName,
                    Status = UserEntity.StatusEnum.Enabled,
                });

            Assert.Equal(userId, entity.Id);
            Assert.Equal(userAccountId, entity.AccountId);
            Assert.Equal(userName, entity.Name);
        }

        [Fact]
        public void Update_削除済み()
        {
            var userId = UserId.Reconstruct(new string('a', 8));
            var userAccountId = UserAccountId.Reconstruct(new string('a', 8));
            var userName = UserName.Reconstruct(new string('a', 8));

            var entity = UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    AccountId = userAccountId,
                    Id = userId,
                    Name = userName,
                    Status = UserEntity.StatusEnum.Deleted,
                });

            userAccountId = UserAccountId.Create(new string('b', 8));
            userName = UserName.Create(new string('b', 8));
            Assert.Throws<DomainInvalidOperationException>(
                () => entity.Update(
                    new UserEntity.UpdateCommand()
                {
                    AccountIdOptional = userAccountId,
                    NameOptional = userName,
                }));
        }

        [Fact]
        public void Update_正常()
        {
            var userId = UserId.Reconstruct(new string('a', 8));
            var userAccountId = UserAccountId.Reconstruct(new string('a', 8));
            var userName = UserName.Reconstruct(new string('a', 8));

            var entity = UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    AccountId = userAccountId,
                    Id = userId,
                    Name = userName,
                    Status = UserEntity.StatusEnum.Enabled,
                });

            userAccountId = UserAccountId.Create(new string('b', 8));
            userName = UserName.Create(new string('b', 8));
            entity = entity.Update(
                new UserEntity.UpdateCommand()
                {
                    AccountIdOptional = userAccountId,
                    NameOptional = userName,
                });

            Assert.Equal(userId, entity.Id);
            Assert.Equal(userAccountId, entity.AccountId);
            Assert.Equal(userName, entity.Name);
            Assert.Equal(UserEntity.StatusEnum.Enabled, entity.Status);
        }

        [Fact]
        public void ステータス変更()
        {
            var userId = UserId.Reconstruct(new string('a', 8));
            var userAccountId = UserAccountId.Reconstruct(new string('a', 8));
            var userName = UserName.Reconstruct(new string('a', 8));

            var entity = UserEntity.Reconstruct(
                new UserEntity.ReconstructCommand()
                {
                    AccountId = userAccountId,
                    Id = userId,
                    Name = userName,
                    Status = UserEntity.StatusEnum.Enabled,
                });

            Assert.Throws<DomainInvalidOperationException>(() => entity.Enable());

            entity = entity.Disable();
            Assert.Equal(UserEntity.StatusEnum.Disabled, entity.Status);

            entity = entity.Enable();
            Assert.Equal(UserEntity.StatusEnum.Enabled, entity.Status);

            entity = entity.Delete();
            Assert.Equal(UserEntity.StatusEnum.Deleted, entity.Status);

            Assert.Throws<DomainInvalidOperationException>(() => entity.Enable());
            Assert.Throws<DomainInvalidOperationException>(() => entity.Disable());
        }
    }
}
