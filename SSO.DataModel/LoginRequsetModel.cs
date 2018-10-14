using System.Runtime.Serialization;

namespace SSO.DataModel
{
    [DataContract]
    public class LoginRequsetModel : ServiceRequestModel
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string AccessToken { get; set; }

    }
}
