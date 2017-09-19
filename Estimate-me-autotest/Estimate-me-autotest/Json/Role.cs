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
    public class Role
    {
        [DataMember]
        private String _id { get; set; }
        [DataMember]
        private String name { get; set; }
        [DataMember]
        private String user { get; set; }
        [DataMember]
        private bool canExportMoney { get; set; }

    }
}

