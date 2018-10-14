using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SSO.DataModel
{
    [DataContract]
    public class AppUser : ICloneable
    {
        [Column]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int Id { get; set; }

        [Column]
        [DataMember]
        public string Username { get; set; }

        [Column]
        [DataMember]
        public string FullName { get; set; }

        [Column]
        [DataMember]
        public string PasswordSalt { get; set; }

        [Column]
        [DataMember]
        public string HashedPassword { get; set; }

        [DataMember]
        public string Password { get; set; }

        [Column]
        [DataMember]
        public int UserLevel { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
