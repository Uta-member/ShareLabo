using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record PostTitle
        : ValueObjectBase, ISingleValueObject<string, PostTitle>, ILengthDefinedSingleValueObject
    {
        private PostTitle(string value)
        {
            Value = value;
        }

        public static PostTitle Create(string value)
        {
            var valueObject = new PostTitle(value);
            valueObject.Validate();
            return valueObject;
        }

        public static PostTitle Reconstruct(string value)
        {
            return new PostTitle(value);
        }

        public override void Validate()
        {
            var validateHelper = new ValidateHelper();
            validateHelper.AddNullCheck(Value);
            validateHelper.AddStrLengthCheck(Value, MinLength, MaxLength);
            validateHelper.ExecuteValidateWithThrowException();
        }

        public static int MaxLength => 128;

        public static int MinLength => 1;

        public string Value { get; }
    }
}
