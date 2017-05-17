using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    [DataContract()]
    [Serializable()]
    public partial class AvailableDate
    {
        /// <summary>
        /// First date of available data
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Last date of available data
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public DateTime EndDate { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public class Date
    {
        [DataMember()]
        public DateTime MinDate { get; set; }

        [DataMember()]
        public DateTime MaxDate { get; set; }

        [DataMember()]
        public List<DateTime> Dates { get; set; }
    }
}