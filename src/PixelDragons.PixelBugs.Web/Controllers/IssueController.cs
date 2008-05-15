using System;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Repositories;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Web.Controllers
{
    [Layout("Default"), Rescue("GeneralError")]
    public class IssueController : SmartDispatcherController
    {
        public IIssueRepository IssueRepository { get; set; }

        public void New()
        {
        }

        public void Create([DataBind("issue")]Issue issue)
        {
            IssueRepository.Save(issue);

            RedirectToAction("List");
        }
    }
}
