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

            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);

            var almanacDayGridSummary = new AlmanacDayGridSummaryModel {AlmanacDays = mappedAlmanacDays};

            return Json(almanacDayGridSummary, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRainfallDataByLocation(int locationId)
        {
            var almanacDays = _repository.Query<AlmanacDay>(x => x.City.Id == locationId);
            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);

            var almanacDayGridSummary = new AlmanacDayGridSummaryModel { AlmanacDays = mappedAlmanacDays };

            return Json(almanacDayGridSummary, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRainfallData(int locationId, int periodId)
        {
            var daysToAdd = 0;

            switch ((PeriodType)periodId)
            {
                case PeriodType.Today:
                    daysToAdd = 0;
                    break;
                case PeriodType.Yesterday:
                    daysToAdd = -1;
                    break;
                case PeriodType.LastWeek:
                    daysToAdd = -7;
                    break;
                case PeriodType.LastMonth:
                    daysToAdd = -30;
                    break;
                case PeriodType.LastYear:
                    daysToAdd = -365;
                    break;
                default:
                    daysToAdd = 0;
                    break;
            }

            IEnumerable<AlmanacDay> almanacDays = locationId != 0
                                          ? _repository.Query<AlmanacDay>(
                                              x => x.Date.Date >= SystemDateTime.Now().AddDays(daysToAdd).Date && x.City.Id == locationId)
                                                       .OrderByDescending(x => x.Date)
                                          : _repository.Query<AlmanacDay>(x => x.Date.Date >= SystemDateTime.Now().AddDays(daysToAdd).Date)
                                                       .OrderByDescending(x => x.Date);

            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);

            var almanacDayGridSummary = new AlmanacDayGridSummaryModel { AlmanacDays = mappedAlmanacDays };

            return Json(almanacDayGridSummary, JsonRequestBehavior.AllowGet);
        }
    }
}
