using System;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Web.Helpers;
using Castle.MonoRail.ActiveRecordSupport;

namespace PixelDragons.PixelBugs.Web.Controllers
{
    [Layout("Default"), Rescue("GeneralError")]
    [Helper(typeof(UIHelper), "UI")]
    [Resource("strings", "PixelDragons.PixelBugs.Web.Resources.Controllers.IssueController")]
    public class IssueController : ARSmartDispatcherController
    {
        #region Properties
        private IIssueRepository IssueRepository { get; set; }
        private IUserRepository UserRepository { get; set; }
        #endregion

        #region Constructors
        public IssueController(IIssueRepository issueRepository, IUserRepository userRepository)
        {
            IssueRepository = issueRepository;
            UserRepository = userRepository;
        }
        #endregion

        public void New()
        {
            PropertyBag["users"] = UserRepository.FindAll();
        }

        [AccessibleThrough(Verb.Post)]
        public void Create([ARDataBind("issue", AutoLoad=AutoLoadBehavior.OnlyNested)]Issue issue)
        {
            issue.CreatedDate = DateTime.Now;

            IssueRepository.Save(issue);

            RedirectToAction("List");
        }

        public void List()
        {
            PropertyBag["issues"] = IssueRepository.FindAll();
        }
    }
}
