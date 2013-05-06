using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AcklenAvenue.Testing.Moq;
using AutoMapper;
using Machine.Specifications;
using Moq;
using Rainfall.Domain.Entities;
using Rainfall.Domain.Services;
using Rainfall.Web.Controllers;
using Rainfall.Web.Models;
using It = Machine.Specifications.It;

namespace Rainfall.Web.Specs
{
    public class when_getting_rainfall_data
    {
        private Establish context = () =>
            {
                _mockRepository = new Mock<IRepository>();
                _mockMappingEngine = new Mock<IMappingEngine>();
                _rainfallController = new RainfallDataController(_mockRepository.Object, _mockMappingEngine.Object);
                IQueryable<AlmanacDay> almanacDays = new List<AlmanacDay>() {new AlmanacDay(), new AlmanacDay()}.AsQueryable();
                _mockRepository.Setup(x => x.Query(ThatHas.AnExpressionFor<AlmanacDay>().Build()))
                               .Returns(almanacDays);
                _almanacDayModels = new List<AlmanacDayModel>() {new AlmanacDayModel(), new AlmanacDayModel()};
                _mockMappingEngine.Setup(x => x.Map<IEnumerable<AlmanacDay>, IEnumerable<AlmanacDayModel>>(almanacDays))
                                  .Returns(_almanacDayModels);
            };

        private Because of = () => _result = _rainfallController.Get();

        private It should_return_rainfall_data = () => _result.Data.ShouldBeLike(_almanacDayModels);
        
        private static Mock<IRepository> _mockRepository;
        private static Mock<IMappingEngine> _mockMappingEngine;
        private static RainfallDataController _rainfallController;
        private static JsonResult _result;
        private static List<AlmanacDayModel> _almanacDayModels;
    }
}
