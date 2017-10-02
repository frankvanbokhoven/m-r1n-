﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UNET_ServiceStatus.UNET_Service {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UNET_Service.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/StartService", ReplyAction="http://tempuri.org/IService1/StartServiceResponse")]
        bool StartService();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/StartService", ReplyAction="http://tempuri.org/IService1/StartServiceResponse")]
        System.Threading.Tasks.Task<bool> StartServiceAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAllInstructorData", ReplyAction="http://tempuri.org/IService1/GetAllInstructorDataResponse")]
        UNET_Classes.Instructor GetAllInstructorData(int _instructorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAllInstructorData", ReplyAction="http://tempuri.org/IService1/GetAllInstructorDataResponse")]
        System.Threading.Tasks.Task<UNET_Classes.Instructor> GetAllInstructorDataAsync(int _instructorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExercises", ReplyAction="http://tempuri.org/IService1/GetExercisesResponse")]
        UNET_Classes.Exercise[] GetExercises();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExercises", ReplyAction="http://tempuri.org/IService1/GetExercisesResponse")]
        System.Threading.Tasks.Task<UNET_Classes.Exercise[]> GetExercisesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoles", ReplyAction="http://tempuri.org/IService1/GetRolesResponse")]
        UNET_Classes.Role[] GetRoles();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoles", ReplyAction="http://tempuri.org/IService1/GetRolesResponse")]
        System.Threading.Tasks.Task<UNET_Classes.Role[]> GetRolesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRadios", ReplyAction="http://tempuri.org/IService1/GetRadiosResponse")]
        UNET_Classes.Radio[] GetRadios();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRadios", ReplyAction="http://tempuri.org/IService1/GetRadiosResponse")]
        System.Threading.Tasks.Task<UNET_Classes.Radio[]> GetRadiosAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetInstructors", ReplyAction="http://tempuri.org/IService1/GetInstructorsResponse")]
        UNET_Classes.Instructor[] GetInstructors();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetInstructors", ReplyAction="http://tempuri.org/IService1/GetInstructorsResponse")]
        System.Threading.Tasks.Task<UNET_Classes.Instructor[]> GetInstructorsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetInstructorsCount", ReplyAction="http://tempuri.org/IService1/SetInstructorsCountResponse")]
        bool SetInstructorsCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetInstructorsCount", ReplyAction="http://tempuri.org/IService1/SetInstructorsCountResponse")]
        System.Threading.Tasks.Task<bool> SetInstructorsCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTrainees", ReplyAction="http://tempuri.org/IService1/GetTraineesResponse")]
        UNET_Classes.Trainee[] GetTrainees();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTrainees", ReplyAction="http://tempuri.org/IService1/GetTraineesResponse")]
        System.Threading.Tasks.Task<UNET_Classes.Trainee[]> GetTraineesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPlatforms", ReplyAction="http://tempuri.org/IService1/GetPlatformsResponse")]
        UNET_Classes.Platform[] GetPlatforms();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPlatforms", ReplyAction="http://tempuri.org/IService1/GetPlatformsResponse")]
        System.Threading.Tasks.Task<UNET_Classes.Platform[]> GetPlatformsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExerciseInfo", ReplyAction="http://tempuri.org/IService1/GetExerciseInfoResponse")]
        UNET_Classes.CurrentInfo GetExerciseInfo(int _traineeID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetExerciseInfo", ReplyAction="http://tempuri.org/IService1/GetExerciseInfoResponse")]
        System.Threading.Tasks.Task<UNET_Classes.CurrentInfo> GetExerciseInfoAsync(int _traineeID);
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRoleAssignedStatus", ReplyAction="http://tempuri.org/IService1/SetRoleAssignedStatusResponse")]
        bool SetRoleAssignedStatus(int _instructorID, int _exersiseID, int _role, bool _add);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRoleAssignedStatus", ReplyAction="http://tempuri.org/IService1/SetRoleAssignedStatusResponse")]
        System.Threading.Tasks.Task<bool> SetRoleAssignedStatusAsync(int _instructorID, int _exersiseID, int _role, bool _add);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTraineeAssignedStatus", ReplyAction="http://tempuri.org/IService1/SetTraineeAssignedStatusResponse")]
        bool SetTraineeAssignedStatus(int _instructorID, int _exersiseID, int _traineeID, bool _add);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTraineeAssignedStatus", ReplyAction="http://tempuri.org/IService1/SetTraineeAssignedStatusResponse")]
        System.Threading.Tasks.Task<bool> SetTraineeAssignedStatusAsync(int _instructorID, int _exersiseID, int _traineeID, bool _add);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetNoiseLevelChanged", ReplyAction="http://tempuri.org/IService1/SetNoiseLevelChangedResponse")]
        bool SetNoiseLevelChanged(int _radioId, bool _changed);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetNoiseLevelChanged", ReplyAction="http://tempuri.org/IService1/SetNoiseLevelChangedResponse")]
        System.Threading.Tasks.Task<bool> SetNoiseLevelChangedAsync(int _radioId, bool _changed);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetNoiseLevel", ReplyAction="http://tempuri.org/IService1/SetNoiseLevelResponse")]
        bool SetNoiseLevel(int _radioID, int _noiselevel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetNoiseLevel", ReplyAction="http://tempuri.org/IService1/SetNoiseLevelResponse")]
        System.Threading.Tasks.Task<bool> SetNoiseLevelAsync(int _radioID, int _noiselevel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNoiseLevelChanged", ReplyAction="http://tempuri.org/IService1/GetNoiseLevelChangedResponse")]
        bool GetNoiseLevelChanged();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNoiseLevelChanged", ReplyAction="http://tempuri.org/IService1/GetNoiseLevelChangedResponse")]
        System.Threading.Tasks.Task<bool> GetNoiseLevelChangedAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNoiseLevel", ReplyAction="http://tempuri.org/IService1/GetNoiseLevelResponse")]
        int GetNoiseLevel(int _radioID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNoiseLevel", ReplyAction="http://tempuri.org/IService1/GetNoiseLevelResponse")]
        System.Threading.Tasks.Task<int> GetNoiseLevelAsync(int _radioID);
        
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
        bool RegisterTrainee(UNET_Classes.CurrentInfo _currentInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegisterTrainee", ReplyAction="http://tempuri.org/IService1/RegisterTraineeResponse")]
        System.Threading.Tasks.Task<bool> RegisterTraineeAsync(UNET_Classes.CurrentInfo _currentInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExerciseCount", ReplyAction="http://tempuri.org/IService1/SetExerciseCountResponse")]
        bool SetExerciseCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExerciseCount", ReplyAction="http://tempuri.org/IService1/SetExerciseCountResponse")]
        System.Threading.Tasks.Task<bool> SetExerciseCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExerciseSelected", ReplyAction="http://tempuri.org/IService1/SetExerciseSelectedResponse")]
        bool SetExerciseSelected(int _instructor, int _exerciseIndex, bool _select);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExerciseSelected", ReplyAction="http://tempuri.org/IService1/SetExerciseSelectedResponse")]
        System.Threading.Tasks.Task<bool> SetExerciseSelectedAsync(int _instructor, int _exerciseIndex, bool _select);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExercises", ReplyAction="http://tempuri.org/IService1/SetExercisesResponse")]
        bool SetExercises(UNET_Classes.Exercise[] _exercises);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetExercises", ReplyAction="http://tempuri.org/IService1/SetExercisesResponse")]
        System.Threading.Tasks.Task<bool> SetExercisesAsync(UNET_Classes.Exercise[] _exercises);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRoles", ReplyAction="http://tempuri.org/IService1/SetRolesResponse")]
        bool SetRoles(UNET_Classes.Role[] _role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRoles", ReplyAction="http://tempuri.org/IService1/SetRolesResponse")]
        System.Threading.Tasks.Task<bool> SetRolesAsync(UNET_Classes.Role[] _role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRolesCount", ReplyAction="http://tempuri.org/IService1/SetRolesCountResponse")]
        bool SetRolesCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRolesCount", ReplyAction="http://tempuri.org/IService1/SetRolesCountResponse")]
        System.Threading.Tasks.Task<bool> SetRolesCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadios", ReplyAction="http://tempuri.org/IService1/SetRadiosResponse")]
        bool SetRadios(UNET_Classes.Radio[] _radio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadios", ReplyAction="http://tempuri.org/IService1/SetRadiosResponse")]
        System.Threading.Tasks.Task<bool> SetRadiosAsync(UNET_Classes.Radio[] _radio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadioStatus", ReplyAction="http://tempuri.org/IService1/SetRadioStatusResponse")]
        bool SetRadioStatus(int _radioNumber, UNET_Classes.UNETRadioState _state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadioStatus", ReplyAction="http://tempuri.org/IService1/SetRadioStatusResponse")]
        System.Threading.Tasks.Task<bool> SetRadioStatusAsync(int _radioNumber, UNET_Classes.UNETRadioState _state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadiosCount", ReplyAction="http://tempuri.org/IService1/SetRadiosCountResponse")]
        bool SetRadiosCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRadiosCount", ReplyAction="http://tempuri.org/IService1/SetRadiosCountResponse")]
        System.Threading.Tasks.Task<bool> SetRadiosCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTraineesCount", ReplyAction="http://tempuri.org/IService1/SetTraineesCountResponse")]
        bool SetTraineesCount(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTraineesCount", ReplyAction="http://tempuri.org/IService1/SetTraineesCountResponse")]
        System.Threading.Tasks.Task<bool> SetTraineesCountAsync(int _count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetInstructors", ReplyAction="http://tempuri.org/IService1/SetInstructorsResponse")]
        bool SetInstructors(UNET_Classes.Instructor[] _instructor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetInstructors", ReplyAction="http://tempuri.org/IService1/SetInstructorsResponse")]
        System.Threading.Tasks.Task<bool> SetInstructorsAsync(UNET_Classes.Instructor[] _instructor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTrainees", ReplyAction="http://tempuri.org/IService1/SetTraineesResponse")]
        bool SetTrainees(UNET_Classes.Trainee[] _trainee);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetTrainees", ReplyAction="http://tempuri.org/IService1/SetTraineesResponse")]
        System.Threading.Tasks.Task<bool> SetTraineesAsync(UNET_Classes.Trainee[] _trainee);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetPlatforms", ReplyAction="http://tempuri.org/IService1/SetPlatformsResponse")]
        bool SetPlatforms(UNET_Classes.Platform[] _platform);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetPlatforms", ReplyAction="http://tempuri.org/IService1/SetPlatformsResponse")]
        System.Threading.Tasks.Task<bool> SetPlatformsAsync(UNET_Classes.Platform[] _platform);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : UNET_ServiceStatus.UNET_Service.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<UNET_ServiceStatus.UNET_Service.IService1>, UNET_ServiceStatus.UNET_Service.IService1 {
        
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
        
        public bool StartService() {
            return base.Channel.StartService();
        }
        
        public System.Threading.Tasks.Task<bool> StartServiceAsync() {
            return base.Channel.StartServiceAsync();
        }
        
        public UNET_Classes.Instructor GetAllInstructorData(int _instructorID) {
            return base.Channel.GetAllInstructorData(_instructorID);
        }
        
        public System.Threading.Tasks.Task<UNET_Classes.Instructor> GetAllInstructorDataAsync(int _instructorID) {
            return base.Channel.GetAllInstructorDataAsync(_instructorID);
        }
        
        public UNET_Classes.Exercise[] GetExercises() {
            return base.Channel.GetExercises();
        }
        
        public System.Threading.Tasks.Task<UNET_Classes.Exercise[]> GetExercisesAsync() {
            return base.Channel.GetExercisesAsync();
        }
        
        public UNET_Classes.Role[] GetRoles() {
            return base.Channel.GetRoles();
        }
        
        public System.Threading.Tasks.Task<UNET_Classes.Role[]> GetRolesAsync() {
            return base.Channel.GetRolesAsync();
        }
        
        public UNET_Classes.Radio[] GetRadios() {
            return base.Channel.GetRadios();
        }
        
        public System.Threading.Tasks.Task<UNET_Classes.Radio[]> GetRadiosAsync() {
            return base.Channel.GetRadiosAsync();
        }
        
        public UNET_Classes.Instructor[] GetInstructors() {
            return base.Channel.GetInstructors();
        }
        
        public System.Threading.Tasks.Task<UNET_Classes.Instructor[]> GetInstructorsAsync() {
            return base.Channel.GetInstructorsAsync();
        }
        
        public bool SetInstructorsCount(int _count) {
            return base.Channel.SetInstructorsCount(_count);
        }
        
        public System.Threading.Tasks.Task<bool> SetInstructorsCountAsync(int _count) {
            return base.Channel.SetInstructorsCountAsync(_count);
        }
        
        public UNET_Classes.Trainee[] GetTrainees() {
            return base.Channel.GetTrainees();
        }
        
        public System.Threading.Tasks.Task<UNET_Classes.Trainee[]> GetTraineesAsync() {
            return base.Channel.GetTraineesAsync();
        }
        
        public UNET_Classes.Platform[] GetPlatforms() {
            return base.Channel.GetPlatforms();
        }
        
        public System.Threading.Tasks.Task<UNET_Classes.Platform[]> GetPlatformsAsync() {
            return base.Channel.GetPlatformsAsync();
        }
        
        public UNET_Classes.CurrentInfo GetExerciseInfo(int _traineeID) {
            return base.Channel.GetExerciseInfo(_traineeID);
        }
        
        public System.Threading.Tasks.Task<UNET_Classes.CurrentInfo> GetExerciseInfoAsync(int _traineeID) {
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
        
        public bool SetRoleAssignedStatus(int _instructorID, int _exersiseID, int _role, bool _add) {
            return base.Channel.SetRoleAssignedStatus(_instructorID, _exersiseID, _role, _add);
        }
        
        public System.Threading.Tasks.Task<bool> SetRoleAssignedStatusAsync(int _instructorID, int _exersiseID, int _role, bool _add) {
            return base.Channel.SetRoleAssignedStatusAsync(_instructorID, _exersiseID, _role, _add);
        }
        
        public bool SetTraineeAssignedStatus(int _instructorID, int _exersiseID, int _traineeID, bool _add) {
            return base.Channel.SetTraineeAssignedStatus(_instructorID, _exersiseID, _traineeID, _add);
        }
        
        public System.Threading.Tasks.Task<bool> SetTraineeAssignedStatusAsync(int _instructorID, int _exersiseID, int _traineeID, bool _add) {
            return base.Channel.SetTraineeAssignedStatusAsync(_instructorID, _exersiseID, _traineeID, _add);
        }
        
        public bool SetNoiseLevelChanged(int _radioId, bool _changed) {
            return base.Channel.SetNoiseLevelChanged(_radioId, _changed);
        }
        
        public System.Threading.Tasks.Task<bool> SetNoiseLevelChangedAsync(int _radioId, bool _changed) {
            return base.Channel.SetNoiseLevelChangedAsync(_radioId, _changed);
        }
        
        public bool SetNoiseLevel(int _radioID, int _noiselevel) {
            return base.Channel.SetNoiseLevel(_radioID, _noiselevel);
        }
        
        public System.Threading.Tasks.Task<bool> SetNoiseLevelAsync(int _radioID, int _noiselevel) {
            return base.Channel.SetNoiseLevelAsync(_radioID, _noiselevel);
        }
        
        public bool GetNoiseLevelChanged() {
            return base.Channel.GetNoiseLevelChanged();
        }
        
        public System.Threading.Tasks.Task<bool> GetNoiseLevelChangedAsync() {
            return base.Channel.GetNoiseLevelChangedAsync();
        }
        
        public int GetNoiseLevel(int _radioID) {
            return base.Channel.GetNoiseLevel(_radioID);
        }
        
        public System.Threading.Tasks.Task<int> GetNoiseLevelAsync(int _radioID) {
            return base.Channel.GetNoiseLevelAsync(_radioID);
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
        
        public bool RegisterTrainee(UNET_Classes.CurrentInfo _currentInfo) {
            return base.Channel.RegisterTrainee(_currentInfo);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterTraineeAsync(UNET_Classes.CurrentInfo _currentInfo) {
            return base.Channel.RegisterTraineeAsync(_currentInfo);
        }
        
        public bool SetExerciseCount(int _count) {
            return base.Channel.SetExerciseCount(_count);
        }
        
        public System.Threading.Tasks.Task<bool> SetExerciseCountAsync(int _count) {
            return base.Channel.SetExerciseCountAsync(_count);
        }
        
        public bool SetExerciseSelected(int _instructor, int _exerciseIndex, bool _select) {
            return base.Channel.SetExerciseSelected(_instructor, _exerciseIndex, _select);
        }
        
        public System.Threading.Tasks.Task<bool> SetExerciseSelectedAsync(int _instructor, int _exerciseIndex, bool _select) {
            return base.Channel.SetExerciseSelectedAsync(_instructor, _exerciseIndex, _select);
        }
        
        public bool SetExercises(UNET_Classes.Exercise[] _exercises) {
            return base.Channel.SetExercises(_exercises);
        }
        
        public System.Threading.Tasks.Task<bool> SetExercisesAsync(UNET_Classes.Exercise[] _exercises) {
            return base.Channel.SetExercisesAsync(_exercises);
        }
        
        public bool SetRoles(UNET_Classes.Role[] _role) {
            return base.Channel.SetRoles(_role);
        }
        
        public System.Threading.Tasks.Task<bool> SetRolesAsync(UNET_Classes.Role[] _role) {
            return base.Channel.SetRolesAsync(_role);
        }
        
        public bool SetRolesCount(int _count) {
            return base.Channel.SetRolesCount(_count);
        }
        
        public System.Threading.Tasks.Task<bool> SetRolesCountAsync(int _count) {
            return base.Channel.SetRolesCountAsync(_count);
        }
        
        public bool SetRadios(UNET_Classes.Radio[] _radio) {
            return base.Channel.SetRadios(_radio);
        }
        
        public System.Threading.Tasks.Task<bool> SetRadiosAsync(UNET_Classes.Radio[] _radio) {
            return base.Channel.SetRadiosAsync(_radio);
        }
        
        public bool SetRadioStatus(int _radioNumber, UNET_Classes.UNETRadioState _state) {
            return base.Channel.SetRadioStatus(_radioNumber, _state);
        }
        
        public System.Threading.Tasks.Task<bool> SetRadioStatusAsync(int _radioNumber, UNET_Classes.UNETRadioState _state) {
            return base.Channel.SetRadioStatusAsync(_radioNumber, _state);
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
        
        public bool SetInstructors(UNET_Classes.Instructor[] _instructor) {
            return base.Channel.SetInstructors(_instructor);
        }
        
        public System.Threading.Tasks.Task<bool> SetInstructorsAsync(UNET_Classes.Instructor[] _instructor) {
            return base.Channel.SetInstructorsAsync(_instructor);
        }
        
        public bool SetTrainees(UNET_Classes.Trainee[] _trainee) {
            return base.Channel.SetTrainees(_trainee);
        }
        
        public System.Threading.Tasks.Task<bool> SetTraineesAsync(UNET_Classes.Trainee[] _trainee) {
            return base.Channel.SetTraineesAsync(_trainee);
        }
        
        public bool SetPlatforms(UNET_Classes.Platform[] _platform) {
            return base.Channel.SetPlatforms(_platform);
        }
        
        public System.Threading.Tasks.Task<bool> SetPlatformsAsync(UNET_Classes.Platform[] _platform) {
            return base.Channel.SetPlatformsAsync(_platform);
        }
    }
}