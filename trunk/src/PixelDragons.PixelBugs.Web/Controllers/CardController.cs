using System;
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
    [Helper(typeof (UIHelper), "UI")]
    [Filter(ExecuteWhen.BeforeAction, typeof (AuthenticationFilter), ExecutionOrder = 0)]
    [Filter(ExecuteWhen.BeforeAction, typeof (AuthorizationFilter), ExecutionOrder = 1)]
    [Resource("strings", "PixelDragons.PixelBugs.Web.Resources.Controllers.CardController")]
    public class CardController : ARSmartDispatcherController
    {
        private readonly ICardService cardService;

        public CardController(ICardService cardService)
        {
            this.cardService = cardService;
        }

        [PermissionRequired(Permission.CreateCards)]
        public void New()
        {
            PropertyBag["users"] = cardService.GetUsersThatCanOwnCards();
            PropertyBag["types"] = cardService.GetCardTypes();
            PropertyBag["statuses"] = cardService.GetCardStatuses();
            PropertyBag["priorities"] = cardService.GetCardPriorities();

            RenderView("New");
        }

        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.CreateCards)]
        public void Create([ARDataBind("card", AutoLoad = AutoLoadBehavior.OnlyNested)] Card card)
        {
            cardService.SaveCard(card, (User) Context.CurrentUser);

            RedirectToAction("Index");
        }

        [PermissionRequired(Permission.ViewCards)]
        public void Index()
        {
            PropertyBag["cards"] = cardService.GetCards();
            PropertyBag["statuses"] = cardService.GetCardStatuses();

            RenderView("Index");
        }

        [PermissionRequired(Permission.ViewCards)]
        public void Show(Guid id)
        {
            PropertyBag["card"] = cardService.GetCard(id);

            RenderView("Show");
        }

        [PermissionRequired(Permission.EditCards)]
        public void Edit(Guid id)
        {
            PropertyBag["card"] = cardService.GetCard(id);
            PropertyBag["users"] = cardService.GetUsersThatCanOwnCards();
            PropertyBag["types"] = cardService.GetCardTypes();
            PropertyBag["statuses"] = cardService.GetCardStatuses();
            PropertyBag["priorities"] = cardService.GetCardPriorities();

            RenderView("Edit");
        }

        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.EditCards)]
        public void Update([ARDataBind("card", AutoLoad = AutoLoadBehavior.NullIfInvalidKey)] Card card)
        {
            cardService.SaveCard(card, (User) Context.CurrentUser);

            RedirectToAction("Show", new {Id = card.Id});
        }

        [AjaxAction]
        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.EditCards)]
        public void UpdateStatus(Guid cardId, Guid statusId)
        {
            cardService.ChangeCardStatus(cardId, statusId, (User) Context.CurrentUser);

            CancelLayout();
            CancelView();
        }
    }
}