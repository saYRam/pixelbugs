using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using NHibernate.Criterion;

namespace PixelDragons.PixelBugs.Core.Repositories
{
    public class UserRepository : ARRepository<User>, IUserRepository
    {
        public User FindByUserNameAndPassword(string userName, string password)
        {
            ICriterion[] criteria = new ICriterion[]{ Expression.Eq("UserName", userName), 
                                                      Expression.Eq("Password", password) };

            return FindOne(criteria);
        }
    }
}
