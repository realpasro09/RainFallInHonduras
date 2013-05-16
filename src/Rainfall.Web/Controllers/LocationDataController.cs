using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Rainfall.Domain.Entities;
using Rainfall.Domain.Services;
using Rainfall.Web.Models;

namespace Rainfall.Web.Controllers
{
    public class LocationDataController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;
        public LocationDataController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }
        
        // GET: /LocationData/
        public JsonResult Get()
        {
            var locations = _repository.Query<City>(x => true).OrderBy(x => x.Name);
            var mappedLocations =
                _mappingEngine.Map<IEnumerable<City>, IEnumerable<CityFilterLocationModel>>(locations).ToList();

            mappedLocations.Add(new CityFilterLocationModel() {CityId = 0, Name = "All"});
            var sortedLocations = mappedLocations.OrderBy(x => x.Name);
            return Json(sortedLocations, JsonRequestBehavior.AllowGet);
        }
    }
}
