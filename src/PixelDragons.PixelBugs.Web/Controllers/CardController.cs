using System;
using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Attributes;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Messages;
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
            RetrieveCardOptionsResponse response = cardService.RetrieveCardOptions();

            PropertyBag["owners"] = response.Owners;
            PropertyBag["types"] = response.CardTypes;
            PropertyBag["statuses"] = response.CardStatuses;
            PropertyBag["priorities"] = response.CardPriorities;

            RenderView("New");
        }

        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.CreateCards)]
        public void Create([ARDataBind("card", AutoLoad = AutoLoadBehavior.OnlyNested)] Card card)
        {
            IPrincipalWithPermissions principalWithPermissions = (IPrincipalWithPermissions)Context.CurrentUser;
            
            cardService.SaveCard(card, principalWithPermissions.Id);

            RedirectToAction("Index");
        }

        [PermissionRequired(Permission.ViewCards)]
        public void Index()
        {
            RetrieveWallResponse response = cardService.RetrieveWall();

            PropertyBag["cards"] = response.Cards;
            PropertyBag["statuses"] = response.CardStatuses;

            RenderView("Index");
        }

        [PermissionRequired(Permission.ViewCards)]
        public void Show(Guid id)
        {
            RetrieveCardResponse response = cardService.RetrieveCard(new RetrieveCardRequest(id));

            PropertyBag["card"] = response.Card;

            RenderView("Show");
        }

        [PermissionRequired(Permission.EditCards)]
        public void Edit(Guid id)
        {
            RetrieveCardOptionsResponse cardOptionsResponse = cardService.RetrieveCardOptions();
            RetrieveCardResponse retrieveCardResponse = cardService.RetrieveCard(new RetrieveCardRequest(id));

            PropertyBag["card"] = retrieveCardResponse.Card;
            PropertyBag["owners"] = cardOptionsResponse.Owners;
            PropertyBag["types"] = cardOptionsResponse.CardTypes;
            PropertyBag["statuses"] = cardOptionsResponse.CardStatuses;
            PropertyBag["priorities"] = cardOptionsResponse.CardPriorities;

            RenderView("Edit");
        }

        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.EditCards)]
        public void Update([ARDataBind("card", AutoLoad = AutoLoadBehavior.NullIfInvalidKey)] Card card)
        {
            IPrincipalWithPermissions principalWithPermissions = (IPrincipalWithPermissions)Context.CurrentUser;

            cardService.SaveCard(card, principalWithPermissions.Id);

            RedirectToAction("Show", new {Id = card.Id});
        }

        [AjaxAction]
        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.EditCards)]
        public void UpdateStatus(Guid cardId, Guid statusId)
        {
            cardService.ChangeCardStatus(cardId, statusId);

            CancelLayout();
            CancelView();
        }
    }
}