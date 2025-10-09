using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record PostContent
        : ValueObjectBase, ISingleValueObject<string, PostContent>, ILengthDefinedSingleValueObject
    {
        private PostContent(string value)
        {
            Value = value;
        }

        public static PostContent Create(string value)
        {
            var valueObject = new PostContent(value);
            valueObject.Validate();
            return valueObject;
        }

        public static PostContent Reconstruct(string value)
        {
            return new PostContent(value);
        }

        public override void Validate()
        {
            var validateHelper = new ValidateHelper();
            validateHelper.AddNullCheck(Value);
            validateHelper.AddStrLengthCheck(Value, MinLength, MaxLength);
            validateHelper.ExecuteValidateWithThrowException();
        }

        public static int MaxLength => 2048;

        public static int MinLength => 1;

        public string Value { get; }
    }
}
