using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopProgs.Models
{
    /// <summary>
    /// Channel information
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Channel name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Channel abbreviation
        /// </summary>
        public string Abbrv { get; set; }

        /// <summary>
        /// Channel ID
        /// </summary>
        public int ID { get; set; }
    }
}