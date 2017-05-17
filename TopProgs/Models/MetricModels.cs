using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    /// <summary>
    /// Event ID and metric results
    /// </summary>
    [DataContract()]
    [Serializable()]
    public class BLEventIDResult
    {
        /// <summary>
        /// Event ID
        /// </summary>
        [DataMember()]
        public int EventID { get; set; }

        /// <summary>
        /// List of results for a set of metrics
        /// </summary>
        [DataMember()]
        public List<BLMetricResult> Results { get; set; }
    }

    /// <summary>
    /// Result for a specific metric type
    /// </summary>
    [DataContract()]
    [Serializable()]
    public class BLMetricResult
    {
        /// <summary>
        /// Metric type
        /// </summary>
        [DataMember()]
        public BLMETRICTYPE Type { get; set; }

        /// <summary>
        /// Metric result
        /// </summary>
        [DataMember()]
        public double Result { get; set; }
    }

    /// <summary>
    /// Metric types for calculations
    /// </summary>
    [DataContract()]
    [Serializable()]
    public enum BLMETRICTYPE : int
    {
        /// <summary>
        /// Rating in thousands
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        Thousands = 1,

        /// <summary>
        /// Rating percent
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        GRP,

        /// <summary>
        /// Rating in thousands with 15 second threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        Thousands_15Sec,

        /// <summary>
        /// Rating percent with 15 second threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        GRP_15Sec,

        /// <summary>
        /// Rating in thousands with 3 minute threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        Thousands_3Min,

        /// <summary>
        /// Rating percent with 3 minute threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        GRP_3Min,

        /// <summary>
        /// Reach in thousands
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        ReachThousands,

        /// <summary>
        /// Reach as percent
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        ReachPercent,

        /// <summary>
        /// Reach in thousands with 15 second threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        ReachThousands_15Sec,

        /// <summary>
        /// Reach as percent with 15 second threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        ReachPercent_15Sec,

        /// <summary>
        /// Reach in thousands with 1 minute threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        ReachThousands_1Min,

        /// <summary>
        /// Reach as percent with 1 minute threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        ReachPercent_1Min,

        /// <summary>
        /// Reach in thousands with 3 minute threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        ReachThousands_3Min,

        /// <summary>
        /// Reach as percent with 3 minute threshold
        /// </summary>
        /// <remarks></remarks>
        [EnumMember()]
        ReachPercent_3Min
    }
}