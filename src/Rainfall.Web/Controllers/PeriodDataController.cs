using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Rainfall.Domain.Services;
using Rainfall.Web.Models;

namespace Rainfall.Web.Controllers
{
    public class PeriodDataController : Controller
    {
        //
        // GET: /Period/

        public JsonResult Get()
        {
            var periodFilterModels = (
                from PeriodType period in Enum.GetValues(typeof (PeriodType)) 
                let periodDescription = Regex.Replace(period.ToString(), "([a-z])([A-Z])", "$1 $2") 
                select new PeriodFilterModel()
                    {
                        PeriodId = (int) period, Name = periodDescription
                    }).ToList();

            return Json(periodFilterModels, JsonRequestBehavior.AllowGet);
        }

    }
}
