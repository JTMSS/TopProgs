using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TopProgs.ViewModels
{
    public class SelectedIDsAndItems
    {
        public string[] SelectedItemIds { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }
    }
}