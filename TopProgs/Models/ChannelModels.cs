using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    /// <summary>
    /// Channel information
    /// </summary>
    [DataContract()]
    [Serializable()]
    public partial class Channel
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }

        [DataMember()]
        public string Abbreviation { get; set; }
    }
}