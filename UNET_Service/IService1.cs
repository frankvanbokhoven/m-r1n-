﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IService1.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IService1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace UNET_Service
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using UNET_Classes;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        #region SIM methods
        [OperationContract]
        bool DisconnectVCS();

        [OperationContract]
        bool Reset();

        [OperationContract]
        bool Login();

        [OperationContract]
        bool Start();

        [OperationContract]
        bool Stop();

        [OperationContract]
        bool KeepAlive(string _id);

        #endregion
        //start service
        //  [OperationContract]
        //  bool StartService();

        #region PTT
        [OperationContract]
        bool AddPTT(string _traineeInstructorID, PTTuser _pttUser);

        [OperationContract]
        bool AcknowledgePTT(string _traineeInstructorID);

        /// <summary>
        /// speciaal tbv UNET_ServiceStatus
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PTTcaller> GetPTTQueue();
        #endregion

        #region Pointtopoint
        [OperationContract]
        List<UNET_Classes.PointToPoint> GetP2P(string _instructorID);

        [OperationContract]
        bool AcknowledgeP2P(string _traineeInstructorID);

        [OperationContract]
        bool RequestPointToPoint(string _traineeInstructorID);
        #endregion

        #region Assist
        [OperationContract]
        bool CreateAssist(string _traineeId, string _traineeInfo);

        [OperationContract]
        bool AcknowledgeAssist(string _instructorID, string _traineeID);

        [OperationContract]
        List<UNET_Classes.Assist> GetAssists(string _instructorID);
        #endregion

        #region add
        [OperationContract]
        bool AddRole(Role _Role);

        [OperationContract]
        bool AddTrainee(Trainee _trainee);

        [OperationContract]
        bool AddInstructor(Instructor _instructor);

        [OperationContract]
        bool AddPlatform(Platform _platform);

        [OperationContract]
        bool AddRadioObject(Radio _radio);

        [OperationContract]
        bool AddExercise(Exercise _exercise);

        [OperationContract]
        bool AddRadio(string _radioname);

        [OperationContract]
        bool AddRadioToExercise(int _exerciseID, Radio _radio);

        [OperationContract]
        bool AddPlatformToExercise(int _exerciseID, string _platformName);

        [OperationContract]
        bool AddRoleToExercise(int _exerciseID, Platform _platform, string _traineeName);

        [OperationContract]
        bool AddTraineeToExercise(int _exerciseID, Platform _platform, string _traineeName);

        [OperationContract]
        bool AddTraineeToInstructor(string _traineeID, string _instructor);

        [OperationContract]
        bool AddInstructorRadio(int _exerciseID, Instructor _instructor, Radio _radio);

        #endregion


        #region Getters
        [OperationContract]
        Instructor GetAllInstructorData(string _instructorID);


        [OperationContract]
        List<UNET_Classes.Exercise> GetExercises();

        [OperationContract]
        List<UNET_Classes.Role> GetRoles();

        [OperationContract]
        List<UNET_Classes.Radio > GetRadios();
    
        [OperationContract]
        List<UNET_Classes.Instructor > GetInstructors();

        //[OperationContract]
        //bool SetInstructorsCount(int _count);


        [OperationContract]
        List<UNET_Classes.Trainee> GetTrainees();

        [OperationContract]
        List<UNET_Classes.Trainee> GetTraineesAssigned(string _instructorID, int _exerciseNumber);


        [OperationContract]
        List<UNET_Classes.Platform> GetPlatforms();

        [OperationContract]
        UNET_Classes.CurrentInfo GetExerciseInfo(string _traineeID);

        [OperationContract]
        bool[] GetTraineeStatus();

        [OperationContract]
        bool GetTraineeStatusChanged();

        [OperationContract]
        List<UNET_Classes.Role> GetTraineeRoles(string _traineeID);


        [OperationContract]
        bool GetNoiseLevelChanged();

        [OperationContract]
        int GetNoiseLevel(int _radioID);

        #endregion

        #region PendingChanges
        [OperationContract]
        DateTime GetPendingChanges();

        #endregion


        #region statusmessages
        //   [OperationContract]
        //   string GetSIPStatusMessage(string _id);

        //   [OperationContract]
        //   bool SetSIPStatusMessage(string _message, string _id);

        //  [OperationContract]
        //  bool ClearStatusMessages(string _id);
        #endregion

        #region Setters
        [OperationContract]
        bool RegisterClient(string _clientID, string _displayName, bool _isTrainee);

        [OperationContract]
        bool UnRegisterClient(string _clientID, bool _isTrainee);

        [OperationContract]
        void NotifyServer(EventDataType eventData); 

        //[OperationContract]
        //bool SetExerciseCount(int _count);

        [OperationContract]
        bool SetExerciseSelected(string _instructor, int _exerciseIndex, bool _select);

        [OperationContract]
        bool SetExercises(List<UNET_Classes.Exercise> _exercises);
        
        [OperationContract]
        bool SetRoles(List<UNET_Classes.Role> _role);

        //[OperationContract]
        //bool SetRolesCount(int _count);

        [OperationContract]
        bool SetRadios(List<UNET_Classes.Radio> _radio);

        [OperationContract]
        bool SetRadioStatus(int _radioNumber, UNET_Classes.UNETRadioState _state);

        [OperationContract]
        bool SetRadiosCount(int _count);

        //[OperationContract]
        //bool SetTraineesCount(int _count);

        [OperationContract]
        bool SetInstructors(List<UNET_Classes.Instructor> _instructor);

        [OperationContract]
        bool SetTrainees(List<UNET_Classes.Trainee> _trainee);

        [OperationContract]
        bool SetPlatforms(List<UNET_Classes.Platform> _platform);

        [OperationContract]
        bool SetTraineeStatusChanged(string _traineeId, bool _changed);

        [OperationContract]
        bool SetRoleAssignedStatus(string _instructorID, int _exersiseID, int _role, bool _add);

        [OperationContract]
        bool SetTraineeAssignedStatus(string _instructorID, int _exersiseID, string _traineeID, bool _add);

        [OperationContract]
        bool SetRadioAssignedStatus(string _instructorID, int _exersiseID, int _radio, bool _add);


        [OperationContract]
        bool SetNoiseLevelChanged(int _radioId, bool _changed);

        [OperationContract]
        bool SetNoiseLevel(int _radioID, int _noiselevel);

        #endregion
    }

}
