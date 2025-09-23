using ShareLabo.Application.Toolkit;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public static class OperateInfoDTOExtensions
    {
        public static MPOperateInfo ToMPDTO(this OperateInfoDTO dto)
        {
            return MPOperateInfo.FromDTO(dto);
        }
    }
}