using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Estimate_me_autotest.Json
{
    [DataContract]
    public class User
    {
        [DataMember]
        private String token { get; set; }
        [DataMember]
        private Int64 expire { get; set; }
        [DataMember]
        private String user { get; set; }
        [DataMember]
        private Role role { get; set; }

    }
}

