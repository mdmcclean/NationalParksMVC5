using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Capstone.Web.DAL;
using Capstone.Web.Mock;

namespace Capstone.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            string connectionString = ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString;
            kernel.Bind<ISurveyDAL>().To<SurveyDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IParkDAL>().To<ParkDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IWeatherDAL>().To<WeatherDAL>().WithConstructorArgument("connectionString", connectionString);

            //Mock Database
            //kernel.Bind<ISurveyDAL>().To<SurveyMock>();
            //kernel.Bind<IParkDAL>().To<ParkMock>();
            //kernel.Bind<IWeatherDAL>().To<WeatherMock>();

            return kernel;
        }
    }

}
