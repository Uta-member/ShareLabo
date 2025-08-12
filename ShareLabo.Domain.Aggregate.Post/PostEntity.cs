using CSStack.TADA;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

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
            ImmutableList<GroupId> publicationGroups)
        {
            Id = id;
            Title = title;
            PostUser = postUser;
            PostDateTime = postDateTime;
            Content = content;
            PublicationGroups = publicationGroups;
        }

        public static PostEntity Create(CreateCommand command)
        {
            var entity = new PostEntity(
                command.Id,
                command.Title,
                command.Content,
                command.PostUser,
                command.PostDateTime,
                command.PublicationGroups);
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
                command.PublicationGroups);
            return entity;
        }

        public PostEntity Update(UpdateCommand command)
        {
            var entity = new PostEntity(
                Id,
                command.TitleOptional.GetValue(Title),
                command.ContentOptional.GetValue(Content),
                PostUser,
                command.PostDateTimeOptional.GetValue(PostDateTime),
                command.PublicationGroupsOptional.GetValue(PublicationGroups));
            entity.Validate();
            return entity;
        }

        public override void Validate()
        {
            var validateHelper = new KeyedValidateHelper<ValidateTypeEnum>();
            validateHelper.Add(ValidateTypeEnum.Id, () => Id.Validate());
            validateHelper.Add(ValidateTypeEnum.Title, () => Title.Validate());
            validateHelper.ExecuteValidateWithThrowException();
        }

        public PostContent Content { get; }

        public PostId Id { get; }

        public override PostId Identifier => Id;

        public DateTime PostDateTime { get; }

        public UserId PostUser { get; }

        public ImmutableList<GroupId> PublicationGroups { get; }

        public PostTitle Title { get; }

        public enum ValidateTypeEnum
        {
            Id = 0,
            Title = 1,
            Content = 2,
        }

        public sealed record CreateCommand
        {
            public required PostContent Content { get; init; }

            public required PostId Id { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required UserId PostUser { get; init; }

            public required ImmutableList<GroupId> PublicationGroups { get; init; }

            public required PostTitle Title { get; init; }
        }

        public sealed record ReconstructCommand
        {
            public required PostContent Content { get; init; }

            public required PostId Id { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required UserId PostUser { get; init; }

            public required ImmutableList<GroupId> PublicationGroups { get; init; }

            public required PostTitle Title { get; init; }
        }

        public sealed record UpdateCommand
        {
            public Optional<PostContent> ContentOptional { get; init; } = Optional<PostContent>.Empty;

            public Optional<DateTime> PostDateTimeOptional { get; init; } = Optional<DateTime>.Empty;

            public Optional<ImmutableList<GroupId>> PublicationGroupsOptional
            {
                get;
                init;
            } = Optional<ImmutableList<GroupId>>.Empty;

            public Optional<PostTitle> TitleOptional { get; init; } = Optional<PostTitle>.Empty;
        }
    }
}
