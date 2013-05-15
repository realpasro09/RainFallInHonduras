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

            var almanacDayGridSummary = new AlmanacDayGridSummaryModel {aaData = mappedAlmanacDays};

            return Json(almanacDayGridSummary, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRainfallDataByLocation(int locationId)
        {
            var almanacDays = _repository.Query<AlmanacDay>(x => x.City.Id == locationId);
            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);

            var almanacDayGridSummary = new AlmanacDayGridSummaryModel { aaData = mappedAlmanacDays };

            return Json(almanacDayGridSummary, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRainfallData(int locationId, int periodId, TableParameter param)
        {
            var daysfrom = 0;
            var daysTo = 0;

            switch ((PeriodType)periodId)
            {
                case PeriodType.Today:
                    daysfrom = 0;
                    daysTo = 0;
                    break;
                case PeriodType.Yesterday:
                    daysfrom = -1;
                    daysTo = -1;
                    break;
                case PeriodType.LastWeek:
                    daysfrom = -7;
                    daysTo = -1;
                    break;
                case PeriodType.LastMonth:
                    daysfrom = -30;
                    daysTo = -1;
                    break;
                case PeriodType.LastYear:
                    daysfrom = -365;
                    daysTo = -1;
                    break;
                default:
                    daysfrom = 0;
                    daysTo = 0;
                    break;
            }

            var totalRecords = locationId != 0
                                ? _repository.Query<AlmanacDay>(
                                              x => x.Date.Date >= SystemDateTime.Now().AddDays(daysfrom).Date
                                                  && x.Date.Date <= SystemDateTime.Now().AddDays(daysTo).Date
                                                  && x.City.Id == locationId).Count()
                                : _repository.Query<AlmanacDay>(
                                              x => x.Date.Date >= SystemDateTime.Now().AddDays(daysfrom).Date
                                                  && x.Date.Date <= SystemDateTime.Now().AddDays(daysTo).Date).Count();

            IEnumerable<AlmanacDay> almanacDays = locationId != 0
                                          ? _repository.Query<AlmanacDay>(
                                              x => x.Date.Date >= SystemDateTime.Now().AddDays(daysfrom).Date
                                                  && x.Date.Date <= SystemDateTime.Now().AddDays(daysTo).Date 
                                                  && x.City.Id == locationId)
                                                       .OrderByDescending(x => x.Date).Skip(param.iDisplayStart).Take(param.iDisplayLength)
                                          : _repository.Query<AlmanacDay>(
                                              x => x.Date.Date >= SystemDateTime.Now().AddDays(daysfrom).Date
                                                  && x.Date.Date <= SystemDateTime.Now().AddDays(daysTo).Date)
                                                       .OrderByDescending(x => x.Date).Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var mappedAlmanacDays =
                _mappingEngine.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayGridItemModel>>(almanacDays);

            var almanacDayGridSummary = new AlmanacDayGridSummaryModel
                {
                    sEcho = param.sEcho,
                    aaData = mappedAlmanacDays,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };

            return Json(almanacDayGridSummary, JsonRequestBehavior.AllowGet);
        }
    }
}
