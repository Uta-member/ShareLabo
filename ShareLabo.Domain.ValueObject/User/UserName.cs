using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record UserName
        : ValueObjectBase, ISingleValueObject<string, UserName>, ILengthDefinedSingleValueObject
    {
        private UserName(string value)
        {
            Value = value;
        }

        public static UserName Create(string value)
        {
            var valueObject = new UserName(value);
            valueObject.Validate();
            return valueObject;
        }

        public static UserName Reconstruct(string value)
        {
            return new UserName(value);
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

        public string Value { get; set; }
    }
}
