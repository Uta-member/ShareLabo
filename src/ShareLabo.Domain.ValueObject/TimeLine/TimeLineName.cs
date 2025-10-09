using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record TimeLineName
        : ValueObjectBase, ISingleValueObject<string, TimeLineName>, ILengthDefinedSingleValueObject
    {
        private TimeLineName(string value)
        {
            Value = value;
        }

        public static TimeLineName Create(string value)
        {
            var valueObject = new TimeLineName(value);
            valueObject.Validate();
            return valueObject;
        }

        public static TimeLineName Reconstruct(string value)
        {
            return new TimeLineName(value);
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
