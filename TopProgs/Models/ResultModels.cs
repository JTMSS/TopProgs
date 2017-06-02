using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TopProgs.Models
{
    [DataContract()]
    [Serializable()]
    public class Result
    {
        [DataMember()]
        public string Region { get; set; }

        [DataMember()]
        public List<string> Headers { get; set; }

        [DataMember()]
        public List<string> TargetNames { get; set; }

        [DataMember()]
        public List<string> MetricNames { get; set; }

        [DataMember()]
        public List<ResultItem> Items { get; set; }

        public Result()
        {
            Headers = new List<string>();
            TargetNames = new List<string>();
            MetricNames = new List<string>();
            Items = new List<Models.ResultItem>();
        }
    }

    [DataContract()]
    [Serializable()]
    public class ResultItem
    {
        [DataMember()]
        public string Title { get; set; }

        [DataMember()]
        public string Chan { get; set; }

        [DataMember()]
        public DateTime TheDate { get; set; }

        [DataMember()]
        public int Time { get; set; }

        [DataMember()]
        public List<List<double>> Figures { get;set;}

        public ResultItem()
        {
            Figures = new List<List<double>>();
        }
    }
}