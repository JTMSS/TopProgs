using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    /// <summary>
    /// Parameters for TopProgs report
    /// </summary>
    [DataContract()]
    [Serializable()]
    public partial class TopProgsReportCall
    {
        /// <summary>
        /// List of channel IDs
        /// </summary>
        [DataMember()]
        public List<int> ChannelIDs { get; set; }

        /// <summary>
        /// List of region IDs
        /// </summary>
        [DataMember()]
        public List<int> RegionIDs { get; set; }

        /// <summary>
        /// List of event type IDs
        /// </summary>
        [DataMember()]
        public List<int> EventTypeIDs { get; set; }

        /// <summary>
        /// List of dates
        /// </summary>
        [DataMember()]
        public List<DateTime> Dates { get; set; }

        /// <summary>
        /// List of days of the week to filter
        /// </summary>
        [DataMember()]
        public List<string> DaysOfWeek { get; set; }

        /// <summary>
        /// Start time to filter
        /// </summary>
        [DataMember()]
        public int TimeFrom { get; set; }

        /// <summary>
        /// End time to filter
        /// </summary>
        [DataMember()]
        public int TimeTo { get; set; }

        /// <summary>
        /// True if event must be totally within start and end time filters
        /// </summary>
        [DataMember()]
        public bool InclusiveTimes { get; set; }

        /// <summary>
        /// Text that must be in the event title
        /// </summary>
        [DataMember()]
        public string SearchTitle { get; set; }

        /// <summary>
        /// List of target IDs
        /// </summary>
        [DataMember()]
        public List<int> TargetIDs { get; set; }

        /// <summary>
        /// List of metric IDs
        /// </summary>
        [DataMember()]
        public List<BLMETRICTYPE> Metrics { get; set; }

        public TopProgsReportCall ()
        {
            ChannelIDs = new List<int>();
            RegionIDs = new List<int>();
            EventTypeIDs = new List<int>();
            Dates = new List<DateTime>();
            DaysOfWeek = new List<string>();
            TargetIDs = new List<int>();
            Metrics = new List<Models.BLMETRICTYPE>();
        }
    }

    /// <summary>
    /// Event and metric results for multiple targets
    /// </summary>
    [DataContract()]
    [Serializable()]
    public partial class BLEventMetricResult
    {
        /// <summary>
        /// Event information
        /// </summary>
        [DataMember()]
        public EventInfo Evnt { get; set; }

        /// <summary>
        /// List of results for a set of metrics
        /// </summary>
        [DataMember()]
        public List<List<BLMetricResult>> Results { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public class BLTopProgsReport
    {
        [DataMember()]
        public List<string> Channels { get; set; }

        [DataMember()]
        public List<string> Regions { get; set; }

        [DataMember()]
        public List<string> Targets { get; set; }

        [DataMember()]
        public List<string> Dates { get; set; }

        [DataMember()]
        public List<int> ChannelIDs { get; set; }

        [DataMember()]
        public List<int> RegionIDs { get; set; }

        [DataMember()]
        public List<int> EventTypeIDs { get; set; }

        [DataMember()]
        public List<string> DaysOfWeek { get; set; }

        [DataMember()]
        public int TimeFrom { get; set; }

        [DataMember()]
        public int TimeTo { get; set; }

        [DataMember()]
        public bool InclusiveTimes { get; set; }

        [DataMember()]
        public string SearchTitle { get; set; }

        [DataMember()]
        public List<int> TargetIDs { get; set; }

        [DataMember()]
        public List<BLMETRICTYPE> Metrics { get; set; }

        [DataMember()]
        public List<BLEventMetricResult> EventResults { get; set; }

        public BLTopProgsReport()
        {
            Channels = new List<string>();
            Regions = new List<string>();
            Targets = new List<string>();
            Dates = new List<string>();
            ChannelIDs = new List<int>();
            RegionIDs = new List<int>();
            EventTypeIDs = new List<int>();
            DaysOfWeek = new List<string>();
            TargetIDs = new List<int>();
            Metrics = new List<BLMETRICTYPE>();
        }
    }
}