﻿using System;
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

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            var almanacDays = _repository.Query<AlmanacDay>(x => true);
            
            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);

            return Json(mappedAlmanacDays, JsonRequestBehavior.AllowGet);
        }
    }
}