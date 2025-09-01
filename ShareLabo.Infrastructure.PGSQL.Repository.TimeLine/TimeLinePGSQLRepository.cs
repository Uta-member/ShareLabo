using CSStack.TADA;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Infrastructure.EFPG.Table;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Immutable;

namespace ShareLabo.Infrastructure.PGSQL.Repository.TimeLine
{
    public sealed class TimeLinePGSQLRepository : ITimeLineRepository<ShareLaboPGSQLTransaction>
    {
        public async ValueTask DeleteAsync(
            ShareLaboPGSQLTransaction session,
            TimeLineEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            await factory.Query("time_lines")
                .Where("time_line_id", entity.Id.Value)
                .DeleteAsync(cancellationToken: cancellationToken);

            await factory.Query("time_line_filters")
                .Where("time_line_id", entity.Id.Value)
                .DeleteAsync(cancellationToken: cancellationToken);
        }

        public async ValueTask<Optional<TimeLineEntity>> FindByIdentifierAsync(
            ShareLaboPGSQLTransaction session,
            TimeLineId identifier,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbTimeLine = await factory.Query("time_lines")
                .Where("time_line_id", identifier.Value)
                .FirstOrDefaultAsync<DbTimeLine>(cancellationToken: cancellationToken);

            if(dbTimeLine == null)
            {
                return Optional<TimeLineEntity>.Empty;
            }

            var dbTimeLineFilters = await factory.Query("time_line_filters")
                .Where("time_line_id", identifier.Value)
                .GetAsync<DbTimeLineFilter>(cancellationToken: cancellationToken);

            return TimeLineEntity.Reconstruct(
                new TimeLineEntity.ReconstructCommand()
                {
                    FilterMembers =
                        dbTimeLineFilters
                    .Select(x => UserId.Reconstruct(x.UserId))
                                .ToImmutableList(),
                    Id = TimeLineId.Reconstruct(dbTimeLine.TimeLineId),
                    Name = TimeLineName.Reconstruct(dbTimeLine.TimeLineName),
                    OwnerId = UserId.Reconstruct(dbTimeLine.OwnerId),
                });
        }

        public async ValueTask<ImmutableList<TimeLineEntity>> GetByOwnerIdAsync(
            ShareLaboPGSQLTransaction session,
            UserId ownerId,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbTimeLines = await factory.Query("time_lines")
                .Where("owner_id", ownerId.Value)
                .GetAsync<DbTimeLine>(cancellationToken: cancellationToken);
            if(!dbTimeLines.Any())
            {
                return [];
            }

            var dbTimeLineFilters = await factory.Query("time_line_filters")
                .WhereIn("time_line_id", dbTimeLines.Select(x => x.TimeLineId).ToArray())
                .GetAsync<DbTimeLineFilter>(cancellationToken: cancellationToken);

            return dbTimeLines.Select(
                x => TimeLineEntity.Reconstruct(
                    new TimeLineEntity.ReconstructCommand()
                {
                    FilterMembers =
                        dbTimeLineFilters
                            .Where(y => y.TimeLineId == x.TimeLineId)
                                    .Select(y => UserId.Reconstruct(y.UserId))
                                    .ToImmutableList(),
                    Id = TimeLineId.Reconstruct(x.TimeLineId),
                    Name = TimeLineName.Reconstruct(x.TimeLineName),
                    OwnerId = UserId.Reconstruct(x.OwnerId),
                }))
                .ToImmutableList();
        }

        public async ValueTask SaveAsync(
            ShareLaboPGSQLTransaction session,
            TimeLineEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default)
        {
            var connection = session.Transaction.Connection;
            var factory = new QueryFactory(connection, new PostgresCompiler());

            var dbTimeLine = await factory.Query("time_lines")
                .Where("time_line_id", entity.Identifier.Value)
                .FirstOrDefaultAsync<DbTimeLine>(cancellationToken: cancellationToken);

            if(dbTimeLine == null)
            {
                // 1つもないはずだけど念のため削除する
                await factory.Query("time_line_filters")
                    .Where("time_line_id", entity.Identifier.Value)
                    .DeleteAsync();

                await factory.Query("time_lines")
                    .InsertAsync(
                        new  DbTimeLine
                        {
                            TimeLineId = entity.Id.Value,
                            TimeLineName = entity.Name.Value,
                            OwnerId = entity.OwnerId.Value,
                            InsertTimeStamp = operateInfo.OperatedDateTime,
                            InsertUserId = operateInfo.Operator.Value,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);

                foreach(var filterMember in entity.FilterMembers)
                {
                    await factory.Query("time_line_filters")
                        .InsertAsync(
                            new DbTimeLineFilter
                            {
                                TimeLineId = entity.Id.Value,
                                UserId = filterMember.Value,
                                InsertTimeStamp = operateInfo.OperatedDateTime,
                                InsertUserId = operateInfo.Operator.Value,
                            }.ToSnakeCaseDictionary(),
                            cancellationToken: cancellationToken);
                }
            }
            else
            {
                await factory.Query("time_lines")
                    .Where("time_line_id", entity.Identifier.Value)
                    .UpdateAsync(
                        new
                        {
                            TimeLineName = entity.Name.Value,
                            OwnerId = entity.OwnerId.Value,
                            UpdateTimeStamp = operateInfo.OperatedDateTime,
                            UpdateUserId = operateInfo.Operator.Value,
                        }.ToSnakeCaseDictionary(),
                        cancellationToken: cancellationToken);

                var dbTimeLineFilters = await factory.Query("time_line_filters")
                    .Where("time_line_id", entity.Identifier.Value)
                    .GetAsync<DbTimeLineFilter>(cancellationToken: cancellationToken);

                foreach(var dbTimeLineFilter in dbTimeLineFilters)
                {
                    if(!entity.FilterMembers.Any(x => x.Value == dbTimeLineFilter.UserId))
                    {
                        await factory.Query("time_line_filters")
                            .Where("time_line_id", entity.Identifier.Value)
                            .Where("user_id", dbTimeLineFilter.UserId)
                            .DeleteAsync(cancellationToken: cancellationToken);
                    }
                }

                foreach(var filterMember in entity.FilterMembers)
                {
                    if(!dbTimeLineFilters.Any(x => x.UserId == filterMember.Value))
                    {
                        await factory.Query("time_line_filters")
                            .InsertAsync(
                                new DbTimeLineFilter
                                {
                                    TimeLineId = entity.Id.Value,
                                    UserId = filterMember.Value,
                                    InsertTimeStamp = operateInfo.OperatedDateTime,
                                    InsertUserId = operateInfo.Operator.Value,
                                }.ToSnakeCaseDictionary(),
                                cancellationToken: cancellationToken);
                    }
                }
            }
        }
    }
}
