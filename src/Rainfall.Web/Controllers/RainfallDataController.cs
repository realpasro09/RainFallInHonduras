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

        //
        // GET: /RainfallData/

        public JsonResult Get()
        {
            var almanacDays = _repository.Query<AlmanacDay>(x=> x.Date >= SystemDateTime.Now().AddDays(-30)).OrderByDescending(x=>x.Date);
            
            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);

            return Json(mappedAlmanacDays, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocations()
        {
            var locations = _repository.Query<City>(x => true).OrderBy(x => x.Name);
            var mappedLocations =
                _mappingEngine.Map<IEnumerable<City>, IEnumerable<CityFilterLocationModel>>(locations);

            ((List<CityFilterLocationModel>)mappedLocations).Add(new CityFilterLocationModel(){CityId = 0, Name = "All"});
            mappedLocations = mappedLocations.OrderBy(x => x.Name);
            return Json(mappedLocations, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRainfallDataByLocation(int locationId)
        {
            var almanacDays = _repository.Query<AlmanacDay>(x => x.CityId == locationId);
            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);

            return Json(mappedAlmanacDays, JsonRequestBehavior.AllowGet);
        }
    }
}
