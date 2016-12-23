namespace Mvc5Application1.Business.Contracts.Security
{
    public interface IAuthorizationBusiness
    {
        bool CheckPermission(string resource);
    }
}