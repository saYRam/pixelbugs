using System;
using System.Collections.Generic;
using System.Security.Principal;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public interface IPrincipalWithPermissions : IPrincipal
    {
        Guid Id { get; }
        List<Permission> Permissions { get; }
    }
}