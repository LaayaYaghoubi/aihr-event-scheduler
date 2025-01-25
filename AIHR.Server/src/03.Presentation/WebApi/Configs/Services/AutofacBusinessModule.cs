using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Persistence.EF;
using AIHR.EventSchedulerInfrastructure.Dates.Services;
using AIHR.EventSchedulerInfrastructure.Services;
using Autofac;

namespace AIHR.EventScheduler.WebApi.Configs.Services;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder container)
    {
        var serviceAssembly = typeof(IScheduledEventService).Assembly;
        var persistentAssembly = typeof(EfUnitOfWork).Assembly;


        container.RegisterAssemblyTypes(
                serviceAssembly,
                persistentAssembly)
            .AssignableTo<IService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        container.RegisterAssemblyTypes(persistentAssembly, serviceAssembly)
            .AssignableTo<IRepository>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        container.RegisterType<DateTimeService>()
            .As<IDateTimeService>()
            .SingleInstance();
        
        container.RegisterType<UserService>()
            .As<IUserService>()
            .SingleInstance();

        base.Load(container);
    }
}