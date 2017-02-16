// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IService1.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IService1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.IO;

namespace TestMTOM
{
    using System.Runtime.Serialization;
    using System.ServiceModel;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    // [ServiceContract]
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]

    public interface IService1
    {
       
        //look at: https://social.msdn.microsoft.com/Forums/vstudio/en-US/e647042b-a3c9-4d93-a6f5-6ca583b5babc/send-stream-type-as-a-datamember-in-the-datacontract?forum=wcf
        [OperationContract]
        Stream GetLargeObject(string _filename);


        [OperationContract]        
        List<string> GetAvailableFiles();

        [OperationContract]
        List<string> GetTestRoles();

        [OperationContract]
        void SaveLargeObject(SaveFileInfo _request);
    }

    [DataContract]
    public class Result
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public byte[] FileContent { get; set; }
    };




}
