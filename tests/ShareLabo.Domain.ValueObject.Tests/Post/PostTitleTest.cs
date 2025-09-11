using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject.Tests
{
    public sealed class PostTitleTest
    {
        [Fact]
        public void Create_Nullテスト()
        {
            var ex = Assert.Throws<MultiReasonException>(() => PostTitle.Create(null!));
            Assert.Contains(ex.Exceptions, x => x is ValueObjectNullException);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(128)]
        public void Create_境界テスト_正常(int valueLength)
        {
            string value = new string('a', valueLength);
            var valueObject = PostTitle.Create(value);
            Assert.Equal(value, valueObject.Value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(129)]
        public void Create_境界テスト_範囲外(int valueLength)
        {
            string value = new string('a', valueLength);
            var ex = Assert.Throws<MultiReasonException>(() => PostTitle.Create(value));
            Assert.Contains(ex.Exceptions, x => x is ValueObjectLengthException);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(129)]
        public void Reconstruct_Length_正常(int valueLength)
        {
            string value = new string('a', valueLength);
            var valueObject = PostTitle.Reconstruct(value);
            Assert.Equal(value, valueObject.Value);
        }

        [Fact]
        public void Reconstruct_Null_正常()
        {
            var valueObject = PostTitle.Reconstruct(null!);
            Assert.Null(valueObject.Value);
        }
    }
}
