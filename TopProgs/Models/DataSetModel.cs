using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    [DataContract()]
    [Serializable()]
    public class DataSetInfo
    {
        /// <summary>
        /// Name
        /// </summary>
        [DataMember()]
        public string Name { get; set; }

        /// <summary>
        /// Data provider
        /// </summary>
        [DataMember()]
        public string Provider { get; set; }

        /// <summary>
        /// Start time for viewing (HHMMSS)
        /// </summary>
        [DataMember()]
        public int ViewingStart { get; set; }

        /// <summary>
        /// Start time for events (HHMMSS)
        /// </summary>
        [DataMember()]
        public int EventStart { get; set; }

        /// <summary>
        /// First day of the week (e.g. Sun)
        /// </summary>
        [DataMember()]
        public string FirstDayOfWk { get; set; }

        /// <summary>
        /// Abbreviated names for days
        /// </summary>
        [DataMember()]
        public string[] DayNameAbbrev { get; set; }

        /// <summary>
        /// Names for days
        /// </summary>
        [DataMember()]
        public string[] DayName { get; set; }
    }
}