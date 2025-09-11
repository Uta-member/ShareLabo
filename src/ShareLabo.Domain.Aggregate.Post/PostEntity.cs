using CSStack.TADA;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Post
{
    public sealed class PostEntity : EntityBase<PostEntity, PostId>
    {
        private PostEntity(
            PostId id,
            PostTitle title,
            PostContent content,
            UserId postUser,
            DateTime postDateTime,
            long sequenceId)
        {
            Id = id;
            Title = title;
            PostUser = postUser;
            PostDateTime = postDateTime;
            Content = content;
            SequenceId = sequenceId;
        }

        public static PostEntity Create(CreateCommand command, long sequenceId)
        {
            var entity = new PostEntity(
                command.Id,
                command.Title,
                command.Content,
                command.PostUser,
                command.PostDateTime,
                sequenceId);
            entity.Validate();
            return entity;
        }

        public static PostEntity Reconstruct(ReconstructCommand command)
        {
            var entity = new PostEntity(
                command.Id,
                command.Title,
                command.Content,
                command.PostUser,
                command.PostDateTime,
                command.SequenceId);
            return entity;
        }

        public PostEntity Update(UpdateCommand command, long sequenceId)
        {
            var entity = new PostEntity(
                Id,
                command.TitleOptional.GetValue(Title),
                command.ContentOptional.GetValue(Content),
                PostUser,
                command.PostDateTimeOptional.GetValue(PostDateTime),
                sequenceId);
            entity.Validate();
            return entity;
        }

        public override void Validate()
        {
            var validateHelper = new KeyedValidateHelper<ValidateTypeEnum>();
            validateHelper.Add(ValidateTypeEnum.Id, () => Id.Validate());
            validateHelper.Add(ValidateTypeEnum.Title, () => Title.Validate());
            validateHelper.Add(ValidateTypeEnum.Content, () => Content.Validate());
            validateHelper.Add(
                ValidateTypeEnum.SequenceId,
                () =>
                {
                    if(SequenceId < 1)
                    {
                        throw new DomainInvalidOperationException("投稿のシーケンス番号は1以上である必要があります");
                    }
                });
            validateHelper.ExecuteValidateWithThrowException();
        }

        public PostContent Content { get; }

        public PostId Id { get; }

        public override PostId Identifier => Id;

        public DateTime PostDateTime { get; }

        public UserId PostUser { get; }

        public long SequenceId { get; }

        public PostTitle Title { get; }

        public enum ValidateTypeEnum
        {
            Id = 0,
            Title = 1,
            Content = 2,
            SequenceId = 3,
        }

        public sealed record CreateCommand
        {
            public required PostContent Content { get; init; }

            public required PostId Id { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required UserId PostUser { get; init; }

            public required PostTitle Title { get; init; }
        }

        public sealed record ReconstructCommand
        {
            public required PostContent Content { get; init; }

            public required PostId Id { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required UserId PostUser { get; init; }

            public required long SequenceId { get; init; }

            public required PostTitle Title { get; init; }
        }

        public sealed record UpdateCommand
        {
            public Optional<PostContent> ContentOptional { get; init; } = Optional<PostContent>.Empty;

            public Optional<DateTime> PostDateTimeOptional { get; init; } = Optional<DateTime>.Empty;

            public Optional<PostTitle> TitleOptional { get; init; } = Optional<PostTitle>.Empty;
        }
    }
}
