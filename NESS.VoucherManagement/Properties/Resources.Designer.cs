﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NESS.VoucherManagement.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NESS.VoucherManagement.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Un fisier este blocat.
        /// </summary>
        internal static string MainWindowViewModel_CalculateVouchers_FileInUseCaption {
            get {
                return ResourceManager.GetString("MainWindowViewModel_CalculateVouchers_FileInUseCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fisierul &apos;{0}&apos; este deschis in alta aplicatie. Inchide aplicatia si mai incearca odata..
        /// </summary>
        internal static string MainWindowViewModel_CalculateVouchers_FileInUseMessage {
            get {
                return ResourceManager.GetString("MainWindowViewModel_CalculateVouchers_FileInUseMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fisierul nu este Excel.
        /// </summary>
        internal static string MainWindowViewModel_CalculateVouchers_InvalidFileTypeCaption {
            get {
                return ResourceManager.GetString("MainWindowViewModel_CalculateVouchers_InvalidFileTypeCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fisierul &apos;{0}&apos; nu este un fisier Excel (.xls sau .xlsx)..
        /// </summary>
        internal static string MainWindowViewModel_CalculateVouchers_InvalidFileTypeMessage {
            get {
                return ResourceManager.GetString("MainWindowViewModel_CalculateVouchers_InvalidFileTypeMessage", resourceCulture);
            }
        }
    }
}
