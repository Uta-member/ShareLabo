using CSStack.TADA;

namespace ShareLabo.Application.Authentication
{
    public sealed record AccountPassword
        : ValueObjectBase, ISingleValueObject<string, AccountPassword>, ILengthDefinedSingleValueObject
    {
        private AccountPassword(string value)
        {
            Value = value;
        }

        public static AccountPassword Create(string value)
        {
            var valueObject = new AccountPassword(value);
            valueObject.Validate();
            return valueObject;
        }

        public static AccountPassword Reconstruct(string value)
        {
            return new AccountPassword(value);
        }

        public override void Validate()
        {
            var validateHelper = new ValidateHelper();
            validateHelper.AddNullCheck(Value);
            validateHelper.AddStrLengthCheck(Value, MinLength, MaxLength);
            validateHelper.ExecuteValidateWithThrowException();
        }

        public static int MaxLength => 64;

        public static int MinLength => 4;

        public string Value { get; set; }
    }
}
