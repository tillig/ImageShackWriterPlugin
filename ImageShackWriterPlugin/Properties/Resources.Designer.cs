﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageShackWriterPlugin.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ImageShackWriterPlugin.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Images (*.JPG;*.JPEG;*.BMP;*.GIF;*.PNG;*.TIF;*.TIFF;*.PDF;*.SWF)|*.JPG;*.JPEG;*.BMP;*.GIF;*.PNG;*.TIF;*.TIFF;*.PDF;*.SWF|All files (*.*)|*.*.
        /// </summary>
        internal static string Dialog_FileSelectFilter {
            get {
                return ResourceManager.GetString("Dialog_FileSelectFilter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select image for upload to ImageShack....
        /// </summary>
        internal static string Dialog_FileSelectTitle {
            get {
                return ResourceManager.GetString("Dialog_FileSelectTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please go to the plugin options and configure your ImageShack account. Select Tools &gt; Options &gt; Plug-ins, select the ImageShack Upload plugin, and click the &apos;Options&apos; button..
        /// </summary>
        internal static string Dialog_NotConfiguredMessage {
            get {
                return ResourceManager.GetString("Dialog_NotConfiguredMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configure ImageShack Plugin.
        /// </summary>
        internal static string Dialog_NotConfiguredTitle {
            get {
                return ResourceManager.GetString("Dialog_NotConfiguredTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} v{1}.
        /// </summary>
        internal static string Label_Version {
            get {
                return ResourceManager.GetString("Label_Version", resourceCulture);
            }
        }
    }
}
