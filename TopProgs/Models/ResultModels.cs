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
        public List<string> Headers { get; set; }

        public List<string> TargetNames { get; set; }

        public List<string> MetricNames { get; set; }

        public List<ResultItem> Items { get; set; }

        public Result()
        {
            Headers = new List<string>();
            TargetNames = new List<string>();
            MetricNames = new List<string>();
            Items = new List<Models.ResultItem>();
        }
    }

    public class ResultItem
    {
        public string Title { get; set; }

        public string Chan { get; set; }

        public DateTime TheDate { get; set; }

        public int Time { get; set; }

        public List<List<double>> Figures { get;set;}

        public ResultItem()
        {
            Figures = new List<List<double>>();
        }
    }
}