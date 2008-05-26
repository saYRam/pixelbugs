using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using NHibernate.Expression;

namespace PixelDragons.PixelBugs.Core.Repositories
{
    public class UserRepository : ARRepository<User>, IUserRepository
    {
        public User FindByUserNameAndPassword(string userName, string password)
        {
            ICriterion[] criteria = new ICriterion[]{ new EqExpression("UserName", userName), 
                                                      new EqExpression("Password", password) };

            return FindOne(criteria);
        }
    }
}
