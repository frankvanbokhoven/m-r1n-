﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UNET_Trainer_Trainee.UNET_Service {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UNET_Service.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExercises", ReplyAction="http://tempuri.org/IService1/GetExercisesResponse")]
        PJSUA2Implementation.UNET_Service.Exercise[] GetExercises();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExercises", ReplyAction="http://tempuri.org/IService1/GetExercisesResponse")]
        System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Exercise[]> GetExercisesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoles", ReplyAction="http://tempuri.org/IService1/GetRolesResponse")]
        PJSUA2Implementation.UNET_Service.Role[] GetRoles();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoles", ReplyAction="http://tempuri.org/IService1/GetRolesResponse")]
        System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Role[]> GetRolesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRadios", ReplyAction="http://tempuri.org/IService1/GetRadiosResponse")]
        PJSUA2Implementation.UNET_Service.Radio[] GetRadios();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRadios", ReplyAction="http://tempuri.org/IService1/GetRadiosResponse")]
        System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Radio[]> GetRadiosAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetInstructors", ReplyAction="http://tempuri.org/IService1/GetInstructorsResponse")]
        PJSUA2Implementation.UNET_Service.Instructor[] GetInstructors();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetInstructors", ReplyAction="http://tempuri.org/IService1/GetInstructorsResponse")]
        System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Instructor[]> GetInstructorsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTrainees", ReplyAction="http://tempuri.org/IService1/GetTraineesResponse")]
        PJSUA2Implementation.UNET_Service.Trainee[] GetTrainees();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTrainees", ReplyAction="http://tempuri.org/IService1/GetTraineesResponse")]
        System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Trainee[]> GetTraineesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPlatforms", ReplyAction="http://tempuri.org/IService1/GetPlatformsResponse")]
        PJSUA2Implementation.UNET_Service.Platform[] GetPlatforms();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPlatforms", ReplyAction="http://tempuri.org/IService1/GetPlatformsResponse")]
        System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Platform[]> GetPlatformsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExerciseInfo", ReplyAction="http://tempuri.org/IService1/GetExerciseInfoResponse")]
        PJSUA2Implementation.UNET_Service.CurrentInfo GetExerciseInfo(int _traineeID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExerciseInfo", ReplyAction="http://tempuri.org/IService1/GetExerciseInfoResponse")]
        System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.CurrentInfo> GetExerciseInfoAsync(int _traineeID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTraineeStatus", ReplyAction="http://tempuri.org/IService1/GetTraineeStatusResponse")]
        bool[] GetTraineeStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTraineeStatus", ReplyAction="http://tempuri.org/IService1/GetTraineeStatusResponse")]
        System.Threading.Tasks.Task<bool[]> GetTraineeStatusAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTraineeStatusChanged", ReplyAction="http://tempuri.org/IService1/GetTraineeStatusChangedResponse")]
        bool GetTraineeStatusChanged();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTraineeStatusChanged", ReplyAction="http://tempuri.org/IService1/GetTraineeStatusChangedResponse")]
        System.Threading.Tasks.Task<bool> GetTraineeStatusChangedAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTraineeStatusChanged", ReplyAction="http://tempuri.org/IService1/SetTraineeStatusChangedResponse")]
        bool SetTraineeStatusChanged(int _traineeId, bool _changed);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTraineeStatusChanged", ReplyAction="http://tempuri.org/IService1/SetTraineeStatusChangedResponse")]
        System.Threading.Tasks.Task<bool> SetTraineeStatusChangedAsync(int _traineeId, bool _changed);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetSIPStatusMessage", ReplyAction="http://tempuri.org/IService1/GetSIPStatusMessageResponse")]
        string GetSIPStatusMessage(string _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetSIPStatusMessage", ReplyAction="http://tempuri.org/IService1/GetSIPStatusMessageResponse")]
        System.Threading.Tasks.Task<string> GetSIPStatusMessageAsync(string _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetSIPStatusMessage", ReplyAction="http://tempuri.org/IService1/SetSIPStatusMessageResponse")]
        bool SetSIPStatusMessage(string _message, string _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetSIPStatusMessage", ReplyAction="http://tempuri.org/IService1/SetSIPStatusMessageResponse")]
        System.Threading.Tasks.Task<bool> SetSIPStatusMessageAsync(string _message, string _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ClearStatusMessages", ReplyAction="http://tempuri.org/IService1/ClearStatusMessagesResponse")]
        bool ClearStatusMessages(string _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ClearStatusMessages", ReplyAction="http://tempuri.org/IService1/ClearStatusMessagesResponse")]
        System.Threading.Tasks.Task<bool> ClearStatusMessagesAsync(string _id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegisterTrainee", ReplyAction="http://tempuri.org/IService1/RegisterTraineeResponse")]
        bool RegisterTrainee(PJSUA2Implementation.UNET_Service.CurrentInfo _currentInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegisterTrainee", ReplyAction="http://tempuri.org/IService1/RegisterTraineeResponse")]
        System.Threading.Tasks.Task<bool> RegisterTraineeAsync(PJSUA2Implementation.UNET_Service.CurrentInfo _currentInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExerciseCount", ReplyAction="http://tempuri.org/IService1/SetExerciseCountResponse")]
        bool SetExerciseCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExerciseCount", ReplyAction="http://tempuri.org/IService1/SetExerciseCountResponse")]
        System.Threading.Tasks.Task<bool> SetExerciseCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExercises", ReplyAction="http://tempuri.org/IService1/SetExercisesResponse")]
        bool SetExercises(PJSUA2Implementation.UNET_Service.Exercise[] _exercises);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExercises", ReplyAction="http://tempuri.org/IService1/SetExercisesResponse")]
        System.Threading.Tasks.Task<bool> SetExercisesAsync(PJSUA2Implementation.UNET_Service.Exercise[] _exercises);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRoles", ReplyAction="http://tempuri.org/IService1/SetRolesResponse")]
        bool SetRoles(PJSUA2Implementation.UNET_Service.Role[] _role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRoles", ReplyAction="http://tempuri.org/IService1/SetRolesResponse")]
        System.Threading.Tasks.Task<bool> SetRolesAsync(PJSUA2Implementation.UNET_Service.Role[] _role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRolesCount", ReplyAction="http://tempuri.org/IService1/SetRolesCountResponse")]
        bool SetRolesCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRolesCount", ReplyAction="http://tempuri.org/IService1/SetRolesCountResponse")]
        System.Threading.Tasks.Task<bool> SetRolesCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadios", ReplyAction="http://tempuri.org/IService1/SetRadiosResponse")]
        bool SetRadios(PJSUA2Implementation.UNET_Service.Radio[] _radio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadios", ReplyAction="http://tempuri.org/IService1/SetRadiosResponse")]
        System.Threading.Tasks.Task<bool> SetRadiosAsync(PJSUA2Implementation.UNET_Service.Radio[] _radio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadiosCount", ReplyAction="http://tempuri.org/IService1/SetRadiosCountResponse")]
        bool SetRadiosCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadiosCount", ReplyAction="http://tempuri.org/IService1/SetRadiosCountResponse")]
        System.Threading.Tasks.Task<bool> SetRadiosCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTraineesCount", ReplyAction="http://tempuri.org/IService1/SetTraineesCountResponse")]
        bool SetTraineesCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTraineesCount", ReplyAction="http://tempuri.org/IService1/SetTraineesCountResponse")]
        System.Threading.Tasks.Task<bool> SetTraineesCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetInstructors", ReplyAction="http://tempuri.org/IService1/SetInstructorsResponse")]
        bool SetInstructors(PJSUA2Implementation.UNET_Service.Instructor[] _instructor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetInstructors", ReplyAction="http://tempuri.org/IService1/SetInstructorsResponse")]
        System.Threading.Tasks.Task<bool> SetInstructorsAsync(PJSUA2Implementation.UNET_Service.Instructor[] _instructor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTrainees", ReplyAction="http://tempuri.org/IService1/SetTraineesResponse")]
        bool SetTrainees(PJSUA2Implementation.UNET_Service.Trainee[] _trainee);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTrainees", ReplyAction="http://tempuri.org/IService1/SetTraineesResponse")]
        System.Threading.Tasks.Task<bool> SetTraineesAsync(PJSUA2Implementation.UNET_Service.Trainee[] _trainee);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetPlatforms", ReplyAction="http://tempuri.org/IService1/SetPlatformsResponse")]
        bool SetPlatforms(PJSUA2Implementation.UNET_Service.Platform[] _platform);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetPlatforms", ReplyAction="http://tempuri.org/IService1/SetPlatformsResponse")]
        System.Threading.Tasks.Task<bool> SetPlatformsAsync(PJSUA2Implementation.UNET_Service.Platform[] _platform);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : UNET_Trainer_Trainee.UNET_Service.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<UNET_Trainer_Trainee.UNET_Service.IService1>, UNET_Trainer_Trainee.UNET_Service.IService1 {
        
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
        
        public PJSUA2Implementation.UNET_Service.Exercise[] GetExercises() {
            return base.Channel.GetExercises();
        }
        
        public System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Exercise[]> GetExercisesAsync() {
            return base.Channel.GetExercisesAsync();
        }
        
        public PJSUA2Implementation.UNET_Service.Role[] GetRoles() {
            return base.Channel.GetRoles();
        }
        
        public System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Role[]> GetRolesAsync() {
            return base.Channel.GetRolesAsync();
        }
        
        public PJSUA2Implementation.UNET_Service.Radio[] GetRadios() {
            return base.Channel.GetRadios();
        }
        
        public System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Radio[]> GetRadiosAsync() {
            return base.Channel.GetRadiosAsync();
        }
        
        public PJSUA2Implementation.UNET_Service.Instructor[] GetInstructors() {
            return base.Channel.GetInstructors();
        }
        
        public System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Instructor[]> GetInstructorsAsync() {
            return base.Channel.GetInstructorsAsync();
        }
        
        public PJSUA2Implementation.UNET_Service.Trainee[] GetTrainees() {
            return base.Channel.GetTrainees();
        }
        
        public System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Trainee[]> GetTraineesAsync() {
            return base.Channel.GetTraineesAsync();
        }
        
        public PJSUA2Implementation.UNET_Service.Platform[] GetPlatforms() {
            return base.Channel.GetPlatforms();
        }
        
        public System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.Platform[]> GetPlatformsAsync() {
            return base.Channel.GetPlatformsAsync();
        }
        
        public PJSUA2Implementation.UNET_Service.CurrentInfo GetExerciseInfo(int _traineeID) {
            return base.Channel.GetExerciseInfo(_traineeID);
        }
        
        public System.Threading.Tasks.Task<PJSUA2Implementation.UNET_Service.CurrentInfo> GetExerciseInfoAsync(int _traineeID) {
            return base.Channel.GetExerciseInfoAsync(_traineeID);
        }
        
        public bool[] GetTraineeStatus() {
            return base.Channel.GetTraineeStatus();
        }
        
        public System.Threading.Tasks.Task<bool[]> GetTraineeStatusAsync() {
            return base.Channel.GetTraineeStatusAsync();
        }
        
        public bool GetTraineeStatusChanged() {
            return base.Channel.GetTraineeStatusChanged();
        }
        
        public System.Threading.Tasks.Task<bool> GetTraineeStatusChangedAsync() {
            return base.Channel.GetTraineeStatusChangedAsync();
        }
        
        public bool SetTraineeStatusChanged(int _traineeId, bool _changed) {
            return base.Channel.SetTraineeStatusChanged(_traineeId, _changed);
        }
        
        public System.Threading.Tasks.Task<bool> SetTraineeStatusChangedAsync(int _traineeId, bool _changed) {
            return base.Channel.SetTraineeStatusChangedAsync(_traineeId, _changed);
        }
        
        public string GetSIPStatusMessage(string _id) {
            return base.Channel.GetSIPStatusMessage(_id);
        }
        
        public System.Threading.Tasks.Task<string> GetSIPStatusMessageAsync(string _id) {
            return base.Channel.GetSIPStatusMessageAsync(_id);
        }
        
        public bool SetSIPStatusMessage(string _message, string _id) {
            return base.Channel.SetSIPStatusMessage(_message, _id);
        }
        
        public System.Threading.Tasks.Task<bool> SetSIPStatusMessageAsync(string _message, string _id) {
            return base.Channel.SetSIPStatusMessageAsync(_message, _id);
        }
        
        public bool ClearStatusMessages(string _id) {
            return base.Channel.ClearStatusMessages(_id);
        }
        
        public System.Threading.Tasks.Task<bool> ClearStatusMessagesAsync(string _id) {
            return base.Channel.ClearStatusMessagesAsync(_id);
        }
        
        public bool RegisterTrainee(PJSUA2Implementation.UNET_Service.CurrentInfo _currentInfo) {
            return base.Channel.RegisterTrainee(_currentInfo);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterTraineeAsync(PJSUA2Implementation.UNET_Service.CurrentInfo _currentInfo) {
            return base.Channel.RegisterTraineeAsync(_currentInfo);
        }
        
        public bool SetExerciseCount(int _count) {
            return base.Channel.SetExerciseCount(_count);
        }
        
        public System.Threading.Tasks.Task<bool> SetExerciseCountAsync(int _count) {
            return base.Channel.SetExerciseCountAsync(_count);
        }
        
        public bool SetExercises(PJSUA2Implementation.UNET_Service.Exercise[] _exercises) {
            return base.Channel.SetExercises(_exercises);
        }
        
        public System.Threading.Tasks.Task<bool> SetExercisesAsync(PJSUA2Implementation.UNET_Service.Exercise[] _exercises) {
            return base.Channel.SetExercisesAsync(_exercises);
        }
        
        public bool SetRoles(PJSUA2Implementation.UNET_Service.Role[] _role) {
            return base.Channel.SetRoles(_role);
        }
        
        public System.Threading.Tasks.Task<bool> SetRolesAsync(PJSUA2Implementation.UNET_Service.Role[] _role) {
            return base.Channel.SetRolesAsync(_role);
        }
        
        public bool SetRolesCount(int _count) {
            return base.Channel.SetRolesCount(_count);
        }
        
        public System.Threading.Tasks.Task<bool> SetRolesCountAsync(int _count) {
            return base.Channel.SetRolesCountAsync(_count);
        }
        
        public bool SetRadios(PJSUA2Implementation.UNET_Service.Radio[] _radio) {
            return base.Channel.SetRadios(_radio);
        }
        
        public System.Threading.Tasks.Task<bool> SetRadiosAsync(PJSUA2Implementation.UNET_Service.Radio[] _radio) {
            return base.Channel.SetRadiosAsync(_radio);
        }
        
        public bool SetRadiosCount(int _count) {
            return base.Channel.SetRadiosCount(_count);
        }
        
        public System.Threading.Tasks.Task<bool> SetRadiosCountAsync(int _count) {
            return base.Channel.SetRadiosCountAsync(_count);
        }
        
        public bool SetTraineesCount(int _count) {
            return base.Channel.SetTraineesCount(_count);
        }
        
        public System.Threading.Tasks.Task<bool> SetTraineesCountAsync(int _count) {
            return base.Channel.SetTraineesCountAsync(_count);
        }
        
        public bool SetInstructors(PJSUA2Implementation.UNET_Service.Instructor[] _instructor) {
            return base.Channel.SetInstructors(_instructor);
        }
        
        public System.Threading.Tasks.Task<bool> SetInstructorsAsync(PJSUA2Implementation.UNET_Service.Instructor[] _instructor) {
            return base.Channel.SetInstructorsAsync(_instructor);
        }
        
        public bool SetTrainees(PJSUA2Implementation.UNET_Service.Trainee[] _trainee) {
            return base.Channel.SetTrainees(_trainee);
        }
        
        public System.Threading.Tasks.Task<bool> SetTraineesAsync(PJSUA2Implementation.UNET_Service.Trainee[] _trainee) {
            return base.Channel.SetTraineesAsync(_trainee);
        }
        
        public bool SetPlatforms(PJSUA2Implementation.UNET_Service.Platform[] _platform) {
            return base.Channel.SetPlatforms(_platform);
        }
        
        public System.Threading.Tasks.Task<bool> SetPlatformsAsync(PJSUA2Implementation.UNET_Service.Platform[] _platform) {
            return base.Channel.SetPlatformsAsync(_platform);
        }
    }
}
