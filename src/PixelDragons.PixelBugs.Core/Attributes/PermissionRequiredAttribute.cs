using System;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true), Serializable]
    public class PermissionRequiredAttribute : Attribute
    {
        private Permission _permission;

        public PermissionRequiredAttribute(Permission permission)
        {
            _permission = permission;
        }

        public Permission Permission
        {
            get { return _permission; }
        }
    }
}
