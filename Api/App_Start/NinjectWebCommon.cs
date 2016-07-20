using Api.Authorization;
using Api.Commands.IncentiveGroup;
using Api.Commands.Location;
using Api.Commands.Media;
using Api.Commands.Reports;
using Api.Commands.State;
using Api.Commands.User;
using Api.Commands.Vehicle;
using Api.Converters;
using Api.Validators;
using Data;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Api.App_Start.NinjectWebCommon), "Stop")]

namespace Api.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAutoRenterApiConfiguration>().To<AutoRenterApiConfiguration>();
            kernel.Bind<AutoRenterDatabaseContext>().ToSelf().InRequestScope();
            kernel.Bind<IErrorHandler>().To<ErrorHandler>();
            kernel.Bind<IJsonWebToken>().To<JsonWebToken>();
            kernel.Bind<IFileUploadValidator>().To<FileUploadValidator>();

            kernel.Bind<IGetUser>().To<GetUser>();
            kernel.Bind<ISearchForUsers>().To<SearchForUsers>();
            kernel.Bind<ISaveUser>().To<SaveUser>();
            kernel.Bind<IDeleteUser>().To<DeleteUser>();

            kernel.Bind<IGetAllLocations>().To<GetAllLocations>();
            kernel.Bind<IGetLocation>().To<GetLocation>();
            kernel.Bind<ISaveLocation>().To<SaveLocation>();
            kernel.Bind<IDeleteLocation>().To<DeleteLocation>();

            kernel.Bind<IGetIncentiveGroup>().To<GetIncentiveGroup>();
            kernel.Bind<IGetIncentiveGroupsForLocation>().To<GetIncentiveGroupsForLocation>();
            kernel.Bind<ISaveIncentiveGroup>().To<SaveIncentiveGroup>();

            kernel.Bind<IGetAllStates>().To<GetAllStates>();

            kernel.Bind<ISaveVehicle>().To<SaveVehicle>();
            kernel.Bind<IGetVehicle>().To<GetVehicle>();
            kernel.Bind<IDeleteVehicle>().To<DeleteVehicle>();

            kernel.Bind<IGetMedia>().To<GetMedia>();
            kernel.Bind<ISaveMedia>().To<SaveMedia>();            

            kernel.Bind<IGetActiveRentToOwn>().To<GetActiveRentToOwn>();
        }
    }
}
