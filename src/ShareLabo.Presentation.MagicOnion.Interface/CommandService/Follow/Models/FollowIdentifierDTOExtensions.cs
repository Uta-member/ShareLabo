using ShareLabo.Application.UseCase.CommandService.Follow;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public static class FollowIdentifierDTOExtensions
    {
        public static MPFollowIdentifier ToMPDTO(this FollowIdentifierDTO dto)
        {
            return MPFollowIdentifier.FromDTO(dto);
        }
    }
}