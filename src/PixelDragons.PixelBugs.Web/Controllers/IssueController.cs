using System;
using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Web.Helpers;
using PixelDragons.PixelBugs.Core.Attributes;
using PixelDragons.PixelBugs.Web.Filters;

namespace PixelDragons.PixelBugs.Web.Controllers
{
    [Layout("Default"), Rescue("GeneralError")]
    [Helper(typeof(UIHelper), "UI")]
    [Filter(ExecuteEnum.BeforeAction, typeof(AuthenticationFilter), ExecutionOrder = 0)]
    [Filter(ExecuteEnum.BeforeAction, typeof(AuthorizationFilter), ExecutionOrder = 1)]
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

        [PermissionRequired(Permission.CreateIssues)]
        public void New()
        {
            PropertyBag["users"] = UserRepository.FindAll();
        }

        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.CreateIssues)]
        public void Create([ARDataBind("issue", AutoLoad=AutoLoadBehavior.OnlyNested)]Issue issue)
        {
            issue.CreatedDate = DateTime.Now;

            IssueRepository.Save(issue);

            RedirectToAction("List");
        }

        [PermissionRequired(Permission.ViewIssues)]
        public void List()
        {
            PropertyBag["issues"] = IssueRepository.FindAll();
        }
    }
}
