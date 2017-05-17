using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    [DataContract()]
    [Serializable()]
    public partial class Target
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }

        [DataMember()]
        public bool Is_Home { get; set; }

        [DataMember()]
        public virtual ICollection<Target_DemoKey> Target_DemoKey { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class Target_DemoKey
    {
        [DataMember()]
        public string DemoKey { get; set; }
    }
}