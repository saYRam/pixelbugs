using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByUserNameAndPassword(string userName, string password);
    }
}
