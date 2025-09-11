using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record TimeLineId
        : ValueObjectBase, ISingleValueObject<string, TimeLineId>, ILengthDefinedSingleValueObject
    {
        private TimeLineId(string value)
        {
            Value = value;
        }

        public static TimeLineId Create(string value)
        {
            var valueObject = new TimeLineId(value);
            valueObject.Validate();
            return valueObject;
        }

        public static TimeLineId Reconstruct(string value)
        {
            return new TimeLineId(value);
        }

        public override void Validate()
        {
            var validateHelper = new ValidateHelper();
            validateHelper.AddNullCheck(Value);
            validateHelper.AddStrLengthCheck(Value, MinLength, MaxLength);
            validateHelper.ExecuteValidateWithThrowException();
        }

        public static int MaxLength => 128;

        public static int MinLength => 8;

        public string Value { get; set; }
    }
}
