﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UNET_Tester.UNET_Service {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Exercise", Namespace="http://schemas.datacontract.org/2004/07/UNET_Service.Classes")]
    [System.SerializableAttribute()]
    public partial class Exercise : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SpecificationNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Number {
            get {
                return this.NumberField;
            }
            set {
                if ((this.NumberField.Equals(value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SpecificationName {
            get {
                return this.SpecificationNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SpecificationNameField, value) != true)) {
                    this.SpecificationNameField = value;
                    this.RaisePropertyChanged("SpecificationName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Role", Namespace="http://schemas.datacontract.org/2004/07/UNET_Service.Classes")]
    [System.SerializableAttribute()]
    public partial class Role : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Radio", Namespace="http://schemas.datacontract.org/2004/07/UNET_Service.Classes")]
    [System.SerializableAttribute()]
    public partial class Radio : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NoiseLevelField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NoiseLevel {
            get {
                return this.NoiseLevelField;
            }
            set {
                if ((this.NoiseLevelField.Equals(value) != true)) {
                    this.NoiseLevelField = value;
                    this.RaisePropertyChanged("NoiseLevel");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Instructor", Namespace="http://schemas.datacontract.org/2004/07/UNET_Service.Classes")]
    [System.SerializableAttribute()]
    public partial class Instructor : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Trainee", Namespace="http://schemas.datacontract.org/2004/07/UNET_Service.Classes")]
    [System.SerializableAttribute()]
    public partial class Trainee : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Platform", Namespace="http://schemas.datacontract.org/2004/07/UNET_Service.Classes")]
    [System.SerializableAttribute()]
    public partial class Platform : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UNET_Service.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExercises", ReplyAction="http://tempuri.org/IService1/GetExercisesResponse")]
        UNET_Tester.UNET_Service.Exercise[] GetExercises();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExercises", ReplyAction="http://tempuri.org/IService1/GetExercisesResponse")]
        System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Exercise[]> GetExercisesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoles", ReplyAction="http://tempuri.org/IService1/GetRolesResponse")]
        UNET_Tester.UNET_Service.Role[] GetRoles();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoles", ReplyAction="http://tempuri.org/IService1/GetRolesResponse")]
        System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Role[]> GetRolesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRadios", ReplyAction="http://tempuri.org/IService1/GetRadiosResponse")]
        UNET_Tester.UNET_Service.Radio[] GetRadios();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRadios", ReplyAction="http://tempuri.org/IService1/GetRadiosResponse")]
        System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Radio[]> GetRadiosAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetInstructors", ReplyAction="http://tempuri.org/IService1/GetInstructorsResponse")]
        UNET_Tester.UNET_Service.Instructor[] GetInstructors();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetInstructors", ReplyAction="http://tempuri.org/IService1/GetInstructorsResponse")]
        System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Instructor[]> GetInstructorsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTrainees", ReplyAction="http://tempuri.org/IService1/GetTraineesResponse")]
        UNET_Tester.UNET_Service.Trainee[] GetTrainees();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTrainees", ReplyAction="http://tempuri.org/IService1/GetTraineesResponse")]
        System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Trainee[]> GetTraineesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPlatforms", ReplyAction="http://tempuri.org/IService1/GetPlatformsResponse")]
        UNET_Tester.UNET_Service.Platform[] GetPlatforms();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPlatforms", ReplyAction="http://tempuri.org/IService1/GetPlatformsResponse")]
        System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Platform[]> GetPlatformsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExerciseCount", ReplyAction="http://tempuri.org/IService1/SetExerciseCountResponse")]
        bool SetExerciseCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExerciseCount", ReplyAction="http://tempuri.org/IService1/SetExerciseCountResponse")]
        System.Threading.Tasks.Task<bool> SetExerciseCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExercises", ReplyAction="http://tempuri.org/IService1/SetExercisesResponse")]
        bool SetExercises(UNET_Tester.UNET_Service.Exercise[] _exercises);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExercises", ReplyAction="http://tempuri.org/IService1/SetExercisesResponse")]
        System.Threading.Tasks.Task<bool> SetExercisesAsync(UNET_Tester.UNET_Service.Exercise[] _exercises);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRoles", ReplyAction="http://tempuri.org/IService1/SetRolesResponse")]
        bool SetRoles(UNET_Tester.UNET_Service.Role[] _role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRoles", ReplyAction="http://tempuri.org/IService1/SetRolesResponse")]
        System.Threading.Tasks.Task<bool> SetRolesAsync(UNET_Tester.UNET_Service.Role[] _role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRolesCount", ReplyAction="http://tempuri.org/IService1/SetRolesCountResponse")]
        bool SetRolesCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRolesCount", ReplyAction="http://tempuri.org/IService1/SetRolesCountResponse")]
        System.Threading.Tasks.Task<bool> SetRolesCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadios", ReplyAction="http://tempuri.org/IService1/SetRadiosResponse")]
        bool SetRadios(UNET_Tester.UNET_Service.Radio[] _radio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadios", ReplyAction="http://tempuri.org/IService1/SetRadiosResponse")]
        System.Threading.Tasks.Task<bool> SetRadiosAsync(UNET_Tester.UNET_Service.Radio[] _radio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetInstructors", ReplyAction="http://tempuri.org/IService1/SetInstructorsResponse")]
        bool SetInstructors(UNET_Tester.UNET_Service.Instructor[] _instructor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetInstructors", ReplyAction="http://tempuri.org/IService1/SetInstructorsResponse")]
        System.Threading.Tasks.Task<bool> SetInstructorsAsync(UNET_Tester.UNET_Service.Instructor[] _instructor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTrainees", ReplyAction="http://tempuri.org/IService1/SetTraineesResponse")]
        bool SetTrainees(UNET_Tester.UNET_Service.Trainee[] _trainee);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTrainees", ReplyAction="http://tempuri.org/IService1/SetTraineesResponse")]
        System.Threading.Tasks.Task<bool> SetTraineesAsync(UNET_Tester.UNET_Service.Trainee[] _trainee);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetPlatforms", ReplyAction="http://tempuri.org/IService1/SetPlatformsResponse")]
        bool SetPlatforms(UNET_Tester.UNET_Service.Platform[] _platform);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetPlatforms", ReplyAction="http://tempuri.org/IService1/SetPlatformsResponse")]
        System.Threading.Tasks.Task<bool> SetPlatformsAsync(UNET_Tester.UNET_Service.Platform[] _platform);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : UNET_Tester.UNET_Service.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<UNET_Tester.UNET_Service.IService1>, UNET_Tester.UNET_Service.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public UNET_Tester.UNET_Service.Exercise[] GetExercises() {
            return base.Channel.GetExercises();
        }
        
        public System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Exercise[]> GetExercisesAsync() {
            return base.Channel.GetExercisesAsync();
        }
        
        public UNET_Tester.UNET_Service.Role[] GetRoles() {
            return base.Channel.GetRoles();
        }
        
        public System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Role[]> GetRolesAsync() {
            return base.Channel.GetRolesAsync();
        }
        
        public UNET_Tester.UNET_Service.Radio[] GetRadios() {
            return base.Channel.GetRadios();
        }
        
        public System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Radio[]> GetRadiosAsync() {
            return base.Channel.GetRadiosAsync();
        }
        
        public UNET_Tester.UNET_Service.Instructor[] GetInstructors() {
            return base.Channel.GetInstructors();
        }
        
        public System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Instructor[]> GetInstructorsAsync() {
            return base.Channel.GetInstructorsAsync();
        }
        
        public UNET_Tester.UNET_Service.Trainee[] GetTrainees() {
            return base.Channel.GetTrainees();
        }
        
        public System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Trainee[]> GetTraineesAsync() {
            return base.Channel.GetTraineesAsync();
        }
        
        public UNET_Tester.UNET_Service.Platform[] GetPlatforms() {
            return base.Channel.GetPlatforms();
        }
        
        public System.Threading.Tasks.Task<UNET_Tester.UNET_Service.Platform[]> GetPlatformsAsync() {
            return base.Channel.GetPlatformsAsync();
        }
        
        public bool SetExerciseCount(int _count) {
            return base.Channel.SetExerciseCount(_count);
        }
        
        public System.Threading.Tasks.Task<bool> SetExerciseCountAsync(int _count) {
            return base.Channel.SetExerciseCountAsync(_count);
        }
        
        public bool SetExercises(UNET_Tester.UNET_Service.Exercise[] _exercises) {
            return base.Channel.SetExercises(_exercises);
        }
        
        public System.Threading.Tasks.Task<bool> SetExercisesAsync(UNET_Tester.UNET_Service.Exercise[] _exercises) {
            return base.Channel.SetExercisesAsync(_exercises);
        }
        
        public bool SetRoles(UNET_Tester.UNET_Service.Role[] _role) {
            return base.Channel.SetRoles(_role);
        }
        
        public System.Threading.Tasks.Task<bool> SetRolesAsync(UNET_Tester.UNET_Service.Role[] _role) {
            return base.Channel.SetRolesAsync(_role);
        }
        
        public bool SetRolesCount(int _count) {
            return base.Channel.SetRolesCount(_count);
        }
        
        public System.Threading.Tasks.Task<bool> SetRolesCountAsync(int _count) {
            return base.Channel.SetRolesCountAsync(_count);
        }
        
        public bool SetRadios(UNET_Tester.UNET_Service.Radio[] _radio) {
            return base.Channel.SetRadios(_radio);
        }
        
        public System.Threading.Tasks.Task<bool> SetRadiosAsync(UNET_Tester.UNET_Service.Radio[] _radio) {
            return base.Channel.SetRadiosAsync(_radio);
        }
        
        public bool SetInstructors(UNET_Tester.UNET_Service.Instructor[] _instructor) {
            return base.Channel.SetInstructors(_instructor);
        }
        
        public System.Threading.Tasks.Task<bool> SetInstructorsAsync(UNET_Tester.UNET_Service.Instructor[] _instructor) {
            return base.Channel.SetInstructorsAsync(_instructor);
        }
        
        public bool SetTrainees(UNET_Tester.UNET_Service.Trainee[] _trainee) {
            return base.Channel.SetTrainees(_trainee);
        }
        
        public System.Threading.Tasks.Task<bool> SetTraineesAsync(UNET_Tester.UNET_Service.Trainee[] _trainee) {
            return base.Channel.SetTraineesAsync(_trainee);
        }
        
        public bool SetPlatforms(UNET_Tester.UNET_Service.Platform[] _platform) {
            return base.Channel.SetPlatforms(_platform);
        }
        
        public System.Threading.Tasks.Task<bool> SetPlatformsAsync(UNET_Tester.UNET_Service.Platform[] _platform) {
            return base.Channel.SetPlatformsAsync(_platform);
        }
    }
}
