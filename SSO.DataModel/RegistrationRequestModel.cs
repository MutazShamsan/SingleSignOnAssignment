using System.Runtime.Serialization;

namespace SSO.DataModel
{
    [DataContract]
    public class RegistrationRequestModel : ServiceRequestModel
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public int UserLevel { get; set; }
    }
}
