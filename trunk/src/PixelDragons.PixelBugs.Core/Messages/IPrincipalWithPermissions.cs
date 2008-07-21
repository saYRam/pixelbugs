using System;
using System.Security.Principal;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public interface IPrincipalWithPermissions : IPrincipal
    {
        Guid Id { get; }
        bool HasPermission(Permission permission);
    }
}