using CSStack.TADA;

namespace ShareLabo.Domain.ValueObject
{
    public sealed record FollowIdentifier : ValueObjectBase
    {
        private FollowIdentifier(UserId followFromId, UserId followToId)
        {
            FollowFromId = followFromId;
            FollowToId = followToId;
        }

        public static FollowIdentifier Create(UserId followFromId, UserId followToId)
        {
            var valueObject = new FollowIdentifier(followFromId, followToId);
            valueObject.Validate();
            return valueObject;
        }

        public static FollowIdentifier Reconstruct(UserId followFromId, UserId followToId)
        {
            return new FollowIdentifier(followFromId, followToId);
        }

        public override void Validate()
        {
            var validateHelper = new ValidateHelper();
            validateHelper.Add(
                () =>
                {
                    if(FollowFromId == FollowToId)
                    {
                        throw new InvalidOperationException("FollowFromId and FollowToId cannot be the same.");
                    }
                });
            validateHelper.ExecuteValidateWithThrowException();
        }

        public UserId FollowFromId { get; }

        public UserId FollowToId { get; }
    }
}
