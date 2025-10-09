using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record UserAccountId
        : ValueObjectBase, ISingleValueObject<string, UserAccountId>, ILengthDefinedSingleValueObject
    {
        private UserAccountId(string value)
        {
            Value = value;
        }

        public static UserAccountId Create(string value)
        {
            var valueObject = new UserAccountId(value);
            valueObject.Validate();
            return valueObject;
        }

        public static UserAccountId Reconstruct(string value)
        {
            return new UserAccountId(value);
        }

        public override void Validate()
        {
            var validateHelper = new ValidateHelper();
            validateHelper.AddNullCheck(Value);
            validateHelper.AddStrLengthCheck(Value, MinLength, MaxLength);
            validateHelper.ExecuteValidateWithThrowException();
        }

        public static int MaxLength => 64;

        public static int MinLength => 1;

        public string Value { get; }
    }
}
