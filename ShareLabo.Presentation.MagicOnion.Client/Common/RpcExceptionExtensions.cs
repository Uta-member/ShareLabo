using CSStack.TADA;
using Grpc.Core;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public static class RpcExceptionExtensions
    {
        public static Exception? GetExceptionTypeMatch(this RpcException ex)
        {
            var exceptionType = ex.Trailers.GetValue("exception-type");
            var exceptionMessage = ex.Trailers.GetValue("exception-message");

            if(string.IsNullOrWhiteSpace(exceptionType))
            {
                return null;
            }

            if(exceptionType.Contains("ObjectNotFoundException"))
            {
                return new ObjectNotFoundException(exceptionMessage, ex);
            }

            if(exceptionType.Contains("ObjectAlreadyExistException"))
            {
                return new ObjectAlreadyExistException(exceptionMessage, ex);
            }

            if(exceptionType.Contains("DomainInvalidOperationException"))
            {
                return new DomainInvalidOperationException(exceptionMessage, ex);
            }

            if(exceptionType.Contains("ValueObjectInvalidException"))
            {
                return new ValueObjectInvalidException(exceptionMessage, ex);
            }

            if(exceptionType.Contains("ValueObjectNullException"))
            {
                return new ValueObjectNullException(exceptionMessage, ex);
            }

            return null;
        }
    }
}
