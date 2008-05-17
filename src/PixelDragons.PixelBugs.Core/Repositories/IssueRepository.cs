using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Repositories
{
    public class IssueRepository : ARRepository<Issue>, IIssueRepository
    {
    }
}
