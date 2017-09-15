// --------------------------------------------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using UNET_Classes;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        //Getters
        [OperationContract]
        List<UNET_Classes.Exercise> GetExercises();

        [OperationContract]
        List<UNET_Classes.Role> GetRoles();

        [OperationContract]
        List<UNET_Classes.Radio > GetRadios();
    
        [OperationContract]
        List<UNET_Classes.Instructor > GetInstructors();

        [OperationContract]
        bool SetInstructorsCount(int _count);


        [OperationContract]
        List<UNET_Classes.Trainee> GetTrainees();
       
        [OperationContract]
        List<UNET_Classes.Platform> GetPlatforms();

        [OperationContract]
        UNET_Classes.CurrentInfo GetExerciseInfo(int _traineeID);

        [OperationContract]
        bool[] GetTraineeStatus();

        [OperationContract]
        bool GetTraineeStatusChanged();

        [OperationContract]
        bool SetTraineeStatusChanged(int _traineeId, bool _changed);

        [OperationContract]
        bool SetTraineeAssignedStatus(int _instructorID, int _exersiseID, int _traineeID, bool _add);


        [OperationContract]
        bool SetNoiseLevelChanged(int _radioId, bool _changed);

        [OperationContract]
        bool SetNoiseLevel(int _radioID, int _noiselevel);

        [OperationContract]
        bool GetNoiseLevelChanged();

        [OperationContract]
        int GetNoiseLevel(int _radioID);

        #region statusmessages
        [OperationContract]
        string GetSIPStatusMessage(string _id);

        [OperationContract]
        bool SetSIPStatusMessage(string _message, string _id);

        [OperationContract]
        bool ClearStatusMessages(string _id);
        #endregion

        //Setters
        [OperationContract]
        bool RegisterTrainee(UNET_Classes.CurrentInfo _currentInfo);

        [OperationContract]
        bool SetExerciseCount(int _count);

        [OperationContract]
        bool SetExerciseSelected(int _instructor, int _exerciseIndex, bool _select);

        [OperationContract]
        bool SetExercises(List<UNET_Classes.Exercise> _exercises);
        
        [OperationContract]
        bool SetRoles(List<UNET_Classes.Role> _role);

        [OperationContract]
        bool SetRolesCount(int _count);

        [OperationContract]
        bool SetRadios(List<UNET_Classes.Radio> _radio);

        [OperationContract]
        bool SetRadioStatus(int _radioNumber, UNET_Classes.UNETRadioState _state);

        [OperationContract]
        bool SetRadiosCount(int _count);

        [OperationContract]
        bool SetTraineesCount(int _count);

        [OperationContract]
        bool SetInstructors(List<UNET_Classes.Instructor> _instructor);

        [OperationContract]
        bool SetTrainees(List<UNET_Classes.Trainee> _trainee);

        [OperationContract]
        bool SetPlatforms(List<UNET_Classes.Platform> _platform);
    }

 }
