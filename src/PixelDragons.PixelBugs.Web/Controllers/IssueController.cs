using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Attributes;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Filters;
using PixelDragons.PixelBugs.Web.Helpers;

namespace PixelDragons.PixelBugs.Web.Controllers
{
    [Layout("Default"), Rescue("GeneralError")]
    [Helper(typeof(UIHelper), "UI")]
    [Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter), ExecutionOrder = 0)]
    [Filter(ExecuteWhen.BeforeAction, typeof(AuthorizationFilter), ExecutionOrder = 1)]
    [Resource("strings", "PixelDragons.PixelBugs.Web.Resources.Controllers.IssueController")]
    public class IssueController : ARSmartDispatcherController
    {
        #region Properties
        private IIssueService _issueService;
        #endregion

        #region Constructors
        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }
        #endregion

        [PermissionRequired(Permission.CreateIssues)]
        public void New()
        {
            PropertyBag["users"] = _issueService.GetUsersThatCanOwnIssues();

            RenderView("New");
        }

        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.CreateIssues)]
        public void Create([ARDataBind("issue", AutoLoad=AutoLoadBehavior.OnlyNested)]Issue issue)
        {
            _issueService.SaveIssue(issue, Context.CurrentUser as User);

            RedirectToAction("List");
        }

        [PermissionRequired(Permission.ViewIssues)]
        public void List()
        {
            PropertyBag["issues"] = _issueService.GetAllIssues();

            RenderView("List");
        }
    }
}
