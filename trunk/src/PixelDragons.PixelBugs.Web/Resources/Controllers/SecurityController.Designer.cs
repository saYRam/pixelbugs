﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PixelDragons.PixelBugs.Web.Resources.Controllers {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SecurityController {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SecurityController() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PixelDragons.PixelBugs.Web.Resources.Controllers.SecurityController", typeof(SecurityController).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sign In.
        /// </summary>
        internal static string Buttons_SignIn {
            get {
                return ResourceManager.GetString("Buttons_SignIn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You entered an invalid user name or password.
        /// </summary>
        internal static string Errors_InvalidCredentials {
            get {
                return ResourceManager.GetString("Errors_InvalidCredentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password:.
        /// </summary>
        internal static string Labels_Password {
            get {
                return ResourceManager.GetString("Labels_Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User Name:.
        /// </summary>
        internal static string Labels_UserName {
            get {
                return ResourceManager.GetString("Labels_UserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sign In.
        /// </summary>
        internal static string Links_SignIn {
            get {
                return ResourceManager.GetString("Links_SignIn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your session has expired due to inactivity so as a security measure we have automatically signed you out..
        /// </summary>
        internal static string Messages_SessionHasTimedOut {
            get {
                return ResourceManager.GetString("Messages_SessionHasTimedOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have tried to access a restricted area without the proper permissions..
        /// </summary>
        internal static string Messages_TriedToAccessRestrictedArea {
            get {
                return ResourceManager.GetString("Messages_TriedToAccessRestrictedArea", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Access Denied.
        /// </summary>
        internal static string Titles_AccessDenied {
            get {
                return ResourceManager.GetString("Titles_AccessDenied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sign In.
        /// </summary>
        internal static string Titles_SignIn {
            get {
                return ResourceManager.GetString("Titles_SignIn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Session Timeout.
        /// </summary>
        internal static string Titles_Timeout {
            get {
                return ResourceManager.GetString("Titles_Timeout", resourceCulture);
            }
        }
    }
}
