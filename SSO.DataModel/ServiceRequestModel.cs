using System;
using System.Runtime.Serialization;

namespace SSO.DataModel
{
    [DataContract]
    public abstract class ServiceRequestModel
    {
        [DataMember] public DateTimeOffset RequestDateTime { get; set; }

        [DataMember] public string RequestIpAddress { get; set; }
    }
}
