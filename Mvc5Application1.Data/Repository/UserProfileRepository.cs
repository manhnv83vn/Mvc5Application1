using Mvc5Application1.Data.Contracts;
using Mvc5Application1.Data.Model;

namespace Mvc5Application1.Data.Repository
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}