using System;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public interface IRetrievedUser
    {
        Guid Id { get; }
        bool HasPermission(Permission permission);
    }
}