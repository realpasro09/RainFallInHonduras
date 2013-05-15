using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rainfall.Web.Models
{
    public class TableParameter
    {
        public string sEcho { get; set; }
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
    }
}