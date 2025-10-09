using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record PostId : ValueObjectBase, ISingleValueObject<string, PostId>, ILengthDefinedSingleValueObject
    {
        private PostId(string value)
        {
            Value = value;
        }

        public static PostId Create(string value)
        {
            var valueObject = new PostId(value);
            valueObject.Validate();
            return valueObject;
        }

        public static PostId Reconstruct(string value)
        {
            return new PostId(value);
        }

        public override void Validate()
        {
            var validateHelper = new ValidateHelper();
            validateHelper.AddNullCheck(Value);
            validateHelper.AddStrLengthCheck(Value, MinLength, MaxLength);
            validateHelper.ExecuteValidateWithThrowException();
        }

        public static int MaxLength => 64;

        public static int MinLength => 8;

        public string Value { get; }
    }
}
