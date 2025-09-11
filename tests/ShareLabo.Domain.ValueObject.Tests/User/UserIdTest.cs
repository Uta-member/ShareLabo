using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject.Tests
{
    public sealed class UserIdTest
    {
        [Fact]
        public void Create_Nullテスト()
        {
            var ex = Assert.Throws<MultiReasonException>(() => UserId.Create(null!));
            Assert.Contains(ex.Exceptions, x => x is ValueObjectNullException);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(64)]
        public void Create_境界テスト_正常(int valueLength)
        {
            string value = new string('a', valueLength);
            var valueObject = UserId.Create(value);
            Assert.Equal(value, valueObject.Value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        [InlineData(65)]
        public void Create_境界テスト_範囲外(int valueLength)
        {
            string value = new string('a', valueLength);
            var ex = Assert.Throws<MultiReasonException>(() => UserId.Create(value));
            Assert.Contains(ex.Exceptions, x => x is ValueObjectLengthException);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        [InlineData(65)]
        public void Reconstruct_Length_正常(int valueLength)
        {
            string value = new string('a', valueLength);
            var valueObject = UserId.Reconstruct(value);
            Assert.Equal(value, valueObject.Value);
        }

        [Fact]
        public void Reconstruct_Null_正常()
        {
            var valueObject = UserId.Reconstruct(null!);
            Assert.Null(valueObject.Value);
        }
    }
}
