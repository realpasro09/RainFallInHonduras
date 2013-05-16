using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;
using Rainfall.Web.Controllers;
using Rainfall.Web.Models;
using FizzWare.NBuilder;

namespace Rainfall.Web.Specs
{
    public class when_getting_period_data
    {
        static JsonResult _result;
        static PeriodDataController _periodController;

        private Establish context =
            () =>
                {
                    _periodController = new PeriodDataController();
                };
        Because of = () => _result = _periodController.Get();

        It should_not_return_null = () => _result.ShouldNotBeNull();

        It should_return_period_data = () => _result.Data.ShouldBeOfType<List<PeriodFilterModel>>();
        
    }
}