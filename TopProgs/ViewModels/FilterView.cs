using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopProgs.Models;

namespace TopProgs.ViewModels
{
    public class FilterView
    {
        public SelectedIDsAndItems DaysOfWeek { get; set; }

        public TPFilter Filter { get; set; }
    }
}