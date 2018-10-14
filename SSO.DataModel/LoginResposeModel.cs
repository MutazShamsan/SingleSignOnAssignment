using System;
using System.Runtime.Serialization;

namespace SSO.DataModel
{
    [DataContract]
    public class LoginResposeModel : ServiceResponseModel
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public bool IsAuthorized { get; set; }

        [DataMember]
        public string AccessToken { get; set; }

        [DataMember]
        public int UserLevel { get; set; }

        [DataMember]
        public DateTimeOffset FirstActiveTime { get; set; }

        [DataMember]
        public DateTimeOffset LastActiveTime { get; set; }
    }
}
