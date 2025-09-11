using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject.Tests
{
    public sealed class PostContentTest
    {
        [Fact]
        public void Create_Nullテスト()
        {
            var ex = Assert.Throws<MultiReasonException>(() => PostContent.Create(null!));
            Assert.Contains(ex.Exceptions, x => x is ValueObjectNullException);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2048)]
        public void Create_境界テスト_正常(int valueLength)
        {
            string value = new string('a', valueLength);
            var valueObject = PostContent.Create(value);
            Assert.Equal(value, valueObject.Value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2049)]
        public void Create_境界テスト_範囲外(int valueLength)
        {
            string value = new string('a', valueLength);
            var ex = Assert.Throws<MultiReasonException>(() => PostContent.Create(value));
            Assert.Contains(ex.Exceptions, x => x is ValueObjectLengthException);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2049)]
        public void Reconstruct_Length_正常(int valueLength)
        {
            string value = new string('a', valueLength);
            var valueObject = PostContent.Reconstruct(value);
            Assert.Equal(value, valueObject.Value);
        }

        [Fact]
        public void Reconstruct_Null_正常()
        {
            var valueObject = PostContent.Reconstruct(null!);
            Assert.Null(valueObject.Value);
        }
    }
}
