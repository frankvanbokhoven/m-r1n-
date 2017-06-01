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

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]

    public interface IService1
    {
        //Getters
        [OperationContract]
        List<UNET_Service.Classes.Exercise> GetExercises();

        [OperationContract]
        List<UNET_Service.Classes.Role> GetRoles();

        [OperationContract]
        List<UNET_Service.Classes.Radio > GetRadios();
    
        [OperationContract]
        List<UNET_Service.Classes.Instructor > GetInstructors();
 
        [OperationContract]
        List<UNET_Service.Classes.Trainee> GetTrainees();
       
        [OperationContract]
        List<UNET_Service.Classes.Platform> GetPlatforms();

        [OperationContract]
        Classes.CurrentInfo GetExerciseInfo(int _traineeID);

        [OperationContract]
        bool[] GetTraineeStatus();

        [OperationContract]
        bool GetTraineeStatusChanged();

        [OperationContract]
        bool SetTraineeStatusChanged(int _traineeId, bool _changed);

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
        bool RegisterTrainee(Classes.CurrentInfo _currentInfo);

        [OperationContract]
        bool SetExerciseCount(int _count);

        [OperationContract]
        bool SetExercises(List<Classes.Exercise> _exercises);
        
        [OperationContract]
        bool SetRoles(List<Classes.Role> _role);

        [OperationContract]
        bool SetRolesCount(int _count);

        [OperationContract]
        bool SetRadios(List<Classes.Radio> _radio);

        [OperationContract]
        bool SetRadiosCount(int _count);

        [OperationContract]
        bool SetTraineesCount(int _count);

        [OperationContract]
        bool SetInstructors(List<Classes.Instructor> _instructor);

        [OperationContract]
        bool SetTrainees(List<Classes.Trainee> _trainee);

        [OperationContract]
        bool SetPlatforms(List<Classes.Platform> _platform);
    }

 }
