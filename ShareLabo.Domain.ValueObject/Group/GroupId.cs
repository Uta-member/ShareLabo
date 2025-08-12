using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record GroupId : ValueObjectBase, ISingleValueObject<string, GroupId>, ILengthDefinedSingleValueObject
    {
        private GroupId(string value)
        {
            Value = value;
        }

        public static GroupId Create(string value)
        {
            var valueObject = new GroupId(value);
            valueObject.Validate();
            return valueObject;
        }

        public static GroupId Reconstruct(string value)
        {
            return new GroupId(value);
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

        public string Value { get; set; }
    }
}
