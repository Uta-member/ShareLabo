using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Post.Tests
{
    public sealed class PostEntityTest
    {
        [Fact]
        public void Create_Update_正常()
        {
            var postId = PostId.Create(new string('a', 8));
            var postTitle = PostTitle.Create(new string('a', 8));
            var postContent = PostContent.Create(new string('a', 8));
            var postDateTime = new DateTime(2025, 9, 1);
            var postUserId = UserId.Reconstruct(new string('a', 8));
            long sequenceId = 1L;

            var entity = PostEntity.Create(
                new PostEntity.CreateCommand()
                {
                    Content = postContent,
                    Id = postId,
                    PostDateTime = postDateTime,
                    PostUser = postUserId,
                    Title = postTitle,
                },
                sequenceId);

            Assert.Equal(postId, entity.Id);
            Assert.Equal(postId, entity.Identifier);
            Assert.Equal(postTitle, entity.Title);
            Assert.Equal(postContent, entity.Content);
            Assert.Equal(postDateTime, entity.PostDateTime);
            Assert.Equal(postUserId, entity.PostUser);
            Assert.Equal(sequenceId, entity.SequenceId);

            postTitle = PostTitle.Create(new string('b', 8));
            postContent = PostContent.Create(new string('b', 8));
            postDateTime = new DateTime(2025, 9, 2);
            sequenceId = 2L;

            entity = entity.Update(
                new PostEntity.UpdateCommand()
                {
                    ContentOptional = postContent,
                    PostDateTimeOptional = postDateTime,
                    TitleOptional = postTitle,
                },
                sequenceId);

            Assert.Equal(postId, entity.Id);
            Assert.Equal(postId, entity.Identifier);
            Assert.Equal(postTitle, entity.Title);
            Assert.Equal(postContent, entity.Content);
            Assert.Equal(postDateTime, entity.PostDateTime);
            Assert.Equal(postUserId, entity.PostUser);
            Assert.Equal(sequenceId, entity.SequenceId);
        }

        [Fact]
        public void Reconstruct_正常()
        {
            var postId = PostId.Reconstruct(new string('a', 8));
            var postTitle = PostTitle.Reconstruct(new string('a', 8));
            var postContent = PostContent.Reconstruct(new string('a', 8));
            var postDateTime = new DateTime(2025, 9, 1);
            var postUserId = UserId.Reconstruct(new string('a', 8));
            long sequenceId = 1L;

            var entity = PostEntity.Reconstruct(
                new PostEntity.ReconstructCommand()
                {
                    Content = postContent,
                    Id = postId,
                    PostDateTime = postDateTime,
                    PostUser = postUserId,
                    Title = postTitle,
                    SequenceId = sequenceId,
                });

            Assert.Equal(postId, entity.Id);
            Assert.Equal(postId, entity.Identifier);
            Assert.Equal(postTitle, entity.Title);
            Assert.Equal(postContent, entity.Content);
            Assert.Equal(postDateTime, entity.PostDateTime);
            Assert.Equal(postUserId, entity.PostUser);
            Assert.Equal(sequenceId, entity.SequenceId);
        }
    }
}
