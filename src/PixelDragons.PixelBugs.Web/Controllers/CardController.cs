using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Attributes;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Filters;
using PixelDragons.PixelBugs.Web.Helpers;
using System;

namespace PixelDragons.PixelBugs.Web.Controllers
{
    [Layout("Default"), Rescue("GeneralError")]
    [Helper(typeof(UIHelper), "UI")]
    [Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter), ExecutionOrder = 0)]
    [Filter(ExecuteWhen.BeforeAction, typeof(AuthorizationFilter), ExecutionOrder = 1)]
    [Resource("strings", "PixelDragons.PixelBugs.Web.Resources.Controllers.CardController")]
    public class CardController : ARSmartDispatcherController
    {
        #region Properties
        private ICardService _cardService;
        #endregion

        #region Constructors
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        #endregion

        [PermissionRequired(Permission.CreateCards)]
        public void New()
        {
            PropertyBag["users"] = _cardService.GetUsersThatCanOwnCards();
            PropertyBag["types"] = _cardService.GetCardTypes();
            PropertyBag["statuses"] = _cardService.GetCardStatuses();
            PropertyBag["priorities"] = _cardService.GetCardPriorities();

            RenderView("New");
        }

        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.CreateCards)]
        public void Create([ARDataBind("card", AutoLoad=AutoLoadBehavior.OnlyNested)]Card card)
        {
            _cardService.SaveCard(card, (User)Context.CurrentUser);

            RedirectToAction("Index");
        }

        [PermissionRequired(Permission.ViewCards)]
        public void Index()
        {
            PropertyBag["cards"] = _cardService.GetCards();
            PropertyBag["statuses"] = _cardService.GetCardStatuses();

            RenderView("Index");
        }

        [PermissionRequired(Permission.ViewCards)]
        public void Show(Guid id)
        {
            PropertyBag["card"] = _cardService.GetCard(id);

            RenderView("Show");
        }

        [PermissionRequired(Permission.EditCards)]
        public void Edit(Guid id)
        {
            PropertyBag["card"] = _cardService.GetCard(id);
            PropertyBag["users"] = _cardService.GetUsersThatCanOwnCards();
            PropertyBag["types"] = _cardService.GetCardTypes();
            PropertyBag["statuses"] = _cardService.GetCardStatuses();
            PropertyBag["priorities"] = _cardService.GetCardPriorities();

            RenderView("Edit");
        }

        [AccessibleThrough(Verb.Post)]
        [PermissionRequired(Permission.EditCards)]
        public void Update([ARDataBind("card", AutoLoad = AutoLoadBehavior.NullIfInvalidKey)]Card card)
        {
            _cardService.SaveCard(card, (User)Context.CurrentUser);

            RedirectToAction("Show", new {Id = card.Id} );
        }
    }
}
