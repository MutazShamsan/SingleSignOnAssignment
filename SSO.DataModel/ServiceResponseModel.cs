using System.Runtime.Serialization;

namespace SSO.DataModel
{
    [DataContract]
    public abstract class ServiceResponseModel
    {
        [DataMember]
        public string ErrorMessage { get; set; }


        [DataMember]
        public bool Success { get; set; }
    }
}
