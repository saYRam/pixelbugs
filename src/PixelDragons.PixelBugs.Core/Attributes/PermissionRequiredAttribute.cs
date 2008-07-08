using System;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true), Serializable]
    public class PermissionRequiredAttribute : Attribute
    {
        public PermissionRequiredAttribute(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; private set; }
    }
}