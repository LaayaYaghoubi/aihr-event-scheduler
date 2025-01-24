using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace AIHR.EventScheduler.WebApi.Configs.Services;

public static class ConfigAutofac
{
   public static ConfigureHostBuilder AddAutofacConfig(
      this ConfigureHostBuilder builder)
   {
      builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
      builder.ConfigureContainer<ContainerBuilder>(b =>
         b.RegisterModule(new AutofacBusinessModule())
      );
      return builder;
   }
}