// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUNETService.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IUNETService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace UNET_Server
{
    public interface IUNETService
    {
        //Getters
        [OperationContract]
        List<UNET_Server.Classes.Exercise> GetExercises();

        //for testing purposes
        [OperationContract]
        List<string> GetRoles();

        //for testing purposes
        [OperationContract]
        List<string> GetRadios();

        //for testing purposes
        [OperationContract]
        List<string> GetInstructors();

        //for testing purposes
        [OperationContract]
        List<string> GetTrainees();

        //for testing purposes
        [OperationContract]
        List<string> GetPlatforms();


        //Setters
        [OperationContract]
        bool SetExerciseCount(int _count);

        [OperationContract]
        bool SetExercises(List<Classes.Exercise> _exercises);

        //for testing purposes
        [OperationContract]
        bool SetRoles(string _role);

        //for testing purposes
        [OperationContract]
        bool SetRadios(string _radio);

        //for testing purposes
        [OperationContract]
        bool SetInstructors(string _instructor);

        //for testing purposes
        [OperationContract]
        bool SetTrainees(string _trainee);

        //for testing purposes
        [OperationContract]
        bool SetPlatforms(string _platform);
    }
}
