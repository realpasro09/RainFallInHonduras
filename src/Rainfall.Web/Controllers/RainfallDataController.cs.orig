using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Rainfall.Domain;
using Rainfall.Domain.Entities;
using Rainfall.Domain.Services;
using Rainfall.Web.Models;

namespace Rainfall.Web.Controllers
{
    public class RainfallDataController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public RainfallDataController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }

        public JsonResult Get()
        {
            var almanacDays = 
                _repository.Query<AlmanacDay>(x=> x.Date >= SystemDateTime.Now().AddDays(-30)).OrderByDescending(x=>x.Date);

            var summary = ReturnRainfallInformationSummary(almanacDays);
            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);

            var almanacDaysGridSummary = new AlmanacDayGridSummary(mappedAlmanacDays,summary);
            return Json(almanacDaysGridSummary, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRainfallDataByLocation(int locationId)
        {
<<<<<<< HEAD
            var almanacDays = _repository.Query<AlmanacDay>(x => x.City.Id == locationId);
=======
            var almanacDays = _repository.Query<AlmanacDay>(x => x.CityId == locationId);
            var summary = ReturnRainfallInformationSummary(almanacDays);
>>>>>>> c9b1275f35a0f178a6f211c07a95e8fb205f3251
            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);
            var almanacDaysGridSummary = new AlmanacDayGridSummary(mappedAlmanacDays, summary);
            return Json(almanacDaysGridSummary, JsonRequestBehavior.AllowGet);
        }

        private double[] ReturnRainfallInformationSummary(IQueryable<AlmanacDay> almanacDays)
        {
           
            var sumary = new double[7];
            sumary[0] =
                almanacDays.Max<AlmanacDay>(x => x.Get24HrsTempHigh());
            sumary[1] =
                almanacDays.Min<AlmanacDay>(x => x.Get24HrsTempLow());
            sumary[2] =
                almanacDays.Select(x => x.Get24HrsTempHigh()).ToList<Double>().Average();
            sumary[3] =
                almanacDays.Select(x => x.Get24HrsTempLow()).ToList<Double>().Average();
            sumary[4] =
                almanacDays.Max<AlmanacDay>(x => x.Get24HrsPrecipitation());
            sumary[5] =
                almanacDays.Min<AlmanacDay>(x => x.Get24HrsPrecipitation());
            
            sumary[6] =
                    almanacDays.Select(x => x.Get24HrsPrecipitation()).ToList<Double>().Average();
            
            return sumary;
        }
    }
}
