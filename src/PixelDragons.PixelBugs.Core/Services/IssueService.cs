using System;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;

namespace PixelDragons.PixelBugs.Core.Services
{
    public class IssueService : IIssueService
    {
        #region Properties
        private IUserRepository _userRepository;
        private IIssueRepository _issueRepository;
        #endregion

        #region Constructors
        public IssueService(IUserRepository userRepository, IIssueRepository issueRepository)
        {
            _userRepository = userRepository;
            _issueRepository = issueRepository;
        }
        #endregion

        public Issue[] GetAllIssues()
        {
            return _issueRepository.FindAll();
        }

        public User[] GetUsersThatCanOwnIssues()
        {
            return _userRepository.FindAll();
        }

        public Issue SaveIssue(Issue issue, User currentUser)
        {
            if (issue.Id == Guid.Empty)
            {
                issue.CreatedDate = DateTime.Now;
                issue.CreatedBy = currentUser;
            }

            return _issueRepository.Save(issue);
        }
    }
}
