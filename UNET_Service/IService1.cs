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
