using System;
using Castle.MonoRail.Framework;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Web.ViewComponents
{
    [ViewComponentDetails("security")]
    public class SecurityBlockComponent : ViewComponent
    {
        [ViewComponentParam(Required = true)]
        public string Permission { get; set; }

        public bool Rendered { get; set; }

        public override void Initialize()
        {
            Rendered = false;
            base.Initialize();
        }

        public override void Render()
        {           
            try
            {
                Permission permission = (Permission)Enum.Parse(typeof(Permission), Permission, true);

                IPrincipalWithPermissions principalWithPermissions = EngineContext.CurrentUser as IPrincipalWithPermissions;
                if (principalWithPermissions != null && principalWithPermissions.HasPermission(permission))
                {
                    Context.RenderBody();
                    Rendered = true;
                }
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(String.Format("Unable to convert '{0}' to a system permission.", Permission), ex));
            }
        }
    }
}