using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Machine.Specifications;
using Rainfall.Web.Infrastructure;

namespace Rainfall.Web.Specs
{
    public class when_instantiating_all_controllers
    {
        static List<Type> _controllers;
        static ILifetimeScope _container;
        static Exception _exception;

        Establish context = () =>
                                {
                                    var containerBuilder = new ContainerBuilder();
                                    _container = new Bootstrapper(containerBuilder).Run();
                                    
                                    _controllers = new TypeScanner.TypeScanner().GetTypesOf<Controller>().ToList();
                                };

        Because of = () => _exception = Catch.Exception(() => _controllers.ForEach(x => _container.Resolve(x)));

        It should_instantiate_all_controllers_without_throwing_an_exception = () => _exception.ShouldBeNull();
    }
}