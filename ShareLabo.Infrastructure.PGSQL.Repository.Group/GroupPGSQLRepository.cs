using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Group;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.Repository.Group
{
    public sealed class GroupPGSQLRepository : IGroupRepository<ShareLaboPGSQLTransaction>
    {
        public async ValueTask<Optional<GroupEntity>> FindByIdentifierAsync(
            ShareLaboPGSQLTransaction session,
            GroupId identifier,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbGroup = await factory.Query("groups")
                .Where("group_id", identifier.Value)
                .FirstOrDefaultAsync<DbGroup>(cancellationToken: cancellationToken);

            if(dbGroup == null)
            {
                return Optional<GroupEntity>.Empty;
            }

            var dbGroupMembers = await factory.Query("group_members")
                .Where("group_id", dbGroup.GroupId)
                .GetAsync<DbGroupMember>(cancellationToken: cancellationToken);

            return GroupEntity.Reconstruct(
                new GroupEntity.ReconstructCommand()
                {
                    Id = GroupId.Reconstruct(dbGroup.GroupId),
                    Members =
                        dbGroupMembers
                        .Select(x => UserId.Reconstruct(x.UserId))
                                .ToImmutableList(),
                    Name = GroupName.Reconstruct(dbGroup.GroupName),
                });
        }

        public async ValueTask<ImmutableList<GroupEntity>> GetByMemberAsync(
            ShareLaboPGSQLTransaction session,
            UserId memberId,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbGroups = await factory.Query("groups")
                .WhereIn(
                    "group_id",
                    factory.Query("group_members")
                        .Select("group_id")
                        .Where("user_id", memberId.Value))
                .GetAsync<DbGroup>(cancellationToken: cancellationToken);

            if(dbGroups.Count() < 1)
            {
                return ImmutableList<GroupEntity>.Empty;
            }

            var dbGroupMembers = await factory.Query("group_members")
                .WhereIn(
                    "group_id",
                    dbGroups.Select(x => x.GroupId))
                .GetAsync<DbGroupMember>(cancellationToken: cancellationToken);

            return dbGroups
                .Select(
                    dbGroup => GroupEntity.Reconstruct(
                        new GroupEntity.ReconstructCommand()
                        {
                            Id = GroupId.Reconstruct(dbGroup.GroupId),
                            Members =
                                dbGroupMembers
                            .Where(x => x.GroupId == dbGroup.GroupId)
                                            .Select(x => UserId.Reconstruct(x.UserId))
                                            .ToImmutableList(),
                            Name = GroupName.Reconstruct(dbGroup.GroupName),
                        }))
                .ToImmutableList();
        }

        public async ValueTask RemoveAsync(
            ShareLaboPGSQLTransaction session,
            GroupEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            await factory.Query("groups")
                .Where("group_id", entity.Id.Value)
                .DeleteAsync(cancellationToken: cancellationToken);
            await factory.Query("group_members")
                .Where("group_id", entity.Id.Value)
                .DeleteAsync(cancellationToken: cancellationToken);
        }

        public async ValueTask SaveAsync(
            ShareLaboPGSQLTransaction session,
            GroupEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbGroup = await factory.Query("groups")
                .Where("group_id", entity.Id.Value)
                .FirstOrDefaultAsync<DbGroup>(cancellationToken: cancellationToken);

            if(dbGroup == null)
            {
                await factory.Query("groups")
                    .InsertGetIdAsync<DbGroup>(
                        new DbGroup()
                        {
                            GroupId = entity.Id.Value,
                            GroupName = entity.Name.Value,
                            InsertTimeStamp = operateInfo.OperatedDateTime,
                            InsertUserId = operateInfo.Operator.Value,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);
            }
            else
            {
                await factory.Query("groups")
                    .Where("group_id", entity.Identifier.Value)
                    .UpdateAsync(
                        new
                        {
                            GroupName = entity.Name.Value,
                            UpdateTimeStamp = operateInfo.OperatedDateTime,
                            UpdateUserId = operateInfo.Operator.Value,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);
            }

            await factory.Query("group_members")
                .Where("group_id", entity.Identifier.Value)
                .WhereNotIn("user_id", entity.Members.Select(x => x.Value))
                .DeleteAsync(cancellationToken: cancellationToken);

            var currentDbGroupMembers = await factory.Query("group_members")
                .Where("group_id", entity.Identifier.Value)
                .GetAsync<DbGroupMember>(cancellationToken: cancellationToken);

            foreach(var member in entity.Members)
            {
                if(currentDbGroupMembers.Any(x => x.UserId == member.Value))
                {
                    continue;
                }
                await factory.Query("group_members")
                    .InsertAsync(
                        new DbGroupMember()
                        {
                            GroupId = entity.Identifier.Value,
                            UserId = member.Value,
                            InsertTimeStamp = operateInfo.OperatedDateTime,
                            InsertUserId = operateInfo.Operator.Value,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);
            }
        }
    }
}
