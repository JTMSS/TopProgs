using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    [DataContract()]
    [Serializable()]
    public partial class EventInfo
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }

        [DataMember()]
        public Nullable<DateTime> Date_Start { get; set; }

        [DataMember()]
        public Nullable<DateTime> Date_End { get; set; }

        [DataMember()]
        public virtual Channel Channel { get; set; }

        [DataMember()]
        public virtual Event_Type Event_Type { get; set; }

        [DataMember()]
        public virtual Region Region { get; set; }

        [DataMember()]
        public virtual Programme Programme { get; set; }

        [DataMember()]
        public virtual Spot Spot { get; set; }

        [DataMember()]
        public virtual ExternalEvent ExternalEvent { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class Event_Type
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class Programme
    {
        [DataMember()]
        public string Episode { get; set; }

        [DataMember()]
        public bool Has_Sponsorship { get; set; }

        [DataMember()]
        public bool Repeat { get; set; }

        [DataMember()]
        public bool Live { get; set; }

        [DataMember()]
        public bool Tele_shopping { get; set; }

        [DataMember()]
        public bool Product_Placement { get; set; }

        [DataMember()]
        public Nullable<int> Part { get; set; }

        [DataMember()]
        public virtual Genre Genre { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class Genre
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class Spot
    {
        [DataMember()]
        public bool Break_Going_In { get; set; }

        [DataMember()]
        public bool Break_Centre { get; set; }

        [DataMember()]
        public bool Break_End { get; set; }

        [DataMember()]
        public bool First_min_of_break { get; set; }

        [DataMember()]
        public bool Last_min_of_break { get; set; }

        [DataMember()]
        public bool Centre_min_of_break { get; set; }

        [DataMember()]
        public bool Targeted_ad { get; set; }

        [DataMember()]
        public bool Tele_shopping_spot { get; set; }

        [DataMember()]
        public virtual Brand Brand { get; set; }

        [DataMember()]
        public virtual Sales_House Sales_House { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class Brand
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }

        [DataMember()]
        public Nullable<DateTime> Valid_From_Date { get; set; }

        [DataMember()]
        public Nullable<DateTime> Valid_To_Date { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class Sales_House
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }

        [DataMember()]
        public string Abbreviation { get; set; }

        [DataMember()]
        public Nullable<DateTime> Valid_From_Date { get; set; }

        [DataMember()]
        public Nullable<DateTime> Valid_To_Date { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class ExternalEvent
    {
        [DataMember()]
        public Nullable<DateTime> MapStartDate { get; set; }

        [DataMember()]
        public virtual CalcType CalcType { get; set; }
    }

    [DataContract()]
    [Serializable()]
    public partial class CalcType
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }
    }


    [DataContract()]
    [Serializable()]
    public partial class BLEventFilters
    {
        /// <summary>
        /// List of channels IDs
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public List<int> ChannelIDs { get; set; }

        /// <summary>
        /// List of region IDs (defaults to 1 if not present)
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public List<int> RegionIDs { get; set; }

        /// <summary>
        /// List of event types
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public List<int> EventTypeIDs { get; set; }

        /// <summary>
        /// First date event can be on
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public Nullable<DateTime> DateFrom { get; set; }

        /// <summary>
        /// Last date event can be on
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public Nullable<DateTime> DateTo { get; set; }

        /// <summary>
        /// List of the days of the week (defaults to all days if not present)
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public List<string> DaysOfWeek { get; set; }

        /// <summary>
        /// Time of day from which event can be on (defaults to 00:00:00 if not present)
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public int TimeFrom { get; set; }

        /// <summary>
        /// Time of day to which event can be on (defaults to 23:59:59 if not present)
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public int TimeTo { get; set; }

        /// <summary>
        /// True if event must be wholly within TimeFrom and TimeTo
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public bool InclusiveTimes { get; set; }

        /// <summary>
        /// Text that must be in the event's title
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public string SearchTitle { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        /// <remarks></remarks>
        [DataMember()]
        public int ID { get; set; }

        public BLEventFilters()
        {
            RegionIDs = new List<int>();
            ChannelIDs = new List<int>();
            EventTypeIDs = new List<int>();
            DaysOfWeek = new List<string>();
        }
    }
}