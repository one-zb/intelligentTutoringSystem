﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace KRLab.DiagramEditor {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5000")]
        public int DiagramWidth {
            get {
                return ((int)(this["DiagramWidth"]));
            }
            set {
                this["DiagramWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4000")]
        public int DiagramHeight {
            get {
                return ((int)(this["DiagramHeight"]));
            }
            set {
                this["DiagramHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UsePrecisionSnapping {
            get {
                return ((bool)(this["UsePrecisionSnapping"]));
            }
            set {
                this["UsePrecisionSnapping"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AsNeeded")]
        public global::KRLab.DiagramEditor.NetworkDiagram.ChevronMode ShowChevron {
            get {
                return ((global::KRLab.DiagramEditor.NetworkDiagram.ChevronMode)(this["ShowChevron"]));
            }
            set {
                this["ShowChevron"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowType {
            get {
                return ((bool)(this["ShowType"]));
            }
            set {
                this["ShowType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowParameters {
            get {
                return ((bool)(this["ShowParameters"]));
            }
            set {
                this["ShowParameters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowParameterNames {
            get {
                return ((bool)(this["ShowParameterNames"]));
            }
            set {
                this["ShowParameterNames"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowInitialValue {
            get {
                return ((bool)(this["ShowInitialValue"]));
            }
            set {
                this["ShowInitialValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Never")]
        public global::KRLab.DiagramEditor.NetworkDiagram.ClearTypeMode UseClearType {
            get {
                return ((global::KRLab.DiagramEditor.NetworkDiagram.ClearTypeMode)(this["UseClearType"]));
            }
            set {
                this["UseClearType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UseClearTypeForImages {
            get {
                return ((bool)(this["UseClearTypeForImages"]));
            }
            set {
                this["UseClearTypeForImages"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::KRLab.DiagramEditor.NetworkDiagram.Dialogs.PrintingSettings PrintingSettings {
            get {
                return ((global::KRLab.DiagramEditor.NetworkDiagram.Dialogs.PrintingSettings)(this["PrintingSettings"]));
            }
            set {
                this["PrintingSettings"] = value;
            }
        }
    }
}
