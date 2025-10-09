using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record UserId : ValueObjectBase, ISingleValueObject<string, UserId>, ILengthDefinedSingleValueObject
    {
        private UserId(string value)
        {
            Value = value;
        }

        public static UserId Create(string value)
        {
            var valueObject = new UserId(value);
            valueObject.Validate();
            return valueObject;
        }

        public static UserId Reconstruct(string value)
        {
            return new UserId(value);
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
