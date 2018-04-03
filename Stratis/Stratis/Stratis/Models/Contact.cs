using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Stratis.Models
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
    }
}
