﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnaControl.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Column")]
        public string DefaultChartType {
            get {
                return ((string)(this["DefaultChartType"]));
            }
            set {
                this["DefaultChartType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("产能分析")]
        public string DefaultDataType {
            get {
                return ((string)(this["DefaultDataType"]));
            }
            set {
                this["DefaultDataType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2010-01-01")]
        public global::System.DateTime DefaultDateTimeStart {
            get {
                return ((global::System.DateTime)(this["DefaultDateTimeStart"]));
            }
            set {
                this["DefaultDateTimeStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("02/26/2015 15:30:00")]
        public global::System.DateTime DefaultDateTimeEnd {
            get {
                return ((global::System.DateTime)(this["DefaultDateTimeEnd"]));
            }
            set {
                this["DefaultDateTimeEnd"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string DefaultTestItem {
            get {
                return ((string)(this["DefaultTestItem"]));
            }
            set {
                this["DefaultTestItem"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DefaultRemoveRepeat {
            get {
                return ((bool)(this["DefaultRemoveRepeat"]));
            }
            set {
                this["DefaultRemoveRepeat"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DefaultRemovePassData {
            get {
                return ((bool)(this["DefaultRemovePassData"]));
            }
            set {
                this["DefaultRemovePassData"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DefaultRemoveExceptFail {
            get {
                return ((bool)(this["DefaultRemoveExceptFail"]));
            }
            set {
                this["DefaultRemoveExceptFail"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int DefaultShowNum {
            get {
                return ((int)(this["DefaultShowNum"]));
            }
            set {
                this["DefaultShowNum"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-999999")]
        public double AbnormalLowData {
            get {
                return ((double)(this["AbnormalLowData"]));
            }
            set {
                this["AbnormalLowData"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("999999")]
        public double AbnormalUpData {
            get {
                return ((double)(this["AbnormalUpData"]));
            }
            set {
                this["AbnormalUpData"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%")]
        public string ProductType {
            get {
                return ((string)(this["ProductType"]));
            }
            set {
                this["ProductType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AutoWildcard {
            get {
                return ((bool)(this["AutoWildcard"]));
            }
            set {
                this["AutoWildcard"] = value;
            }
        }
    }
}
