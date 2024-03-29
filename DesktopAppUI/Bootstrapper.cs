﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using Caliburn.Micro;
using DesktopAppUI.Helpers;
using DesktopAppUI.Library.Api;
using DesktopAppUI.Library.Helpers;
using DesktopAppUI.Library.Models;
using DesktopAppUI.Models;
using DesktopAppUI.ViewModels;

namespace DesktopAppUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
        PasswordBoxHelper.BoundPasswordProperty,
        "Password",
        "PasswordChanged");

            // other bootstrapper stuff here
        }
        private IMapper ConfigureAutomapper()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductDisplayModel>();
                cfg.CreateMap<CartProductModel, CartProductDisplayModel>();
            });

            var mapper = configMapper.CreateMapper();

            return mapper;
        }

        protected override void Configure()
        {
            _container.Instance<IMapper>(ConfigureAutomapper());

            _container.Instance(_container)
                .PerRequest<IUserApi, UserApi>()
                .PerRequest<ISaleApi, SaleApi>()
                .PerRequest<IProductApi, ProductApi>();

            _container
               .Singleton<IWindowManager, WindowManager>()
               .Singleton<IEventAggregator, EventAggregator>()
               .Singleton<IAPIHelper, APIHelper>()
               .Singleton<IConfigHelper, ConfigHelper>()
               .Singleton<ILoggedInUserModel, LoggedInUserModel>();


            GetType().Assembly.GetTypes()
             .Where(type => type.IsClass)
             .Where(type => type.Name.EndsWith("ViewModel"))
             .ToList()
             .ForEach(viewModelType => _container.RegisterPerRequest(
                  viewModelType, viewModelType.ToString(), viewModelType));
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
