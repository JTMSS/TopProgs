using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    [DataContract]
    [Serializable]
    public class TPFilter
    {
        [DataMember]
        public string[] DaysOfWk { get; set; }

        [Display(Name = "Time (from)")]
        [DataType(DataType.Time)]
        [DataMember]
        public int StartTime { get; set; }

        [Display(Name = "Time (to)")]
        [DataType(DataType.Time)]
        [DataMember]
        public int EndTime { get; set; }

        [Display(Name = "Times Inclusive")]
        [DataMember]
        public bool TimeInclusive { get; set; }

        [Display(Name = "Min Duration")]
        [DataMember]
        public int MinDuration { get; set; }

        [Display(Name = "Search Title")]
        [DataType(DataType.Text)]
        [DataMember]
        public string SearchTitle { get; set; }

        public TPFilter() { }

        public TPFilter(string[] arrDayAbbrev)
        {
            StartTime = 0;
            EndTime = 2359;
            MinDuration = 5;
            TimeInclusive = false;
            SearchTitle = "";

            if (arrDayAbbrev != null)
            {
                DaysOfWk = new string[7];

                for (int i = 0; i < 7; i++)
                {
                    DaysOfWk[i] = arrDayAbbrev[i];
                }
            }
        }

        internal bool Validate(ref string rstrErr)
        {
            return true;
        }
    }
}