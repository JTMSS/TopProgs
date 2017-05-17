using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    [DataContract()]
    [Serializable()]
    public partial class InventoryCall
    {
        /// <summary>
        /// Inventory defintion
        /// </summary>
        [DataMember()]
        public BLInventoryInfo InvInfo { get; set; }

        /// <summary>
        /// Filters used for event IDs in definition
        /// </summary>
        [DataMember()]
        public BLEventFilters Filter { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class BLInventoryInfo
    {
        /// <summary>
        /// List of event IDs
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public List<int> EventIDs { get; set; }

        /// <summary>
        /// List of region IDs
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public List<int> RegIDs { get; set; }

        /// <summary>
        /// Date on which demographics taken
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public DateTime DemoDate { get; set; }

        /// <summary>
        /// Date on which weight taken
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public DateTime WgtDate { get; set; }

        /// <summary>
        /// List of demographics in target
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public List<string> DemoKeys { get; set; }

        /// <summary>
        /// True if target is based on homes, false if individuals
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public bool IsHome { get; set; }

        /// <summary>
        /// True if include homes and individuals in inventory, false if include just one based on IsHome
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public bool IncludeIndsAndHomes { get; set; }

        /// <summary>
        /// Inventory key
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public string Key { get; set; }

        /// <summary>
        /// Inventory name
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public string Name { get; set; }

        /// <summary>
        /// Name of the target defined by DemoKeys etc
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public string TargetName { get; set; }

        public BLInventoryInfo()
        {
            EventIDs = new List<int>();
            RegIDs = new List<int>();
            DemoKeys = new List<string>();
        }
    }

    /// <summary>
    /// Call to Inventory PostEventMetrics
    /// </summary>
    /// <remarks></remarks>
    [DataContract()]
    [Serializable()]
    public partial class InvEventMetricCall
    {
        /// <summary>
        /// Inventory Key
        /// </summary>
        /// <value>Inventory Key</value>
        /// <returns>Inventory Key</returns>
        /// <remarks></remarks>
        [DataMember()]
        public string InvKey { get; set; }

        /// <summary>
        /// List of metrics wanted
        /// </summary>
        /// <value>List of metrics wanted</value>
        /// <returns>List of metrics wanted</returns>
        /// <remarks></remarks>
        [DataMember()]
        public List<BLMETRICTYPE> Metrics { get; set; }

        public InvEventMetricCall()
        {
            Metrics = new List<BLMETRICTYPE>();
        }
    }
}